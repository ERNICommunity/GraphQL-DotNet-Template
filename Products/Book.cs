using System;

namespace productsWebapi.Products
{
    public class Book : Product
    {
        public String Author{get;}
        public Book(String name, String author, Int32 stock) : base(name, stock)
        {
            Author = author;
        }
    }
}