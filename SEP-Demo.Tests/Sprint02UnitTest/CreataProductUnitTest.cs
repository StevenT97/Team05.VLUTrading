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
    public class CreataProductUnitTest
    {
        [TestMethod]
        public void View_Product()
        {
            // Arrange
            ProductController controller = new ProductController();
            // Act
            ViewResult result = controller.Product() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void View_CreateProduct()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.ProductController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            // Act
            PartialViewResult result = controller.Create() as PartialViewResult;
            // Assert
            Assert.AreEqual("CreatePartial", result.ViewName);
        }
        [TestMethod]
        public void CreateProduct_Success()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.ProductController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var serverMock = new Mock<HttpServerUtilityBase>();
            serverMock.Setup(x => x.MapPath("~/Images/ProductAvatar/")).Returns(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar");

            context.Setup(x => x.Server).Returns(serverMock.Object);
            
            var file1Mock = new Mock<HttpPostedFileBase>();
            file1Mock.Setup(x => x.FileName).Returns("1.jpg");

            var image = new[] { file1Mock.Object };
            var image1 = file1Mock.Object;
            context.SetupGet(x => x.Session["ID"]).Returns(6);
            var product = new Product
            {
                Name = "deplao1",
                CategoryID = 4,
                Description = "son ",
                Image_Detail = image,
                Image = image1
            };

            // act
            var actual = controller.Create(product, 12) as RedirectToRouteResult;
            file1Mock.Verify(x => x.SaveAs(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar\1.jpg"));
            // Assert
            Assert.AreEqual("ViewProfile", actual.RouteValues["Action"]);
            Assert.AreEqual("Account", actual.RouteValues["controller"]);
        }
        [TestMethod]
        public void CreateProduct_Success_withTowPicture()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.ProductController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var serverMock = new Mock<HttpServerUtilityBase>();
            serverMock.Setup(x => x.MapPath("~/Images/ProductAvatar/")).Returns(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar");

            context.Setup(x => x.Server).Returns(serverMock.Object);

            var file1Mock = new Mock<HttpPostedFileBase>();
            var file2Mock = new Mock<HttpPostedFileBase>();
            file1Mock.Setup(x => x.FileName).Returns("1.jpg");
            file2Mock.Setup(x => x.FileName).Returns("2.jpg");

            var image = new[] { file1Mock.Object, file2Mock.Object };
            var image1 = file1Mock.Object;
            context.SetupGet(x => x.Session["ID"]).Returns(6);
            var product = new Product
            {
                Name = "deplao1",
                CategoryID = 4,
                Description = "son ",
                Image_Detail = image,
                Image = image1
            };

            // act
            var actual = controller.Create(product, 12) as RedirectToRouteResult;
            file1Mock.Verify(x => x.SaveAs(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar\1.jpg"));
            // Assert
            Assert.AreEqual("ViewProfile", actual.RouteValues["Action"]);
            Assert.AreEqual("Account", actual.RouteValues["controller"]);
        }
        [TestMethod]
        public void CreateProduct_Success_withThreePicture()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.ProductController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var serverMock = new Mock<HttpServerUtilityBase>();
            serverMock.Setup(x => x.MapPath("~/Images/ProductAvatar/")).Returns(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar");

            context.Setup(x => x.Server).Returns(serverMock.Object);

            var file1Mock = new Mock<HttpPostedFileBase>();
            var file2Mock = new Mock<HttpPostedFileBase>();
            var file3Mock = new Mock<HttpPostedFileBase>();
            file1Mock.Setup(x => x.FileName).Returns("1.jpg");
            file2Mock.Setup(x => x.FileName).Returns("2.jpg");
            file3Mock.Setup(x => x.FileName).Returns("3.jpg");

            var image = new[] { file1Mock.Object, file2Mock.Object, file3Mock.Object };
            var image1 = file1Mock.Object;
            context.SetupGet(x => x.Session["ID"]).Returns(6);
            var product = new Product
            {
                Name = "deplao1",
                CategoryID = 4,
                Description = "son ",
                Image_Detail = image,
                Image = image1
            };

            // act
            var actual = controller.Create(product, 12) as RedirectToRouteResult;
            file1Mock.Verify(x => x.SaveAs(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar\1.jpg"));
            // Assert
            Assert.AreEqual("ViewProfile", actual.RouteValues["Action"]);
            Assert.AreEqual("Account", actual.RouteValues["controller"]);
        }
        [TestMethod]
        public void CreateProduct_Success_withFuorPicture()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.ProductController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var serverMock = new Mock<HttpServerUtilityBase>();
            serverMock.Setup(x => x.MapPath("~/Images/ProductAvatar/")).Returns(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar");

            context.Setup(x => x.Server).Returns(serverMock.Object);

            var file1Mock = new Mock<HttpPostedFileBase>();
            var file2Mock = new Mock<HttpPostedFileBase>();
            var file3Mock = new Mock<HttpPostedFileBase>();
            var file4Mock = new Mock<HttpPostedFileBase>();

            file1Mock.Setup(x => x.FileName).Returns("1.jpg");
            file2Mock.Setup(x => x.FileName).Returns("2.jpg");
            file3Mock.Setup(x => x.FileName).Returns("3.jpg");
            file4Mock.Setup(x => x.FileName).Returns("4.jpg");

            var image = new[] { file1Mock.Object, file2Mock.Object, file3Mock.Object, file4Mock.Object };
            var image1 = file1Mock.Object;
            context.SetupGet(x => x.Session["ID"]).Returns(6);
            var product = new Product
            {
                Name = "deplao1",
                CategoryID = 4,
                Description = "son ",
                Image_Detail = image,
                Image = image1
            };

            // act
            var actual = controller.Create(product, 12) as RedirectToRouteResult;
            file1Mock.Verify(x => x.SaveAs(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar\1.jpg"));
            // Assert
            Assert.AreEqual("ViewProfile", actual.RouteValues["Action"]);
            Assert.AreEqual("Account", actual.RouteValues["controller"]);
        }
        [TestMethod]
        public void CreateProduct_Success_withFivePicture()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.ProductController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var serverMock = new Mock<HttpServerUtilityBase>();
            serverMock.Setup(x => x.MapPath("~/Images/ProductAvatar/")).Returns(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar");

            context.Setup(x => x.Server).Returns(serverMock.Object);

            var file1Mock = new Mock<HttpPostedFileBase>();
            var file2Mock = new Mock<HttpPostedFileBase>();
            var file3Mock = new Mock<HttpPostedFileBase>();
            var file4Mock = new Mock<HttpPostedFileBase>();
            var file5Mock = new Mock<HttpPostedFileBase>();

            file1Mock.Setup(x => x.FileName).Returns("1.jpg");
            file2Mock.Setup(x => x.FileName).Returns("2.jpg");
            file3Mock.Setup(x => x.FileName).Returns("3.jpg");
            file4Mock.Setup(x => x.FileName).Returns("4.jpg");
            file5Mock.Setup(x => x.FileName).Returns("4.jpg");

            var image = new[] { file1Mock.Object, file2Mock.Object, file3Mock.Object, file4Mock.Object, file5Mock.Object };
            var image1 = file1Mock.Object;
            context.SetupGet(x => x.Session["ID"]).Returns(6);
            var product = new Product
            {
                Name = "deplao1",
                CategoryID = 4,
                Description = "son ",
                Image_Detail = image,
                Image = image1
            };

            // act
            var actual = controller.Create(product, 12) as RedirectToRouteResult;
            file1Mock.Verify(x => x.SaveAs(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar\1.jpg"));
            // Assert
            Assert.AreEqual("ViewProfile", actual.RouteValues["Action"]);
            Assert.AreEqual("Account", actual.RouteValues["controller"]);
        }
    }
}
