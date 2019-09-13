using GraphQL;
using GraphQL.Types;
using productsWebapi.GraphQl.Types;

namespace productsWebapi.GraphQl
{
    public sealed class ProductSchema: Schema
    {
        public ProductSchema(IDependencyResolver resolver): base(resolver){
            Query = resolver.Resolve<ProductQuery>();
            Mutation = resolver.Resolve<ProductMutation>();
            // This be uglyness!
            RegisterType<BookType>();
            RegisterType<FilmType>();
            RegisterType<ShoeType>();
        }
    }
}