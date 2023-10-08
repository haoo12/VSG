using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VSG.Models;
using PagedList;
using VSG.Model;
using System.Data.Entity.Migrations;

namespace VSG.Controllers
{
    public class ShoeController : Controller
    {
        // GET: Shoe
        public ActionResult Index(int? page, string SearchString)
        {
            var context = new VSGModel();
         
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 8;
            var lsShoes = context.Shoe_Services
                .AsQueryable()
                .OrderByDescending(x => x.Id);
            PagedList.IPagedList<Shoe_Service> models = new PagedList<Shoe_Service>(lsShoes, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;

            if (!string.IsNullOrEmpty(SearchString))
            {
                char[] charArray = SearchString.ToCharArray();
                bool foundSpace = true;
                //sử dụng vòng lặp for lặp từng phần tử trong mảng
                for (int i = 0; i < charArray.Length; i++)
                {
                    //sử dụng phương thức IsLetter() để kiểm tra từng phần tử có phải là một chữ cái
                    if (Char.IsLetter(charArray[i]))
                    {
                        if (foundSpace)
                        {
                            //nếu phải thì sử dụng phương thức ToUpper() để in hoa ký tự đầu
                            charArray[i] = Char.ToUpper(charArray[i]);
                            foundSpace = false;
                        }
                    }
                    else
                    {
                        foundSpace = true;
                    }
                }
                //chuyển đổi kiểu mảng char thàng string
                SearchString = new string(charArray);
                var results = 
                (from m in context.Shoe_Services.ToList()
                 where
                 m.Title.Contains(SearchString)

                 select m);
                if (results.Count() > 0)
                {                
                    int pageIndex = page.HasValue ? page.Value : 1;
                    var result = results.ToList().ToPagedList(pageIndex, pageSize);
                    return View("Index", result);
                }
                return HttpNotFound("Khong tim thay dich vu!!! Moi nhap lai");
            }
            return View(models);
        }
        public ActionResult GetShoeByCategory(int id, int? page)
        {
            var context = new VSGModel();
            int pageSize = 4;
            int pageIndex = page.HasValue ? page.Value : 1;
            var result = context.Shoes.Where(p => p.CategoryId == id).ToList().ToPagedList(pageIndex, pageSize);
            return View("Index", result);
        }
        public ActionResult GetCategory()
        {
            var context = new VSGModel();
            var listCategory = context.Categories.ToList();
            return PartialView(listCategory);
        }
        public ActionResult Details(int id)
        {
            var context = new VSGModel();
            var firstShoe = context.Shoe_Services.FirstOrDefault(p => p.Id == id);
            if (firstShoe == null)
                return HttpNotFound("Không tìm thấy mã giày này!");
            return View(firstShoe);
        }
        public ActionResult AddToCart(int id)
        {
            return Content("thêm thành công!!");
        }
        public ActionResult ServiceOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ServiceOrder(FormCollection collection, DatDichvu d)
        {
            VSGModel context = new VSGModel();
            var Hoten = collection["HoTen"];
            var Sdt = collection["SDT"];
            var Madv = collection["Id"];
            var Tendv = collection["Title"];
            var Ngaydat = collection["Ngaydat"];
            var Ghichu = collection["Ghichu"];


            if (string.IsNullOrEmpty(Hoten) || string.IsNullOrEmpty(Sdt) || string.IsNullOrEmpty(Tendv))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                d.HoTen = Hoten.ToString();
                var sdt = collection["SDT"];
                if (!string.IsNullOrEmpty(Sdt))
                {
                    // Loại bỏ các ký tự không phải là số
                    string phoneNumber = new string(sdt.Where(char.IsDigit).ToArray());

                    // Kiểm tra độ dài số điện thoại
                    if (phoneNumber.Length == 10)
                    {
                        d.SDT = phoneNumber;
                    }
                    else
                    {
                        return HttpNotFound("Số điện thoại không hợp lệ!!");
                    }
                }
                if (int.TryParse(Madv, out int madvValue) && madvValue >= 0)
                {
                    d.Id = Convert.ToInt32(Madv);
                }
                else
                {
                    return HttpNotFound("Mã dịch vụ không được nhỏ hơn 0");
                }
                d.Title = Tendv.ToString();

                d.NgayDat = DateTime.Now;



                d.GhiChu = Ghichu.ToString();


                context.DatDichvus.AddOrUpdate(d);
                context.SaveChanges();

                return RedirectToAction("Index", "Shoe");
            }
            return this.ServiceOrder();
        }
        public ActionResult Datdichvu()
        {
            VSGModel context = new VSGModel();
            var ddv = context.DatDichvus.ToList();
            var dvs = new List<string>();
            return View(ddv);

        }
    }
}
