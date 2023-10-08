using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using VSG.Models;
using PagedList;
using VSG.Model;
using Newtonsoft.Json;

namespace VSG.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        public ActionResult Index(int? page, string SearchString)
        {
            var context = new VSGModel();
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 8;
            var lsSanPham = context.SanPhams
                .AsQueryable()
                .OrderByDescending(x => x.MaSanPham);
            PagedList.IPagedList<SanPham> models = new PagedList<SanPham>(lsSanPham, pageNumber, pageSize);
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
                (from m in context.SanPhams.ToList()
                 where
                 m.TenSanPham.Contains(SearchString)

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
        //public ActionResult GetSanPhamByCategory(int id, int? page)
        //{
        //    var context = new VSGModel();
        //    int pageSize = 4;
        //    int pageIndex = page.HasValue ? page.Value : 1;
        //    var result = context.SanPhams.Where(p => p.CategoryId == id).ToList().ToPagedList(pageIndex, pageSize);
        //    return View("Index", result);
        //}
        //public ActionResult GetCategory()
        //{
        //    var context = new VSGModel();
        //    var listCategory = context.Categories.ToList();
        //    return PartialView(listCategory);
        //}
    

        // Lấy cart từ Session (danh sách CartItem)
       

        /* public ActionResult Search(string SearchString)
         {
             VSGModel context = new VSGModel();
             var listProduct = context.Shoe_Services.ToList();
             var results = listProduct.Where(p => p.Title.Contains(SearchString)).ToList();
             if (results.Count() > 0)

                 return View("Index","Shoe", results);

             TempData["SearchError"] = "Không tìm thấy dịch vụ này";
             return RedirectToAction("Index");

         }*/
        /*public ActionResult SearchOder(String searchTerm)
        {
            List<Order> orders = db.Orders.Where(o => o.OrderNumber.Contains(searchTerm) || o.CustomerName.Contains(searchTerm)).ToList();

            if (orders.Count > 0)
            {
                return View("Index", orders);
            }

            return HttpNotFound("Không tìm thấy đơn hàng này");
        }*/
        public ActionResult AddToCart(int id)
        {
            return Content("thêm thành công!!");
        }
    }
}