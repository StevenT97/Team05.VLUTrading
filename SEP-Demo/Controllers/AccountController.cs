using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEP_Demo.Models;
using System.Net.Mail;
using System.Net;
using System.Web.Security;

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

                //Save to database
                using (MyDataEntities db = new MyDataEntities())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }

                //Send Email to User
                SendVerificationLinkEmail(user.EmailID, user.ActivationCode.ToString());
                message = " Registration sucessfully done. Account activation link" +
                    " has been sent to your email : " +user.EmailID;
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
            using (MyDataEntities db = new MyDataEntities())
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

        //Login
        public ActionResult Login()
        {
            return View();
        }
        //Login Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login)
        {
            string message = "";
            using(MyDataEntities db = new MyDataEntities())
            {
                var v = db.Users.Where(a => a.EmailID == login.EmailID).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(login.Password),v.Password) == 0)
                    {
                        //int timeout = login.RememberMe ? 525600 : 20; //525600 min = 1 year
                        //var ticket = new FormsAuthenticationTicket(login.EmailID,login.RememberMe,timeout);
                        //string encrypted = FormsAuthentication.Encrypt(ticket);
                        //var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        //cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        //cookie.HttpOnly = true;
                        //Response.Cookies.Add(cookie);

                        //if (Url.IsLocalUrl(ReturnUrl))
                        //{
                        //    return Redirect(ReturnUrl);
                        //}
                        //else
                        //{
                        //    return RedirectToAction("Index", "Home");
                        //}
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        message = "Invalid credential provied";
                    }
                }
                else
                {
                    message = "Invalid credential provied";
                }
            }

            ViewBag.Message = message;
            return View(login);
        }
        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult SignOut()
        {
            //FormsAuthentication.SignOut();

            return RedirectToAction("SignIn", "Account");
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

            using (MyDataEntities db = new MyDataEntities())
            {
                var account = db.Users.Where(a => a.EmailID == EmailID).FirstOrDefault();
                if (account != null)
                {
                    //Send Email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.EmailID,resetCode, "ResetPassword");
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
            using(MyDataEntities db = new MyDataEntities())
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
                using(MyDataEntities db = new MyDataEntities())
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
            using (MyDataEntities db = new MyDataEntities())
            {
                var v = db.Users.Where(u => u.EmailID == emailID).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/Account/"+emailFor+"/" + activationCode;
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
            else if(emailFor == "ResetPassword")
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
    }
}