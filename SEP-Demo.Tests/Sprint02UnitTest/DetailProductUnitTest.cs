using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEP_Demo.Controllers;
using System.Web.Mvc;
using SEP_Demo.Models;
using Moq;
using System.Web.Routing;
using System.Threading.Tasks;
using System.Web;
using System.Net;

namespace SEP_Demo.Tests.Sprint02UnitTest
{
    [TestClass]
    public class DetailProductUnitTest
    {
        [TestMethod]
        public void View_ProductDetail ()
        {
            // Arrange
            ProductController controller = new ProductController();
            // Act
            ViewResult result = controller.Detail(1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void ValidateProductDetail_Null_ID()
        {
            // Arrange
            ProductController controller = new ProductController();
            // Act
            ViewResult result = controller.Detail(4) as ViewResult;
            // Assert
            Assert.IsNull(result);
        }

    }
}
