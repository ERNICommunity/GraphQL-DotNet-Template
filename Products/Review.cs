using System;

namespace productsWebapi.Products
{
    public abstract class Review: IIdentifiable, IRelatable
    {
        public String Id {get;}
        public String Title {get;}
        public String Text {get;}
        public abstract String RelationId { get; }
        // Badness exposing a name here :-/
        public abstract String RelationName { get; }
        protected Review(String title, String text){
            Id = IdService.NewId();
            Title = title;
            Text = text;
        }
    }

    public sealed class Review<TProduct>: Review
        where TProduct : IProduct
    {
        public TProduct Product { get; }
        public override String RelationId => Product.Id;
        public override String RelationName => Product.Name;
        internal Review(TProduct product, String title, String text)
            :base(title, text)
        {
            Product = product;
        }
    }
}