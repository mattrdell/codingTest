using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodingTest.WebAPI;
using CodingTest.WebAPI.Controllers;

namespace CodingTest.WebAPI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("The Gilded Rose Online Store", result.ViewBag.Title);
        }
    }
}
