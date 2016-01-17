using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CodingTest.DAL.DataContexts;
using CodingTest.DAL.Entities;
using CodingTest.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodingTest.WebAPI.Tests.Repositories
{
    [TestClass]
    public class ItemRepositoryTest
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
        public void TestPurchaseItem()
        {
            var itemRepo = new ItemRepository(mockContext.Object);

            var firstOrDefault = itemRepo.GetItems().FirstOrDefault(x => x.ItemId == 0);
            Assert.IsTrue(firstOrDefault != null && firstOrDefault.QtyInStock == 10);

            itemRepo.PurchaseItem(0);

            firstOrDefault = itemRepo.GetItems().FirstOrDefault(x => x.ItemId == 0);
            Assert.IsTrue(firstOrDefault != null && firstOrDefault.QtyInStock == 9);
        }

        [TestMethod]
        public void TestGetItems()
        {
            var itemRepo = new ItemRepository(mockContext.Object);
        }
    }
}