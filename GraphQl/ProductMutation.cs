using System;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using productsWebapi.GraphQl.Messaging;
using productsWebapi.GraphQl.Types;
using productsWebapi.Products;
using productsWebapi.Repositories;

namespace productsWebapi.GraphQl
{
    public sealed class ProductMutation : ObjectGraphType {
        const String reviewArg = "review";
        readonly IRepository<IProduct> _productRepo;
        readonly IMutableRepository<Review> _reviewRepo;
        readonly ReviewMessageService _messageService;
        public ProductMutation (IRepository<IProduct> productRepo, IMutableRepository<Review> reviewRepo, ReviewMessageService messageService) {
            _reviewRepo = reviewRepo;
            _productRepo = productRepo;
            // Badness: This dependency should be removed!
            _messageService = messageService;
            var args = new QueryArguments (new QueryArgument<NonNullGraphType<ReviewInputType>> { Name = reviewArg });
            FieldAsync<ReviewType> ("createReview", arguments : args, resolve : AddReview);
        }

        private async Task<Object> AddReview (IResolveFieldContext<Object> context) {
            var review = await CreateReview (context.GetArgument<ReviewDto> (reviewArg)).ConfigureAwait(false);
            await _reviewRepo.Add(review).ConfigureAwait(false);
            _messageService.AddReviewAddedMessage(review);
            return review;
        }

        private async Task<Review> CreateReview (ReviewDto reviewDto) {
            IProduct product = await _productRepo.Find(reviewDto.ProductId).ConfigureAwait(false);
            if (product == null) {
                throw new ArgumentOutOfRangeException ($"No product with id '{reviewDto.ProductId}' found.");
            }
            String text = reviewDto.Text;
            String title = reviewDto.Title;
            return product switch
            {
                // Badness having to switch over the types!
                Book book => new Review<Book>(book, title, text),
                Film film => new Review<Film>(film, title, text),
                Shoe shoe => new Review<Shoe>(shoe, title, text),
                _ => throw new ArgumentOutOfRangeException(
                    $"Unknown how to add a review to a product of type '{product.Type}'.")
            };
        }
    }
}