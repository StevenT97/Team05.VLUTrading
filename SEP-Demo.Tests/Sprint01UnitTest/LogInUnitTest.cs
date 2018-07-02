using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEP_Demo.Controllers;
using System.Web.Mvc;
using SEP_Demo.Models;
using Moq;
using System.Web.Routing;
using System.Threading.Tasks;
using System.Web;

namespace SEP_Demo.Tests
{

    [TestClass]
    public class LogInUnitTest
    {
        //Test LogIn
        [TestMethod]
        public void View_LogIn()
        {
            // Arrange
            AccountController controller = new AccountController();
            // Act
            ViewResult result = controller.Login() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Login_ValidEmailIDPassword()
        {

            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.AccountController();
            var EmailID = "sonnguyen66@vanlanguni.vn";
            var password = "123123";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var redirectRoute = controller.Login(EmailID, password) as RedirectToRouteResult;
            //
            Assert.IsNotNull(redirectRoute);
            Assert.AreEqual("HomeIndex", redirectRoute.RouteValues["action"]);
            Assert.AreEqual("Home", redirectRoute.RouteValues["controller"]);
        }
        [TestMethod]
        public void Login_InValidPassword()
        {
            //Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.AccountController();
            var EmailID = "sonnguyen66@vanlanguni.vn";
            var password = "1234";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            //act
            var redirectRoute = controller.Login(EmailID, password) as RedirectToRouteResult;
            //Assert
            Assert.IsNotNull(redirectRoute);
            Assert.AreEqual("LogIn", redirectRoute.RouteValues["action"]);
            Assert.AreEqual("Account", redirectRoute.RouteValues["controller"]);
        }
        [TestMethod]
        public void LogIn_InvalidEmailID()
        {
            //Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext(); 
            var controller = new Controllers.AccountController();
            var EmailID = "sonnguyen@vanlanguni.vn";
            var password = "456456";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            //act
            var redirectRoute = controller.Login(EmailID, password) as ViewResult;
            //Assert
            Assert.AreEqual("* Account Invalid", redirectRoute.ViewBag.Message);
        }
    }
}
