using System;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using productsWebapi.GraphQl.Types;

namespace productsWebapi.GraphQl
{
    public sealed class ProductSchema: Schema
    {
        public ProductSchema(IServiceProvider resolver): base(resolver){
            Query = resolver.GetService<ProductQuery>();
            Mutation = resolver.GetService<ProductMutation>();
            Subscription = resolver.GetService<ReviewSupscription>();

            // This be ugliness!
            RegisterType<BookType>();
            RegisterType<FilmType>();
            RegisterType<ShoeType>();
        }
    }
}