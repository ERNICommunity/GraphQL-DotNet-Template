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
            // Best solution at a somewhat clumsy graphql-dotnet requirement.
            ProductInterface.Register<TProduct>(this, context => reviews.For(context.Source));
            Interface<ProductInterface>();
        }
    }
}