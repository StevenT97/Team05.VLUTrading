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
    public class AdminControllerUnitTest
    {
        [TestMethod]
        public void View_ListUser()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Admin.Controllers.AdminsController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            context.SetupGet(x => x.Session["ID"]).Returns(6);
            //act
            ViewResult result = controller.Index() as ViewResult;
            //assert
            Assert.AreEqual("", result.ViewName);
            //Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            //Assert.AreEqual("Home", redirectToRouteResult.RouteValues["controller"]);
        }
        [TestMethod]
        public void View_ListUser_withNull_UserID()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Admin.Controllers.AdminsController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            //act
            var redirectToRouteResult = controller.Index() as RedirectToRouteResult;
            //assert
            Assert.AreEqual("Login", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Account", redirectToRouteResult.RouteValues["controller"]);
        }
        [TestMethod]
        public void View_Dashboard_with_RoleID_1()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Admin.Controllers.AdminsController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            context.SetupGet(x => x.Session["RoleID"]).Returns(1);
            //act
            var redirectToRouteResult = controller.Dashboard() as RedirectToRouteResult;
            //assert
            Assert.AreEqual("Login", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Account", redirectToRouteResult.RouteValues["controller"]);
        }
        [TestMethod]
        public void View_Dashboard_with_RoleID_2()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Admin.Controllers.AdminsController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            context.SetupGet(x => x.Session["RoleID"]).Returns(2);
            //act
            ViewResult result = controller.Dashboard() as ViewResult;
            //assert
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void View_Dashboard_with_RoleID_3()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Admin.Controllers.AdminsController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            context.SetupGet(x => x.Session["RoleID"]).Returns(3);
            //act
            ViewResult result = controller.Dashboard() as ViewResult;
            //assert
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void View_ListProduct()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Admin.Controllers.AdminsController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            context.SetupGet(x => x.Session["RoleID"]).Returns(2);
            //act
            ViewResult result = controller.Listproduct() as ViewResult;
            //assert
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void View_ListProduct_withInvaliedRoleID()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Admin.Controllers.AdminsController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            context.SetupGet(x => x.Session["RoleID"]).Returns(1);
            //act
            var redirectToRouteResult = controller.Listproduct() as RedirectToRouteResult;
            //assert
            Assert.AreEqual("Login", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Account", redirectToRouteResult.RouteValues["controller"]);
        }
        [TestMethod]
        public void Update()
        {
            // Arrange
            AdminsController controller = new AdminsController();
            // Act
            ViewResult result = controller.Update() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
    }
}
