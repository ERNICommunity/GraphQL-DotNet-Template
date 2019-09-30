using System;
using GraphQL.Types;
using productsWebapi.Products;
using productsWebapi.Repositories;

namespace productsWebapi.GraphQl.Types
{
    public abstract class ProductType<TProduct>: ObjectGraphType<TProduct>
        where TProduct: IProduct
    {
        public ProductType(IRepository<Review> reviews, String name){
            Name = name;
            Field(p => p.Name).Description("The products name");
            Field(p => p.Id).Description("The products id");
            Field(p => p.Stock).Description("The number of products in stock");
            Field(p => p.Type).Description("The type of product");
            Field<ListGraphType<ReviewType>>("reviews", resolve: context => reviews.For(context.Source));
            Interface<ProductInterface>();
        }
    }
}