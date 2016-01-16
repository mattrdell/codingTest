using System.Linq;
using CodingTest.WebAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingTest.WebAPI.Tests.Controllers
{
    [TestClass]
    public class ItemsControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange

            //TODO insert a mock ctx here with test values
            var controller = new ItemsController();

            // Act
            var result = controller.GetItems();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }
    }
}