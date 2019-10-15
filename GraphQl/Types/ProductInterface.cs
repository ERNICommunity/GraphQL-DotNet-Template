using System;
using GraphQL.Types;
using productsWebapi.Products;

namespace productsWebapi.GraphQl.Types
{
    public sealed class ProductInterface: InterfaceGraphType<IProduct>
    {
        public ProductInterface() 
        {
            Name = nameof(Product);
            Register<IProduct>(this);
        }

        internal static void Register<TProduct>(ComplexGraphType<TProduct> type, Func<ResolveFieldContext<TProduct>, Object> reviewResolver=null)
            where TProduct: IProduct
        {
            type.Field(p => p.Name).Description("The products name");
            type.Field(p => p.Id).Description("The products id").Type(new NonNullGraphType(new IdGraphType()));
            type.Field(p => p.Stock).Description("The number of products in stock");
            type.Field(p => p.Type).Description("The type of product");
            type.Field<ListGraphType<ReviewType>>("reviews", resolve: reviewResolver);
        }
    }
}