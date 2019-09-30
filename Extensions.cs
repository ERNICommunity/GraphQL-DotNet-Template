using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using productsWebapi.Products;
using productsWebapi.Repositories;

namespace productsWebapi
{
    public static class Extensions
    {
        public static Review<TPoduct> Review<TPoduct>(this TPoduct product, String title, String text)
            where TPoduct: IProduct
        {
            return new Review<TPoduct>(product, title, text);
        }
        public static async Task<IEnumerable<TItem>> For<TItem>(this IRepository<TItem> repo, IIdentifiable relation)
            where TItem: IIdentifiable, IRelatable
        {
            // All() --> Badness!
            var items = await repo.All().ConfigureAwait(false);
            return items.Where(item => item.RelationId == relation.Id);
        }
        public static async Task<ILookup<Int32, TItem>> For<TItem>(this IRepository<TItem> repo, IEnumerable<IIdentifiable> relations)
            where TItem: IIdentifiable, IRelatable
        {
            // All() --> Badness!
            var items = await repo.All().ConfigureAwait(false);
            return (from item in items
                    join relation in relations on item.RelationId equals relation.Id
                    select item).ToLookup(item => item.RelationId);
        }
    }
}