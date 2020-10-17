using GraphQL.Types;
using productsWebapi.Products;

namespace productsWebapi.GraphQl.Types
{
    public sealed class ReviewType: ObjectGraphType<Review>
    {
        public ReviewType(){
            Field(r => r.Title).Description("The reviews title.");
            Field(r => r.Text).Description("The reviews text, i.e. the actual review");
        }
    }
}