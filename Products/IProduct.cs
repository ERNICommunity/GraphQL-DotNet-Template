using System;

namespace productsWebapi.Products
{
    public interface IProduct : IIdentifiable
    {
         String Name{get;}
         String Type{get;}
         Int32 Stock{get;}
    }
}