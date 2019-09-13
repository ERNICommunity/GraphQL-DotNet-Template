using System;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using productsWebapi.Products;
using productsWebapi.Repositories;
using productsWebapi.GraphQl.Types;

namespace productsWebapi.GraphQl
{
    public sealed class ProductMutation : ObjectGraphType
    {
        const String reviewArg = "review";
        readonly IRepository<IProduct> _productRepo;
        readonly IMutableRepository<Review> _reviewRepo;
        public ProductMutation(IRepository<IProduct> productRepo, IMutableRepository<Review> reviewRepo)
        {
            _reviewRepo = reviewRepo;
            _productRepo = productRepo;
            var args = new QueryArguments(new QueryArgument<NonNullGraphType<ReviewInputType>>{Name = reviewArg});
            FieldAsync<ReviewInputType>("createReview", arguments: args, resolve: AddReview);
        }

        private async Task<Object> AddReview(ResolveFieldContext<Object> context)
        {
            var review = CreateReview(context.GetArgument<ReviewDto>(reviewArg));
            return await context.TryAsyncResolve(async c => await _reviewRepo.Add(review).ConfigureAwait(false)).ConfigureAwait(false);
        }

        private Review CreateReview(ReviewDto reviewDto)
        {
            IProduct product = _productRepo.FirstOrDefault(p => p.Name == reviewDto.ProductName);
            if (product == null){
                throw new ArgumentOutOfRangeException($"No product named '{reviewDto.ProductName}' found.");
            }
            String title = reviewDto.Title;
            String text = reviewDto.Text;
            switch(product){
                // Badness having to switch over the types!
                case Book book:
                    return new Review<Book>(book, title, text);
                case Film film:
                    return new Review<Film>(film, title, text);
                case Shoe shoe:
                    return new Review<Shoe>(shoe, title, text);
            }
            throw new ArgumentOutOfRangeException($"Unknown how to add a review to a product of type '{product.Type}'.");
        }
    }
}