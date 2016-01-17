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

        public bool PurchaseItem(int itemId)
        {
            var item = db.Items.FirstOrDefault(x => x.ItemId == itemId);

            if (item == null || item.QtyInStock <= 0)
            {
                return false;
            }

            item.QtyInStock -= 1;

            db.Entry(item).State = EntityState.Modified;

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