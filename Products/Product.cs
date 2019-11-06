using System;

namespace productsWebapi.Products
{
    public abstract class Product: IProduct
    {
        public String Id {get;}
        public String Name {get;}
        public String Type {get;}
        public Int32 Stock {get;}
        protected Product(String name, Int32 stock)
        {
            Name = name;
            Id = IdService.NewId();
            Type = GetType().Name;
            Stock = stock;
        }
    }
}