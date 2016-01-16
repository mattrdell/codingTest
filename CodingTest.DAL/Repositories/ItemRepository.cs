using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using CodingTest.DAL.DataContexts;
using CodingTest.DAL.Entities;

namespace CodingTest.DAL.Repositories
{
    public class ItemRepository
    {
        private readonly ItemDbCtx db;

        public ItemRepository(ItemDbCtx itemDbCtx)
        {
            db = itemDbCtx;
        }

        public IQueryable<Item> GetItems()
        {
            return db.Items;
        }

        public bool PurchaseItem(Item item)
        {
            db.Entry(item).State = EntityState.Deleted;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(item.ItemId))
                {
                    return false;
                }
                throw;
            }

            return true;
        }

        private bool ItemExists(int id)
        {
            return db.Items.Count(e => e.ItemId == id) > 0;
        }
    }
}