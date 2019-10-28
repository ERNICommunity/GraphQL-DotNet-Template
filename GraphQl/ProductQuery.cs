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
    // These classes that expose API components, ought to expose docs that comply with OpenAPI
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
                new QueryArgument<NonNullGraphType<IdGraphType>>{Name = idArg});
            var productsArgs = new QueryArguments(
                new QueryArgument<IntGraphType>{Name = firstArg, DefaultValue=-1},
                new QueryArgument<StringGraphType>{Name = nameArg, DefaultValue=null},           
                new QueryArgument<StringGraphType>{Name = typeArg, DefaultValue=null}
                );
            Field<ProductInterface>("product", arguments: productArgs, resolve: ResolveProduct);
            Field<ListGraphType<ProductInterface>>("products", arguments: productsArgs, resolve: ResolveProducts);
        }

        // This ougth to be documented as this method actually resolves the product in the public API
        private IProduct ResolveProduct(ResolveFieldContext<Object> context)
        {
            var id = context.GetArgument<Int32>(idArg);
            var type = context.GetArgument<String>(typeArg);
            var products = _repo.Where(product => product.Id == id);
            if(!String.IsNullOrEmpty(type)){
                products = products.Where(product => product.Type == type);
            }
            return products.FirstOrDefault();
        }

        private async Task<IEnumerable<IProduct>> ResolveProducts(ResolveFieldContext<Object> context)
        {
            // These queries should use some mapping from internal entities
            // and map the internal instances to public strongly typed instances
            // Mapping:
            // - https://automapper.org/
            // - http://docs.automapper.org/en/stable/Projection.html
            
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