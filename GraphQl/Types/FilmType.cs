using productsWebapi.Products;
using productsWebapi.Repositories;
namespace productsWebapi.GraphQl.Types
{
    public sealed class FilmType : ProductType<Film>
    {
        public FilmType(IRepository<Review> reviews)
            :base(reviews, nameof(Film))
        {
            Field(f => f.Director).Description("The films director.");
        }
    }
}