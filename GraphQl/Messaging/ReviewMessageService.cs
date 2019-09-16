using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using productsWebapi.Products;

namespace productsWebapi.GraphQl.Messaging
{
    public sealed class ReviewMessageService
    {
        private readonly ISubject<ReviewAddedMessage> _messageStream;
        public IObservable<ReviewAddedMessage> Messages => _messageStream.AsObservable();
        public ReviewMessageService()
        {
            _messageStream = new ReplaySubject<ReviewAddedMessage>(1);
        }
        public ReviewAddedMessage AddReviewAddedMessage(Review review)
        {
            var message = new ReviewAddedMessage
            {
                ProductName = review.RelationName,
                Title = review.Title
            };
            _messageStream.OnNext(message);
            return message;
        }
    }
}