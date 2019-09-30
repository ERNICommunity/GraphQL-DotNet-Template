using GraphQL.Types;
using productsWebapi.Products;

namespace productsWebapi.GraphQl.Types
{
    public class ProductInterface: InterfaceGraphType<IProduct>
    {
        public ProductInterface()
        {
            Name = nameof(Product);
            Field(p => p.Name).Description("The products name");
            Field(p => p.Id).Description("The products id").Type(new NonNullGraphType(new IdGraphType()));
            Field(p => p.Stock).Description("The number of products in stock");
            Field(p => p.Type).Description("The type of product");
            Field<ListGraphType<ReviewType>>("reviews");
        }
    }
}