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
            Subscription = resolver.Resolve<ReviewSupscription>();

            // This be ugliness!
            RegisterType<BookType>();
            RegisterType<FilmType>();
            RegisterType<ShoeType>();
        }
    }
}