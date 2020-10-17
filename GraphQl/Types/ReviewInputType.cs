using GraphQL.Types;

namespace productsWebapi.GraphQl.Types
{
    public sealed class ReviewInputType : InputObjectGraphType
    {
        public ReviewInputType()
        {
            Name = "reviewInput";
            Field<NonNullGraphType<IdGraphType>>("productId");
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<StringGraphType>("text");
        }
    }
}