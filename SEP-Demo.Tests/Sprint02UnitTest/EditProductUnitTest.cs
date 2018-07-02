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
using SEP_Demo.Admin.Controllers;

namespace SEP_Demo.Tests.Sprint02UnitTest
{
    [TestClass]
    public class EditProductUnitTest
    {
        [TestMethod]
        public void View_EditProduct()
        {

            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.ProductController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            // Act
            PartialViewResult result = controller.Edit(2) as PartialViewResult;
            // Assert
            Assert.AreEqual("EditPartial", result.ViewName);
        }
        [TestMethod]
        public void EditProduct_withNull_productID()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.ProductController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var serverMock = new Mock<HttpServerUtilityBase>();
            serverMock.Setup(x => x.MapPath("~/Images/ProductAvatar")).Returns(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar");

            context.Setup(x => x.Server).Returns(serverMock.Object);

            var file1Mock = new Mock<HttpPostedFileBase>();
            file1Mock.Setup(x => x.FileName).Returns("2.jpg");

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
            var actual = controller.Edit(product, 12) as ViewResult;
            // Assert
            Assert.IsNull(actual);
 
        }
        [TestMethod]
        public void EditProduct_Success()
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
                ID =2,
                Name = "deplao20101",
                CategoryID = 4,
                Description = "son ",
                Image_Detail = image,
                Image = image1
            };

            // act
            var actual = controller.Edit(product, 12) as RedirectToRouteResult;
            file1Mock.Verify(x => x.SaveAs(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar\1.jpg"));
            // Assert
            Assert.AreEqual("ViewProfile", actual.RouteValues["Action"]);
            Assert.AreEqual("Account", actual.RouteValues["controller"]);
        }
        [TestMethod]
        public void EditProduct_Success_withTowPicture()
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
                ID = 2,
                Name = "deplao1111111",
                CategoryID = 4,
                Description = "thien ",
                Image_Detail = image,
                Image = image1
            };

            // act
            var actual = controller.Edit(product, 12) as RedirectToRouteResult;
            file1Mock.Verify(x => x.SaveAs(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar\1.jpg"));
            // Assert
            Assert.AreEqual("ViewProfile", actual.RouteValues["Action"]);
            Assert.AreEqual("Account", actual.RouteValues["controller"]);
        }
        [TestMethod]
        public void EditProduct_Success_withThreePicture()
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
                ID = 2,
                Name = "deplao2222222",
                CategoryID = 4,
                Description = "son ",
                Image_Detail = image,
                Image = image1
            };

            // act
            var actual = controller.Edit(product, 12) as RedirectToRouteResult;
            file1Mock.Verify(x => x.SaveAs(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar\1.jpg"));
            // Assert
            Assert.AreEqual("ViewProfile", actual.RouteValues["Action"]);
            Assert.AreEqual("Account", actual.RouteValues["controller"]);
        }
        [TestMethod]
        public void EditProduct_Success_withFuorPicture()
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
                ID = 2,
                Name = "deplao33333333333",
                CategoryID = 4,
                Description = "son ",
                Image_Detail = image,
                Image = image1
            };

            // act
            var actual = controller.Edit(product, 12) as RedirectToRouteResult;
            file1Mock.Verify(x => x.SaveAs(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar\1.jpg"));
            // Assert
            Assert.AreEqual("ViewProfile", actual.RouteValues["Action"]);
            Assert.AreEqual("Account", actual.RouteValues["controller"]);
        }
        [TestMethod]
        public void EditProduct_Success_withFivePicture()
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
                ID =2,
                Name = "deplao4444444444",
                CategoryID = 4,
                Description = "son ",
                Image_Detail = image,
                Image = image1
            };

            // act
            var actual = controller.Edit(product, 12) as RedirectToRouteResult;
            file1Mock.Verify(x => x.SaveAs(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar\1.jpg"));
            // Assert
            Assert.AreEqual("ViewProfile", actual.RouteValues["Action"]);
            Assert.AreEqual("Account", actual.RouteValues["controller"]);
        }
    }
}
