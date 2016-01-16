using System.Data.Entity;
using CodingTest.DAL.Entities;

namespace CodingTest.DAL.DataContexts
{
    public class ItemDbCtx : DbContext
    {
        public ItemDbCtx()
            : base("name=ItemDbCtx")
        {
        }

        public virtual DbSet<Item> Items { get; set; }
    }
}