using MoMo;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VSG.Models;
using System.Reflection;
using VSG.Model;
using Microsoft.AspNet.Identity;
using System.EnterpriseServices.CompensatingResourceManager;
using VSG.Dao;

namespace VSG.Controllers
{
    public class GioHangController : Controller
    {
        VSGModel db = new VSGModel();
        public ActionResult Index()
        {

            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            if (ShoppingCart.Count == 0)
                return RedirectToAction("Index", "SanPham");
            ViewBag.Tongsoluong = ShoppingCart.Sum(p => p.SoLuong);
            ViewBag.Tongtien = ShoppingCart.Sum(p => p.SoLuong * p.Gia);
            return View(ShoppingCart);
        }
        public List<CartItem> GetShoppingCartFromSession()
        {
            var lstShoppingCart = Session["ShoppingCart"] as List<CartItem>;
            if (lstShoppingCart == null)
            {
                lstShoppingCart = new List<CartItem>();
                Session["ShoppingCart"] = lstShoppingCart;
            }
            return lstShoppingCart;
        }

        public RedirectToRouteResult AddToCart(int id)
        {
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            CartItem findCartItem = ShoppingCart.FirstOrDefault(m => m.Id == id);
            if (findCartItem == null)
            {
                SanPham findSP = db.SanPhams.First(m => m.MaSanPham == id);
                CartItem newItem = new CartItem();
                newItem.Id = findSP.MaSanPham;
                newItem.tenSP = findSP.TenSanPham;
                newItem.SoLuong = 1;
                newItem.AnhSP = findSP.Image;
                newItem.Gia = findSP.Price.Value;
                ShoppingCart.Add(newItem);
            }
            else
            {
                findCartItem.SoLuong++;
            }
            return RedirectToAction("Index", "GioHang");

        }

        public RedirectToRouteResult UpdateCart(int id, int txtQuantity)
        {
            var lstShoppingCart = Session["ShoppingCart"] as List<CartItem>;
            var itemFind = GetShoppingCartFromSession().FirstOrDefault(p => p.Id == id);
            if (itemFind != null)
            {
                 if ((Int32)txtQuantity > db.SanPhams.Where(x => x.MaSanPham == itemFind.Id).FirstOrDefault().SoLuong)

                    {
                        //alert = "So Luong San Pham Con Lai Khong Du ";
                        return RedirectToAction("Index");
                    }

                itemFind.SoLuong = txtQuantity;
                    Session["ShoppingCart"] = lstShoppingCart;
                }
                return RedirectToAction("Index");
            }
        public ActionResult CartSummary()
        {
            ViewBag.CartCount = GetShoppingCartFromSession().Sum(p => p.SoLuong);
            return PartialView("CartSummary");
        }
        [HttpGet]
        public ActionResult Order()
        {
            var cart = Session["ShoppingCart"];
            var list = new List<CartItem>();
            if(cart != null )
            {
                list = (List<CartItem>) cart;

            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Order(string shipName, string mobile, string address, string email)
        {
            var order = new Orderr();
            order.CreatedDate = DateTime.Now;
            order.ShipName = shipName;
            order.ShipAddress = address;
            order.ShipMobile = mobile;
            order.ShipEmail = email;
            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session["ShoppingCart"];
                var detailDao = new VSG.Dao.OrderDetailDao();
                foreach(var item in cart)
                {
                    var orderDetail = new OrderDetaill();
                    orderDetail.SanPhamId = item.Id;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Gia;
                    orderDetail.Quantity = item.SoLuong;
                    detailDao.Insert(orderDetail);
                }
            }
            catch(Exception ex)
            {
                return Redirect("/loi-thanh-toan");
            }
            return RedirectToAction("ConfirmOrder", "GioHang");
        }

        public ActionResult ConfirmOrder()
        {
            return View();
        }

        public RedirectToRouteResult RemoveCartItem(int id)
        {
            var itemFind = GetShoppingCartFromSession().FirstOrDefault(p => p.Id == id);
            GetShoppingCartFromSession().Remove(itemFind);
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult Delete()
        {
            GetShoppingCartFromSession().Clear();
            return RedirectToAction("Index");
        }
        public ActionResult Payment()
        {
            #region lay tong tien thanh toan momo
            List<CartItem> giohang = Session["ShoppingCart"] as List<CartItem>;
            float tongtien = 0;

            foreach (CartItem item in giohang)
            {
                tongtien += item.SoLuong * (int)item.Gia;
            }
            #endregion

            //List<Book> books = Session["Cart"] as List<Book> ;
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOOJOI20210710";
            string accessKey = "iPXneGmrJH0G8FOP";
            string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            string orderInfo = "thanh toan";
            string returnUrl = "https://localhost:44394/Home/ConfirmPaymentClient";
            string notifyurl = "https://4c8d-2001-ee0-5045-50-58c1-b2ec-3123-740d.ap.ngrok.io/Home/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

            string amount = tongtien + "";
            string orderid = DateTime.Now.Ticks.ToString(); //mã đơn hàng
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        //Khi thanh toán xong ở cổng thanh toán Momo, Momo sẽ trả về một số thông tin, trong đó có errorCode để check thông tin thanh toán
        //errorCode = 0 : thanh toán thành công (Request.QueryString["errorCode"])
        //Tham khảo bảng mã lỗi tại: https://developers.momo.vn/#/docs/aio/?id=b%e1%ba%a3ng-m%c3%a3-l%e1%bb%97i
        public ActionResult ConfirmPaymentClient(Result result)
        {
            //lấy kết quả Momo trả về và hiển thị thông báo cho người dùng (có thể lấy dữ liệu ở đây cập nhật xuống db)
            string rMessage = result.message;
            string rOrderId = result.orderId;
            string rErrorCode = result.errorCode; // = 0: thanh toán thành công
            return View();
        }

        [HttpPost]
        public void SavePayment()
        {
            //cập nhật dữ liệu vào db
            String a = "";
        }
    }
}





