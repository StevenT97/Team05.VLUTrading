using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEP_Demo.Models;

namespace SEP_Demo.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult HomeIndex()
        {
            var model = new VLUTradingDBEntities();
            var listCategory = model.ProductCategory.ToList();

            return View(listCategory);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AboutUS()
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