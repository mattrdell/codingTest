using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using CodingTest.DAL.DataContexts;
using CodingTest.DAL.Entities;
using CodingTest.DAL.Repositories;

namespace CodingTest.WebAPI.Controllers
{
    [System.Web.Http.Authorize]
    [RequireHttps]
    public class ItemsController : ApiController
    {
        private readonly ItemDbCtx db;

        public ItemsController()
        {
            db = new ItemDbCtx();
        }

        public ItemsController(ItemDbCtx dbCtx)
        {
            db = dbCtx;
        }

        public IEnumerable<Item> GetItems()
        {
            var itemRepo = new ItemRepository(db);

            return itemRepo.GetItems();
        }

        public bool PurchaseItem(Item item)
        {
            var itemRepo = new ItemRepository(db);

            return itemRepo.PurchaseItem(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}