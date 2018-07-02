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
using Newtonsoft.Json;
using SEP_Demo.Admin.Controllers;

namespace SEP_Demo.Tests.Sprint02UnitTest
{
    [TestClass]
    public class HistoryUnitTes
    {
        [TestMethod]
        public void View_History()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.ProductController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            context.SetupGet(x => x.Session["ID"]).Returns(6);
            //act
            ViewResult result = controller.History() as ViewResult;
            //assert
            Assert.AreEqual("",result.ViewName);
        }
        [TestMethod]
        public void View_HistorywithNullUserID()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.ProductController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
           // context.SetupGet(x => x.Session["ID"]).Returns(6);
            //act
            var result = controller.History() as RedirectToRouteResult;
            //assert
            Assert.AreEqual("Login", result.RouteValues["Action"]);
            Assert.AreEqual("Account", result.RouteValues["controller"]);
        }
        [TestMethod]
        public void View_HistoryTrading()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.ProductController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            context.SetupGet(x => x.Session["ID"]).Returns(6);
            //act
            ViewResult result = controller.HistoryTrading() as ViewResult;
            //assert
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void View_HistoryTradingwithNullUserID()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.ProductController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            // context.SetupGet(x => x.Session["ID"]).Returns(6);
            //act
            var result = controller.HistoryTrading() as RedirectToRouteResult;
            //assert
            Assert.AreEqual("Login", result.RouteValues["Action"]);
            Assert.AreEqual("Account", result.RouteValues["controller"]);
        }
    }
}
