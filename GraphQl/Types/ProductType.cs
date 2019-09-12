using System;
using GraphQL.Types;
using productsWebapi.Products;
using productsWebapi.Repositories;

namespace productsWebapi.GraphQl.Types
{
    public sealed class ProductType: ObjectGraphType<IProduct>
    {
        public ProductType(IRepository<Review> reviews){
            Field(p => p.Name).Description("The products name");
            Field(p => p.Stock).Description("The number of products in stock");
            Field(p => p.Type).Description("The type of product");
            Field<ListGraphType<ReviewType>>("reviews", resolve: context => reviews.For(context.Source));
        }
    }
}