using System;
using GraphQL.Types;

namespace productsWebapi.GraphQl.Types
{
    public sealed class ReviewInputType : InputObjectGraphType
    {
        public ReviewInputType()
        {
            Name = "reviewInput";
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<NonNullGraphType<StringGraphType>>("productName");
            Field<StringGraphType>("text");
        }
    }
}