using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Services;
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

            Product productDetail = db.Products.Find(id);
            
            if (productDetail == null)
            {
                return HttpNotFound();
            }
            return View(productDetail);

        }

        [HttpGet]
        public ActionResult Cart()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login","Account");
            }
        }

        [HttpPost]
        //[WebMethod]
        public ActionResult Cart(string Orders)
        {

            var cart = JsonConvert.DeserializeObject<List<OrdersItem>>(Orders);
            Random rnd = new Random();
            if (cart.Count > 0)
            {
                var orderCode = "VLUTrading-" + DateTime.Now + "-" + rnd.Next(0,999) +"-" + DateTime.Now.Millisecond + rnd.Next(0, 999);
                int User_ID = (int)Session["ID"];
                OrderList ordList = new OrderList();
                ordList.OrderCode = orderCode;
                ordList.UserOrder = User_ID;
                ordList.Date = DateTime.Now.Date;

                db.OrderLists.Add(ordList);
                for (int i = 0; i < cart.Count; i++)
                {
                    Order itemord = new Order();
                    itemord.OrderID = orderCode;
                    itemord.ProductID = cart[i].id;
                    itemord.UserTrade = cart[i].usertrade;
                    itemord.Date = DateTime.Now.Date;
                    itemord.Quantity = cart[i].qty;
                    itemord.Price = cart[i].price;
                    itemord.SubPrice = cart[i].qty * cart[i].price;
                    itemord.Status = 1;
                    db.Orders.Add(itemord);
                }

                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }   
        public class OrdersItem
        {
            public int id;
            public string name;
            public string s;
            public int qty;
            public int usertrade;
            public string username;
            public int price;
        }
        


        public ActionResult History()
        {
            return View();
        }

     
        //Thuan Nguyen - Create New Product

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(db.ProductCategories, "ID", "Name");
            return PartialView("CreatePartial");
        }

        [HttpPost]
        public ActionResult Create(Product P, int gia)
        {
            if (ModelState.IsValid)
            {
                var path_Image = ImagesU(P).Trim();
                var detail = path_Image.Split(' '); 
                if (detail.Count() == 4)
                {
                    P.Picture02 = detail[0];
                    P.Picture03 = detail[1];
                    P.Picture04 = detail[2];
                    P.Picture05 = detail[3];
                }
                if (detail.Count() == 3)
                {
                    P.Picture02 = detail[0];
                    P.Picture03 = detail[1];
                    P.Picture04 = detail[2];
                }
                if (detail.Count() == 2)
                {
                    P.Picture02 = detail[0];
                    P.Picture03 = detail[1];
                }
                if (detail.Count() == 1)
                {
                    P.Picture02 = detail[0];
                }


                P.Picture01 = Images(P);
                P.StatusID = 1;

                int User_ID = (int)Session["ID"];
                P.UserID = User_ID;
                //P.UnitPrice = 
                //var datatest = (int)P.ProductCategory.ID;
                db.Products.Add(P);

                //Price of current product
                Price nPr = new Price();
                
                nPr.ProductID = P.ID;
                nPr.Price1 = gia;
                nPr.Status = true;
                nPr.DateUpdate = DateTime.Now.Date;
                db.Prices.Add(nPr);
                db.SaveChanges();

            }
            return RedirectToAction("ViewProfile", "Account");
        }
        public ActionResult Delete(int id)
        {
            Product delProduct = db.Products.Find(id);
            if(delProduct != null)
            {
                
                db.Products.Attach(delProduct);
                var priceProduct = db.Prices.Where(m => m.ProductID == delProduct.ID).ToList();
                if(priceProduct.Count() > 0)
                {
                    foreach(var item in priceProduct)
                    {
                        db.Prices.Remove(item);
                    }
                }
            }
            else
            {
                return HttpNotFound();
            }
            db.Entry(delProduct).State = System.Data.Entity.EntityState.Deleted;
            
            db.SaveChanges();
            return RedirectToAction("ViewProfile","Account", new { @id ="sanpham"});
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Category = new SelectList(db.ProductCategories, "ID", "Name");
            Product currentP = db.Products.Find(id);
            return PartialView("EditPartial");
        }
        [HttpPost]
        public ActionResult Edit(Product P, int gia)
        {
            Product currentP = db.Products.Find(P.ID);

            //If fail => change to RedirectToAction("ViewProfile","Account");
            return RedirectToAction("Detail","Product");
        }
        private string ImagesU(Product p)
        {

            string filename;
            string extension;
            string b;
            string s = "";
            foreach (var file in p.Image_Detail)
            {
                if (file != null)
                {
                    filename = Path.GetFileNameWithoutExtension(file.FileName);
                    extension = Path.GetExtension(file.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssff") + extension;
                    b = "/Images/" + filename;
                    s = string.Concat(s, b, " ");
                    filename = Path.Combine(Server.MapPath("~/Images/ProductAvatar"), filename);
                    file.SaveAs(filename);
                }

            }
            return s;

        }
        public string Images(Product p)
        {
            string s = "";
            string filename;
            string extension;
            filename = Path.GetFileNameWithoutExtension(p.Image.FileName);
            extension = Path.GetExtension(p.Image.FileName);
            filename = filename + extension;
            s = "/Images/ProductAvatar/" + filename;
            filename = Path.Combine(Server.MapPath("~/ProductAvatar"), filename);
            p.Image.SaveAs(filename);
            return s;
        }
        
        
    }
}