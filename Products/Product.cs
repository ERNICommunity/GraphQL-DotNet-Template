using System;

namespace productsWebapi.Products
{
    public abstract class Product: IProduct
    {
        public Guid Id {get;}
        public String Name {get;}
        public String Type {get;}
        public Int32 Stock {get;}
        protected Product(String name, Int32 stock)
        {
            Name = name;
            Id = Guid.NewGuid();
            Type = GetType().Name;
            Stock = stock;
        }
    }
}