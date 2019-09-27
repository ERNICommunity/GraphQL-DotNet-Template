using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
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
        const String firstArg = "first";

        private static readonly IEnumerable<IProduct> NO_PRODUCTS = new IProduct[0];
        private readonly IRepository<IProduct> _repo;
        public ProductQuery(IRepository<IProduct> repo)
        {
            _repo = repo;
            var productArgs = new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>>{Name = nameArg},
                new QueryArgument<StringGraphType>{Name = typeArg});
            var productsArgs = new QueryArguments(
                new QueryArgument<IntGraphType>{Name = firstArg, DefaultValue=-1});
            Field<ProductInterface>("product", arguments: productArgs, resolve: ResolveProduct);
            Field<ListGraphType<ProductInterface>>("products", arguments: productsArgs, resolve: ResolveProducts);
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

        private async Task<IEnumerable<IProduct>> ResolveProducts(ResolveFieldContext<Object> context)
        {
            var first = context.GetArgument<Int32>(firstArg);
            if(first == 0){
                return NO_PRODUCTS; 
            }
            var all = await _repo.All().ConfigureAwait(false);
            return first < 0 ? all : all.Take(first);
        }
    }
}