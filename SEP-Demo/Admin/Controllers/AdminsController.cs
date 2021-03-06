﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SEP_Demo.Models;
using SEP_Demo.Controllers;
using System.Net.Mail;

namespace SEP_Demo.Admin.Controllers
{
    public class AdminsController : Controller
    {
        private VLUTradingDBEntities db = new VLUTradingDBEntities();

        // GET: Admins
        public ActionResult Index()
        {
           
            if (Session["ID"] != null)
            {
                var id = (int)Session["RoleID"];
                if (id == 1)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        public ActionResult Dashboard()
        {
            if (Session["ID"] != null)
            {
                var id = (int)Session["RoleID"];
                if (id == 2 || id == 3)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        public ActionResult Listproduct()
        {
            if (Session["ID"] != null)
            {
                var id = (int)Session["RoleID"];
                if (id == 2)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        public ActionResult Update()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EditStatus(int id)
        {
            var model = db.Products.Find(id);
            ViewBag.Category = new SelectList(db.ProductCategories, "ID", "Name");
            //Product currentP = db.Products.Find(id);
            return PartialView("EditStatusPartial", model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStatus(Product Pr, int status)
        {
            Product currentP = db.Products.Find(Pr.ID);

                    //Update new Infor
                    currentP.StatusID = status;
                    db.SaveChanges();
              
            return RedirectToAction("Listproduct", "Admins");
        }
        [HttpGet]
        public ActionResult EditStatusAccount(int id)
        {
            var model = db.Users.Find(id);
            return PartialView("EditStatusAccountPartial", model);

        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditStatusAccount(User user, int status)
        {
            VLUTradingDBEntities dbs = new VLUTradingDBEntities();
            var currentU = dbs.Users.Find(user.Id);

            //Update new Infor
            currentU.Role_ID = status;
            currentU.ConfirmPassword = currentU.Password;
          
            try
            {
                dbs.Entry(currentU).State = EntityState.Modified;
                dbs.SaveChanges();
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index", "Admins");
        }

    }
}
