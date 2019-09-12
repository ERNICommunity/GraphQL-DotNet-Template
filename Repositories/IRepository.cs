using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using productsWebapi.Products;

namespace productsWebapi.Repositories
{
    public interface IRepository<TItem>: IEnumerable<TItem>
        where TItem: IIdentifiable
    {
         Task<TItem> Find(Guid id);
         Task<IEnumerable<TItem>> All();
    }
}