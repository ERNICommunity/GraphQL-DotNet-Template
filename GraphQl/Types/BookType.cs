using System;
using productsWebapi.Products;
using productsWebapi.Repositories;
namespace productsWebapi.GraphQl.Types
{
    public sealed class BookType: ProductType<Book>
    {
        public BookType(IRepository<Review> reviews)
            :base(reviews, nameof(Book))
        {
            Field(b => b.Author).Description("The books author.");
        }
    }
}