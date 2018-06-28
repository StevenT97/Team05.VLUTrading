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
    public class LogOutUnitTest
    {
        //Test LogOut
        [TestMethod]
        public void LogOut()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.AccountController();
            var EmailID = "thuannguyen44@vanlanguni.vn";
            var password = "456456456";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var redirectRoute = controller.Login(EmailID, password) as RedirectToRouteResult;
            var redirectToRouteResult = controller.LogOut() as RedirectToRouteResult;
            Assert.AreEqual("HomeIndex", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Home", redirectToRouteResult.RouteValues["controller"]);
        }
    }
}
