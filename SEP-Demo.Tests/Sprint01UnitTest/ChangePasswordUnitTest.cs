using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEP_Demo.Controllers;
using System.Web.Mvc;
using SEP_Demo.Models;
using Moq;
using System.Web.Routing;
using System.Threading.Tasks;
using log4net;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Web;

namespace SEP_Demo.Tests
{
    [TestClass]
    public class ChangePasswordUnitTest
    {
        [TestMethod]
        public void View_ChangePassword()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.AccountController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            // Act
            context.SetupGet(x => x.Session["EmailID"]).Returns("thuannguyen44@vanlanguni.vn");
            ViewResult result = controller.ChangePassword() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void ChangePassword_Success()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.AccountController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            // Act
            context.SetupGet(x => x.Session["ID"]).Returns(6);
            var redirectToRouteResult = controller.ChangePassword("456456456", "456456", "456456") as RedirectToRouteResult;
            //  Assert
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Home", redirectToRouteResult.RouteValues["controller"]);
        }
        [TestMethod]
        public void ChangePassword_withInvalidOldPassword()
        {
            //arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.AccountController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            // Act
            context.SetupGet(x => x.Session["ID"]).Returns(6);
            var redirectToRouteResult = controller.ChangePassword("123123", "456456", "456456") as ViewResult;
            //  Assert
            Assert.AreEqual("", redirectToRouteResult.ViewName);
        }
        [TestMethod]
        public void ChangePassword_withInvalidConfirmPassword()
        {
            VLUTradingDBEntities db = new VLUTradingDBEntities();
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.AccountController();
            var user = new User();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            // Act
            context.SetupGet(x => x.Session["ID"]).Returns(6);
            var redirectToRouteResult = controller.ChangePassword("456456", "222222", "111111") as ViewResult;

            //  Assert
            Assert.AreEqual("",redirectToRouteResult.ViewName);
           
            
            
        }
    }
}
