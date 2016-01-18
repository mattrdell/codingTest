using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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

        public IEnumerable<Item> Get()
        {
            var itemRepo = new ItemRepository(db);

            return itemRepo.GetItems();
        }

        [Authorize]
        public HttpResponseMessage Post(Item item)
        {
            var itemRepo = new ItemRepository(db);
            var result = itemRepo.PurchaseItem(item.ItemId);
            return result ? 
                Request.CreateResponse(HttpStatusCode.OK, item) : 
                Request.CreateResponse(HttpStatusCode.BadRequest);
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