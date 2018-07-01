using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEP_Demo.Models;
using System.Net.Mail;
using System.Net;
using System.Web.Security;
using System.Data.Entity.Validation;

namespace SEP_Demo.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Exclude = "IsEmailVerified,ActivationCode")] User user)
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
        //Verify Account
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (VLUTradingDBEntities db = new VLUTradingDBEntities())
            {
                db.Configuration.ValidateOnSaveEnabled = false; // This line i have added to avoid
                                                                // Confirm password does not match issue on save changes
                var v = db.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    db.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }

            }
            ViewBag.Status = Status;
            return View();
        }

        // Login
        public ActionResult Login()
        {
            return View();
        }
        //Login Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string EmailID, string Password)
        {
            VLUTradingDBEntities db = new VLUTradingDBEntities();
            var user = db.Users.SingleOrDefault(x => x.EmailID == EmailID);
            //ThuanNguyen - Start
           // var userInfor = db.UserInfoes.SingleOrDefault(x => x.ID == user.Id);
            
            //ThuanNguyen - End
            if (user != null)
            {
                if (user.Password.Equals(Crypto.Hash(Password)))
                {
                    Session["ID"] = user.Id;
                    Session["UserName"] = user.FirstName.ToString();
                    Session["EmailID"] = user.EmailID.ToString();
                    //ThuanNguyen - Start
                    var userRole = db.Roles.SingleOrDefault(x => x.Id == user.Role_ID);
                    Session["RoleID"] = Convert.ToInt32(user.Role_ID);
                    Session["RoleName"] = userRole.Role_Name.ToString();
                    //ThuanNguyen - End
                    return RedirectToAction("HomeIndex", "Home");
                }
                else
                {
                    return RedirectToAction("LogIn", "Account");
                }
            }
            else
            {
                ViewBag.Message = "* Account Invalid";
            }
            return View();
        }

        public ActionResult LogOut()
        {
            //FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request

            return RedirectToAction("HomeIndex", "Home");
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(string EmailID)
        {
            //Verify Email ID
            //Generate Reset password link
            //Send Email
            string message = "";
            bool Status = false;

            using (VLUTradingDBEntities db = new VLUTradingDBEntities())
            {
                var account = db.Users.Where(a => a.EmailID == EmailID).FirstOrDefault();
                if (account != null)
                {
                    //Send Email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.EmailID, resetCode, "ResetPassword");
                    account.ResetPasswordCode = resetCode;
                    //
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    message = "reset password link has been sent to your email.";
                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find the account associated with this link
            //redirect to reset password page
            using (VLUTradingDBEntities db = new VLUTradingDBEntities())
            {
                var user = db.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (VLUTradingDBEntities db = new VLUTradingDBEntities())
                {
                    var user = db.Users.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.Password = Crypto.Hash(model.NewPassword);
                        user.ResetPasswordCode = Guid.NewGuid().ToString();
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        message = "New Password update successfully";
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                message = " Something invalid";
            }

            ViewBag.Message = message;
            return View(model);
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
        public ActionResult ChangePassword()
        {
            VLUTradingDBEntities db = new VLUTradingDBEntities();
            string ss = Session["EmailID"].ToString();
            var userdetail = db.Users.SingleOrDefault(x => x.EmailID == ss);
            return View(userdetail);
        }
        [HttpPost]
        public ActionResult ChangePassword(string currentpassword, string newpassword, string confirmnewpassword)
        {
             VLUTradingDBEntities db = new VLUTradingDBEntities();
                       int ss = (int)Session["ID"];
            var userdetail = db.Users.SingleOrDefault(x => x.Id == ss);
            if (Crypto.Hash(currentpassword) == userdetail.Password)
            {
                if (Crypto.Hash(confirmnewpassword) == Crypto.Hash(newpassword))
                {
                    if (ModelState.IsValid)
                    {
                        userdetail.Password = Crypto.Hash(newpassword);
                        userdetail.EmailID = userdetail.EmailID;
                        userdetail.Id = userdetail.Id;
                        userdetail.IsEmailVerified = userdetail.IsEmailVerified;
                        userdetail.FirstName = userdetail.FirstName;
                        userdetail.LastName = userdetail.LastName;
                        userdetail.ConfirmPassword = Crypto.Hash(newpassword);
                        userdetail.Role = userdetail.Role;
                        userdetail.Role_ID = userdetail.Role_ID;
                        userdetail.ResetPasswordCode = userdetail.ResetPasswordCode;
                        userdetail.ActivationCode = userdetail.ActivationCode;
                        db.SaveChanges();
                        TempData["changepassword"] = "Your password has been changed";
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("ChangePassword", "Account");

                }
                else
                {
                    ModelState.AddModelError("confirmerror", "ConfirmPassword is wrong");
                    return View(userdetail);
                }
            }
            else
            {
                ModelState.AddModelError("passworderror", "Password is wrong");
                return View(userdetail);
            }

        }
        public ActionResult ViewProfile()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }
  
    }
}
