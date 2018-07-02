using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEP_Demo.Controllers;
using System.Web.Mvc;
using SEP_Demo.Models;
using Moq;
using System.Web.Routing;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Net.Http;


namespace SEP_Demo.Tests
{
    [TestClass]
    public class ForgetPasswordUnitTest
    {
        //Test ForgetPassword
        [TestMethod]
        public void View_ForgetPassword()
        {
            // Arrange
            AccountController controller = new AccountController();
            // Act
            ViewResult result = controller.ForgetPassword() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void ForgetPassword_withInvalidEmail()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.AccountController();
            var EmailID = "thuannguyen@vanlanguni.vn";

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            // Act
            var result = controller.ForgetPassword(EmailID) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Account not found", result.ViewBag.message);
        }
        [TestMethod]
        public void CheckCode_ResetPasswordCode()
        {
            // Arrange
            AccountController controller = new AccountController();
            // Act
            var result = controller.ResetPassword("578bb6adc-21b1-40f5-90f1-080c183ef108") as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void ResetPassword_Success()
        {
            // Arrange
            AccountController controller = new AccountController();
            var model = new ResetPasswordModel
            {
                ResetCode = "14dee28e-42c1-4e8f-a616-47bd6c721258",
                NewPassword = "123123",
                ConfirmPassword = "123123"
            };
            // Act
            var RouteResult = controller.ResetPassword(model) as RedirectToRouteResult;
            // Assert
            Assert.AreEqual("Index", RouteResult.RouteValues["Action"]);
            Assert.AreEqual("Home", RouteResult.RouteValues["controller"]);
        }
    }
}
