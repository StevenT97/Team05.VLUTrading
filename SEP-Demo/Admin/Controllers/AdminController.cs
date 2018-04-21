using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEP_Demo.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminIndex()
        {
            return View();
        }
    }
}