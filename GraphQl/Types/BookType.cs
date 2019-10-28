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
            // Check whether these comments appear in the DOCS
            // Also, check whether these docs are compliant with OpenAPI
            Field(b => b.Author).Description("The books author.");
        }
    }
}