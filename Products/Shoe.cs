using System;
using System.Collections.Generic;

namespace productsWebapi.Products
{
    public sealed class Shoe : Product
    {
        public Int32 Size {get;}
        public Shoe(String name) : this(name, 0, 0){}
        private Shoe(String name, Int32 size, Int32 stock) : base(name, stock)
        {
            Size = size;
        }
        public Shoe With(Int32 size, Int32 stock) => new Shoe(Name, size, stock);
    }
}