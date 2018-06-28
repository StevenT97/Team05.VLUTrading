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
        public ActionResult Index(string searchString)
        {
            //var users = db.Users.Include(u => u.Role).Include(u => u.UserInfo);
            //return View(users.ToList());

            var movies = from m in db.Users
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.LastName.Contains(searchString));
            }
            if (Session["ID"] != null)
            {
                return View(movies);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            //ViewBag.Role_ID = new SelectList(db.Roles, "Id", "Role_Name");
            //ViewBag.Id = new SelectList(db.UserInfoes, "ID", "Phone");
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "IsEmailVerified,ActivationCode")] User user)
        {
            
            bool Status = false;
            string message = "";
            //Model Validation
            if (ModelState.IsValid)
            {
                //Email is already Exist
                var isExist = IsEmailExist(user.EmailID);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View();
                }

                //Generate Acivation Code
                user.ActivationCode = Guid.NewGuid();

                //Password hasing
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
                //
                user.IsEmailVerified = false;

                //Set Resset password
                user.ResetPasswordCode = "";

                //
                user.Role_ID = 3;

                //Save to database
                using (VLUTradingDBEntities db = new VLUTradingDBEntities())
                {
                    //try
                    //{
                    db.Users.Add(user);
                    db.SaveChanges();
                    //}
                    //catch (DbEntityValidationException dbEx)
                    //{
                    //    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    //    {
                    //        foreach (var validationError in validationErrors.ValidationErrors)
                    //        {
                    //            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    //        }
                    //    }
                    //}

                }

                //Send Email to User
                SendVerificationLinkEmail(user.EmailID, user.ActivationCode.ToString());
                message = " Registration sucessfully done. Account activation link" +
                    " has been sent to your email : " + user.EmailID;
                Status = true;
            }
            else
            {
                message = " Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }
        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (VLUTradingDBEntities db = new VLUTradingDBEntities())
            {
                var v = db.Users.Where(u => u.EmailID == emailID).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/Account/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("thienvq97@gmail.com", "Trading Vanlanguniversity");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Gmailqu4ngthien"; //Replace with actual password

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is successfully created";

                body = "<br/><br/>We are excited to tell you that your Trading account is" +
                   " successfully created. Please click on the below link to verify your account" +
                   "<br/><br/><a href='" + link + "'>" + link + "</a>";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi! <br/><br/>We got request for reset your account password. Please click on the below link to verify your account" +
                    "<br/><br/><a href=" + link + ">Reset Password Link</a>";
            }



            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role_ID = new SelectList(db.Roles, "Id", "Role_Name", user.Role_ID);
            ViewBag.Id = new SelectList(db.UserInfoes, "ID", "Phone", user.Id);
            return View(user);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailID,DateOfBirth,Role_ID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.message = "error";
            return View(user);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //[HttpPost]
        //public ActionResult Search  (string EmailID, string txtSearch)
        //{
        //    var search = db.Users.Where(x => x.IsEmailVerified == true).ToList();
        //    if (EmailID != null)
        //    {
        //        search = search.Where(x => x.EmailID == EmailID).ToList();
        //    }
        //    if (txtSearch != "ten email")
        //    {
        //        search = search.Where(x => x.EmailID.Contains(txtSearch)).ToList();
        //    }
        //    return View(search);
        //}
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Icons()
        {
            return View();
        }
        public ActionResult Map()
        {
            return View();
        }
        public ActionResult Notifications()
        {
            return View();
        }
        public ActionResult Tables()
        {
            return View();
        }
        public ActionResult Typography()
        {
            return View();
        }
        public ActionResult Update()
        {
            return View();
        }
        public ActionResult User()
        {
            return View();
        }
    }
}
