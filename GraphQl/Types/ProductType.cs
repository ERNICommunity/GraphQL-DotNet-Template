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
            // Alexeis agrees that this constructor stuff is not nice! 
            Name = name;
            ProductInterface.Register<TProduct>(this, context => reviews.For(context.Source));
            Interface<ProductInterface>();
        }
    }
}