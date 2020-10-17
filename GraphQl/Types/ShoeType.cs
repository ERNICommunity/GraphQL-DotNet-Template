using productsWebapi.Products;
using productsWebapi.Repositories;
namespace productsWebapi.GraphQl.Types
{
    public sealed class ShoeType : ProductType<Shoe>
    {
        public ShoeType(IRepository<Review> reviews)
            :base(reviews, nameof(Shoe))
        {
            Field(s => s.Size).Description("The shoes size in EU units.");
        }
    }
}