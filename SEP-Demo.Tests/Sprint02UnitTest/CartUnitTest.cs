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

namespace SEP_Demo.Tests.Sprint02UnitTest
{
    [TestClass]
   public class CartUnitTest
    {
        [TestMethod]
        public void View_Cart()
        {
            // Arrange
            ProductController controller = new ProductController();
            // Act
            PartialViewResult result = controller.Create() as PartialViewResult;
            // Assert
            Assert.AreEqual("CreatePartial",result.ViewName);
        }
        [TestMethod]
        public void ViewCart_withNullUser()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.ProductController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            // Act
            var redirectToRouteResult = controller.Cart() as RedirectToRouteResult;
            // Assert
            Assert.AreEqual("Login", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Account", redirectToRouteResult.RouteValues["controller"]);
        }
        [TestMethod]
        public void CartOrder_Success()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.ProductController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
     
            var lisst = new OrdersItem
            {
                name = "sdf",
                s = "fsaf",
                username = "dsf",
                usertrade = 1,
                price = 1232,
                qty = 36,
                id = 36
                
            };
          
            var cart1 = JsonConvert.SerializeObject(new[] { lisst });
            context.SetupGet(x => x.Session["ID"]).Returns(6);
            var redirectToRouteResult = controller.Cart(cart1) as RedirectToRouteResult;
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Home", redirectToRouteResult.RouteValues["controller"]);
        }
        [TestMethod]
        public void CartOrder_invalidInput()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.ProductController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var lisst = new OrdersItem
            {
               

            };

            var cart1 = JsonConvert.SerializeObject(new[] { lisst });
            context.SetupGet(x => x.Session["ID"]).Returns(6);
            var redirectToRouteResult = controller.Cart(cart1) as RedirectToRouteResult;
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Home", redirectToRouteResult.RouteValues["controller"]);
        }
    }
}
