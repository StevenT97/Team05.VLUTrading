using System;
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
            //var users = db.Users.Include(u => u.Role).Include(u => u.UserInfo);
            //return View(users.ToList());

            var movies = from m in db.Users
                         select m;

           
            if (Session["ID"] != null)
            {
                return View(movies);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        public ActionResult Dashboard()
        {
            var id = (int)Session["RoleID"];
            if (id == 2 || id == 3)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Listproduct()
        {
            var id = (int)Session["RoleID"];
            if (id ==2)
            {
                return View();
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
       
    }
}
