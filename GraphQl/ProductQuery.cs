using System;
using System.Linq;
using GraphQL.Types;
using productsWebapi.GraphQl.Types;
using productsWebapi.Products;
using productsWebapi.Repositories;

namespace productsWebapi.GraphQl
{
    public sealed class ProductQuery : ObjectGraphType
    {
        public ProductQuery(IRepository<IProduct> repo)
        {
            const String argName = "name";
            var args = new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>{Name = argName});
            Field<ProductInterface>(
                "product",
                arguments: args,
                resolve: context => {
                    var name = context.GetArgument<String>(argName);
                    return repo.FirstOrDefault(product => product.Name == name);
                });
            Field<ListGraphType<ProductInterface>>("products", resolve: _ => repo.All());
        }
    }
}