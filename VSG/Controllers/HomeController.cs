using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoMo;
using VSG.Models;
using System.Configuration;
using VSG.Others;
using PagedList;
using VSG.Model;

namespace VSG.Controllers
{
    public class HomeController : Controller
    {
        VSGModel objModel = new VSGModel();
        public ActionResult Index()
        {
            //var lstCategory = objModel.Categories.ToList();
            //var lstShoe = objModel.Shoes.ToList();
            //Shoe_Category objShoe_Category = new Shoe_Category();
            //objShoe_Category.ListCategory = lstCategory;
            //objShoe_Category.ListShoe = lstShoe;
            //return View(objShoe_Category);
            return View();
        }
       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}
