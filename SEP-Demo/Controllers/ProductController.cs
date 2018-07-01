using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
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
        public ActionResult Cart(List<string> Item)
        {
            if (ModelState.IsValid)
            {
                var orderCode = "VLUTrading-" + DateTime.Now.Millisecond + 5;
                int User_ID = (int)Session["ID"];
                OrderList ordList = new OrderList();
                ordList.OrderCode = orderCode;
                ordList.UserOrder = User_ID;
                ordList.Date = DateTime.Now.Date;

                db.OrderLists.Add(ordList);
                Order ord = new Order();
                var count = Item.Count();
                for (int i = 0; i < count; i++)
                {
                    ord.OrderID = orderCode;
                }

                db.SaveChanges();

            }
            return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Login", "Account");
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
                return RedirectToAction("Login", "Account");
            }
        }


        //Thuan Nguyen - Create New Product

        [HttpGet]
        public ActionResult Create()
        {
            //ViewBag.Category = new SelectList(db.ProductCategories, "ID", "Name");
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
            return RedirectToAction("ViewProfile","Account");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.Products.Find(id);
            ViewBag.Category = new SelectList(db.ProductCategories, "ID", "Name");
            //Product currentP = db.Products.Find(id);
            return PartialView("EditPartial",model);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product Pr,int gia)
        {
            Product currentP = db.Products.Find(Pr.ID);
            var prices = db.Prices.FirstOrDefault(x => x.ProductID == Pr.ID);
            if (ModelState.IsValid)
            {
                if(currentP != null)
                {
                    var a = Pr.Image_Detail;
                    
                    if (Pr.Image_Detail != null)
                    {
                        var path_Image = ImagesU(Pr).Trim();
                        var detail = path_Image.Split(' ');
                        if (detail.Count() == 4)
                        {
                            currentP.Picture02 = detail[0];
                            currentP.Picture03 = detail[1];
                            currentP.Picture04 = detail[2];
                            currentP.Picture05 = detail[3];
                        }
                        if (detail.Count() == 3)
                        {
                            currentP.Picture02 = detail[0];
                            currentP.Picture03 = detail[1];
                            currentP.Picture04 = detail[2];
                        }
                        if (detail.Count() == 2)
                        {
                            currentP.Picture02 = detail[0];
                            currentP.Picture03 = detail[1];
                        }
                        if (detail.Count() == 1)
                        {
                            if (detail[0] != "")
                            {
                                currentP.Picture02 = detail[0];
                            }
                            
                        }

                    }
                    //Update new Infor
                    currentP.ID = Pr.ID;
                    currentP.Picture01 = Pr.Picture01;
                    currentP.Name = Pr.Name;
                    currentP.CategoryID = Pr.CategoryID;
                    currentP.Description = Pr.Description;
                    currentP.Quantity = Pr.Quantity;
                    //currentP.Picture01 = Images(Pr);
                    currentP.StatusID = 4;
                    prices.Price1 = gia;
                    db.SaveChanges();
                }
            }
            //If fail => change to RedirectToAction("ViewProfile","Account");
            return RedirectToAction("ViewProfile","Account");
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
                    filename = filename  + extension;
                    b = "/Images/ProductAvatar/" + filename;
                    s = string.Concat(s, b, " ");
                    filename = Path.Combine(Server.MapPath("/Images/ProductAvatar/"), filename);
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
            filename = Path.Combine(Server.MapPath("/Images/ProductAvatar/"), filename);
            p.Image.SaveAs(filename);
            return s;
        }
        
        
    }
}