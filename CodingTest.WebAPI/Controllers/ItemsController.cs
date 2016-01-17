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

        //[System.Web.Http.Authorize]
        public bool PurchaseItem(int itemId)
        {
            var itemRepo = new ItemRepository(db);

            return itemRepo.PurchaseItem(itemId);
        }

        [Authorize]
        public HttpResponseMessage Post(Item item)
        {
            var userName = this.RequestContext.Principal.Identity.Name;
            var itemRepo = new ItemRepository(db);
            if (itemRepo.PurchaseItem(item.ItemId))
                item.QtyInStock -= 1;
            var response = Request.CreateResponse(HttpStatusCode.OK, item);
            //string url = Url.Link("DefaultApi", new { student.Id });
            //response.Headers.Location = new Uri(url);

            return response;
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