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
            var result = controller.ResetPassword("c2158dfe-f95f-4076-88ba-5f04b646d099") as ViewResult;
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
                ResetCode = "c2158dfe-f95f-4076-88ba-5f04b646d099",
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
