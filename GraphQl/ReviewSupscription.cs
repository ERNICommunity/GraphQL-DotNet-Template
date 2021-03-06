using GraphQL.Types;
using GraphQL.Resolvers;
using productsWebapi.GraphQl.Types;
using productsWebapi.GraphQl.Messaging;

namespace productsWebapi.GraphQl
{
    public sealed class ReviewSupscription: ObjectGraphType
    {
        public ReviewSupscription(ReviewMessageService messageService)
        {
            Name = nameof(ReviewSupscription);
            AddField(new EventStreamFieldType{
                Name = "reviewAdded",
                Type = typeof(ReviewAddedMessageType),
                Resolver = new FuncFieldResolver<ReviewAddedMessage>(c => c.Source as ReviewAddedMessage),
                Subscriber = new EventStreamResolver<ReviewAddedMessage>(_ => messageService.Messages)
            });
        }
    }
}