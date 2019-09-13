using System;

namespace productsWebapi.Products
{
    public abstract class Review: IIdentifiable, IRelatable
    {
        public Guid Id {get;}
        public String Title {get;}
        public String Text {get;}
        public abstract Guid RelationId { get; }

        protected Review(String title, String text){
            Id = Guid.NewGuid();
            Title = title;
            Text = text;
        }
    }

    public sealed class Review<TProduct>: Review
        where TProduct : IProduct
    {
        public TProduct Product {get;}

        public override Guid RelationId => Product.Id;

        internal Review(TProduct product, String title, String text)
            :base(title, text)
        {
            Product = product;
        }
    }
}