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
        const String nameArg = "name";
        const String typeArg = "type";
        private readonly IRepository<IProduct> _repo;
        public ProductQuery(IRepository<IProduct> repo)
        {
            _repo = repo;
            var args = new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>>{Name = nameArg},
                new QueryArgument<StringGraphType>{Name = typeArg});
            Field<ProductInterface>("product", arguments: args, resolve: ResolveProduct);
            Field<ListGraphType<ProductInterface>>("products", resolve: _ => _repo.All());
        }

        private IProduct ResolveProduct(ResolveFieldContext<Object> context)
        {
            var name = context.GetArgument<String>(nameArg);
            var type = context.GetArgument<String>(typeArg);
            var products = _repo.Where(product => product.Name == name);
            if(!String.IsNullOrEmpty(type)){
                products = products.Where(product => product.Type == type);
            }
            return products.FirstOrDefault();
        }
    }
}