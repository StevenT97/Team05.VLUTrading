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

namespace SEP_Demo.Tests.Sprint02UnitTest
{
    [TestClass]
    public class DeleteProductUnitTest
    {
        [TestMethod]
        public void Delete_Success()
        {
            // Arrange
            ProductController controller = new ProductController();
            // Act
            var redirectToRouteResult = controller.Delete(51) as RedirectToRouteResult;
            // Assert
            Assert.AreEqual("ViewProfile", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Account",redirectToRouteResult.RouteValues["controller"]);
        }
        [TestMethod]
        public void Delete_with_Null_Product_ID()
        {
            // Arrange
            ProductController controller = new ProductController();
            // Act
            ViewResult result = controller.Delete(4) as ViewResult;
            // Assert
            Assert.IsNull(result);
        }

    }
}
