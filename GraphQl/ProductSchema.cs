using GraphQL;
using GraphQL.Types;

namespace productsWebapi.GraphQl
{
    public sealed class ProductSchema: Schema
    {
        public ProductSchema(IDependencyResolver resolver): base(resolver){
            Query = resolver.Resolve<ProductQuery>();
        }
    }
}