using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using GraphQL;
using GraphQL.Types;
using productsWebapi.GraphQl.Types;
using productsWebapi.Products;
using productsWebapi.Repositories;

namespace productsWebapi.GraphQl
{
    public sealed class ProductQuery : ObjectGraphType
    {
        const String idArg = "id";
        const String nameArg = "name";
        const String typeArg = "type";
        const String firstArg = "first";

        private static readonly IEnumerable<IProduct> NO_PRODUCTS = new IProduct[0];
        private readonly IRepository<IProduct> _repo;
        public ProductQuery(IRepository<IProduct> repo)
        {
            _repo = repo;
            var productArgs = new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>>{Name = idArg});
            var productsArgs = new QueryArguments(
                new QueryArgument<IntGraphType>{Name = firstArg, DefaultValue=-1},
                new QueryArgument<StringGraphType>{Name = nameArg, DefaultValue=null},           
                new QueryArgument<StringGraphType>{Name = typeArg, DefaultValue=null}
                );
            Field<ProductInterface>("product", arguments: productArgs, resolve: ResolveProduct);
            Field<ListGraphType<ProductInterface>>("products", arguments: productsArgs, resolve: ResolveProducts);
        }

        private IProduct ResolveProduct(IResolveFieldContext<Object> context)
        {
            var id = context.GetArgument<String>(idArg);
            var type = context.GetArgument<String>(typeArg);
            var products = _repo.Where(product => product.Id == id);
            if(!String.IsNullOrEmpty(type)){
                products = products.Where(product => product.Type == type);
            }
            return products.FirstOrDefault();
        }

        private async Task<IEnumerable<IProduct>> ResolveProducts(IResolveFieldContext<Object> context)
        {
            var first = context.GetArgument<Int32>(firstArg);
            var name = context.GetArgument<String>(nameArg);
            var type = context.GetArgument<String>(typeArg);
            if(first == 0){
                return NO_PRODUCTS; 
            }
            var products = await _repo.All().ConfigureAwait(false);
            if(name != null){
                products = products.Where(p => p.Name == name);
            }
            if(type != null){
                products = products.Where(p => p.Type == type);
            }
            return first < 0 ? products : products.Take(first);
        }
    }
}