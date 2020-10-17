using System.Threading.Tasks;
using productsWebapi.Products;
namespace productsWebapi.Repositories
{
    public interface IMutableRepository<TItem> : IRepository<TItem>
        where TItem: IIdentifiable
    {
         Task<TItem> Add(TItem item);
    }
}