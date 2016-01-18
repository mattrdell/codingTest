using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
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
        public void PurchaseItem()
        {
            //Arrange
            var itemRepo = new ItemRepository(mockContext.Object);

            //Act/Assert
            Assert.IsTrue(itemRepo.PurchaseItem(0));
            Assert.IsFalse(itemRepo.PurchaseItem(1));
            Assert.IsTrue(itemRepo.PurchaseItem(2));
            Assert.IsTrue(itemRepo.PurchaseItem(2));
            Assert.IsTrue(itemRepo.PurchaseItem(2));
        }

        [TestMethod]
        public void PurchaseAndGet()
        {
            //Arrange
            var itemRepo = new ItemRepository(mockContext.Object);

            //Act/Assert
            Assert.IsTrue(itemRepo.PurchaseItem(0));
            Assert.IsFalse(itemRepo.PurchaseItem(1));
            Assert.IsTrue(itemRepo.PurchaseItem(2));
            Assert.IsTrue(itemRepo.PurchaseItem(2));
            Assert.IsTrue(itemRepo.PurchaseItem(2));

            var items = itemRepo.GetItems();
            var item0 = items.FirstOrDefault(x => x.ItemId == 0);
            var item1 = items.FirstOrDefault(x => x.ItemId == 1);
            var item2 = items.FirstOrDefault(x => x.ItemId == 2);

            Assert.IsNotNull(item0);
            Assert.AreEqual(item0.QtyInStock, 9);
            Assert.IsNotNull(item1);
            Assert.AreEqual(item1.QtyInStock, 0);
            Assert.IsNotNull(item2);
            Assert.AreEqual(item2.QtyInStock, 27);
        }

        [TestMethod]
        public void GetItems()
        {
            var itemRepo = new ItemRepository(mockContext.Object);

            var items = itemRepo.GetItems();
            var item0 = items.FirstOrDefault(x => x.ItemId == 0);
            var item1 = items.FirstOrDefault(x => x.ItemId == 1);
            var item2 = items.FirstOrDefault(x => x.ItemId == 2);

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
    }
}