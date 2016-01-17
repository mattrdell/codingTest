using System.Collections.Generic;
using System.Web.Http;
using CodingTest.DAL.DataContexts;
using CodingTest.DAL.Entities;
using CodingTest.DAL.Repositories;

namespace CodingTest.WebAPI.Controllers
{
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

        [System.Web.Http.Authorize]
        public bool PurchaseItem(int itemId)
        {
            var itemRepo = new ItemRepository(db);

            return itemRepo.PurchaseItem(itemId);
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