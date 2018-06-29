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
        public void CreateProduct_Success()
        {
            // Arrange


            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.ProductController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);



            //var httpContextMock = new Mock<HttpContextBase>();
            var serverMock = new Mock<HttpServerUtilityBase>();
            serverMock.Setup(x => x.MapPath("~/ProductAvatar")).Returns(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar");

            context.Setup(x => x.Server).Returns(serverMock.Object);
            //var sut = new ProductController();
            //controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);

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

            // assert
            file1Mock.Verify(x => x.SaveAs(@"C:\Users\ngocs\OneDrive\Máy tính\Trading280618\SEP-Demo\Images\ProductAvatar\1.jpg"));

            // Act
            // var redirectToRouteResult = controller.Create(product,12) as RedirectToRouteResult;
            // Assert
            Assert.AreEqual("ViewProfile", actual.RouteValues["Action"]);
            Assert.AreEqual("Account", actual.RouteValues["controller"]);
        }

    }
}
