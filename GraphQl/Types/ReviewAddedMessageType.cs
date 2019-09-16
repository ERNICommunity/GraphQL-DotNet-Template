using GraphQL.Types;
using productsWebapi.GraphQl.Messaging;

namespace productsWebapi.GraphQl.Types
{
    public sealed class ReviewAddedMessageType: ObjectGraphType<ReviewAddedMessage>
    {
        public ReviewAddedMessageType()
        {
            Field(t => t.ProductName);
            Field(t => t.Title);
        }
    }
}