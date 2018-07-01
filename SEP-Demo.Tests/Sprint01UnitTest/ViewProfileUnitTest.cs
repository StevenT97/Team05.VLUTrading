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

namespace SEP_Demo.Tests.Sprint01UnitTest
{
    [TestClass]
    public class ViewProfileUnitTest
    {
        [TestMethod]
        public void ViewProfile()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.AccountController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            context.SetupGet(x => x.Session["ID"]).Returns(8);
            // Act
            ViewResult result = controller.ViewProfile() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void ViewProfile_withNullUser()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.AccountController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
          
            // Act
            var redirectToRouteResult = controller.ViewProfile() as RedirectToRouteResult;
            // Assert
            Assert.AreEqual("Login", redirectToRouteResult.RouteValues["Action"]);
        }
    }
}
