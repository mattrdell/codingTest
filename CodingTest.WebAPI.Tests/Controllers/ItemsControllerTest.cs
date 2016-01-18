using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodingTest.DAL.DataContexts;
using CodingTest.DAL.Entities;
using CodingTest.WebAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodingTest.WebAPI.Tests.Controllers
{
    [TestClass]
    public class ItemsControllerTest
    {
        private Mock<ItemDbCtx> mockContext;

        [TestInitialize]
        public void Setup()
        {
            var data = new List<Item>
            {
                new Item {Name = "Item1", Description = "Item Num 1", ItemId = 0, Price = 5, QtyInStock = 10},
                new Item {Name = "Item2", Description = "Item Num 2", ItemId = 1, Price = 15, QtyInStock = 0},
                new Item {Name = "Item3", Description = "Item Num 3", ItemId = 2, Price = 25, QtyInStock = 30}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Item>>();
            mockSet.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext = new Mock<ItemDbCtx>();
            mockContext.Setup(m => m.Items).Returns(mockSet.Object);
        }

        [TestMethod]
        public void Get()
        {
            var controller = new ItemsController(mockContext.Object);

            var items = controller.Get();
            var enumerable = items as Item[] ?? items.ToArray();
            var item0 = enumerable.FirstOrDefault(x => x.ItemId == 0);
            var item1 = enumerable.FirstOrDefault(x => x.ItemId == 1);
            var item2 = enumerable.FirstOrDefault(x => x.ItemId == 2);

            Assert.IsNotNull(item0);
            Assert.AreEqual(item0.QtyInStock, 10);
            Assert.AreEqual(item0.Name, "Item1");
            Assert.AreEqual(item0.Description, "Item Num 1");
            Assert.AreEqual(item0.Price, 5);

            Assert.IsNotNull(item1);
            Assert.AreEqual(item1.QtyInStock, 0);
            Assert.AreEqual(item1.Name, "Item2");
            Assert.AreEqual(item1.Description, "Item Num 2");
            Assert.AreEqual(item1.Price, 15);

            Assert.IsNotNull(item2);
            Assert.AreEqual(item2.QtyInStock, 30);
            Assert.AreEqual(item2.Name, "Item3");
            Assert.AreEqual(item2.Description, "Item Num 3");
            Assert.AreEqual(item2.Price, 25);
        }

        [TestMethod]
        public void Post()
        {
            //Arrange
            var controller = new ItemsController(mockContext.Object);
            var items = controller.Get();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var enumerable = items as Item[] ?? items.ToArray();
            var item0 = enumerable.FirstOrDefault(x => x.ItemId == 0);
            var item1 = enumerable.FirstOrDefault(x => x.ItemId == 1);

            //Act
            var result1 = controller.Post(item0);
            var result2 = controller.Post(item1);

            //Assert
            Assert.AreEqual(result1.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(result2.StatusCode, HttpStatusCode.BadRequest);
        }
    }
}