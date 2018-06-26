using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SEP_Demo.Models;

namespace SEP_Demo.Controllers
{
    public class ProductController : Controller
    {
        private VLUTradingDBEntities db = new VLUTradingDBEntities();

        // GET: Product
        public ActionResult Product()
        {
            return View();
        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product productDetail = db.Product.Find(id);
            if (productDetail == null)
            {
                return HttpNotFound();
            }
            return View(productDetail);

        }
        public ActionResult Cart()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Cart(Bind(Include= "ID,PropertyName,Avatar,Images,PropertyType_ID,Content,Street_ID,Ward_ID,District_ID,Price,UnitPrice,Area,BedRoom,BathRoom,PackingPlace,UserID,Created_at,Create_post,Status_ID,Note,Updated_at,Sale_ID")] Order oderList)
        //{
        //    return View();
        //}


        public ActionResult History()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }
            
        }
        public ActionResult HistoryTrading()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }

        }

        //public JsonResult SaveCart(string[] cart)
        //{
        //    var orderList = db.Orders;
        //    foreach (var item in cart)
        //    {
        //        orderList.Add(item);

        //    }

        //    var message = "Success";

        //    return message;
        //}
    }
}