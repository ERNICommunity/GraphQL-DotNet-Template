using System;
using System.Collections.Generic;
using productsWebapi.Products;
using static productsWebapi.Products.Film;
using productsWebapi.Repositories;

namespace productsWebapi
{
    public static class DemoData
    {
        private sealed class Data
        {
            public Dictionary<Guid, Review> Reviews {get;}
            public Dictionary<Guid, IProduct> Products {get;}

            public Data(){
                Reviews = new Dictionary<Guid, Review>();
                Products = new Dictionary<Guid, IProduct>();
            }

            public void Add(IProduct product){
                Products.Add(product);
            }
            public void Add<TProduct>(TProduct product, params Review[] reviews)
                where TProduct: IProduct
            {
                Products.Add(product);
                foreach (var review in reviews)
                {
                    Reviews.Add(review);
                }
            }
        }
        internal static void Seed(out IRepository<IProduct> products, out IRepository<Review> reviews){
            var data = new Data();
            AddBooks(ref data);
            AddFilms(ref data);
            AddShoes(ref data);
            products = new InMemoryRepository<IProduct>(data.Products);
            reviews = new InMemoryRepository<Review>(data.Reviews);
        }
        private static void AddFilms(ref Data data){
            var film = Fantasy("The Lord of the Rings", "Peter Jackson", 12);
            data.Add(film);
            film = Fantasy("Fargo", "The Coen Brothers",5);
            data.Add(film,
                film.Review("Thoughtful", "The telephone rings at 3 a.m. and a pregnant woman puts on her police uniform to go out into the Minnesota winter and investigate a homicide.")
            );
        }
        private static void AddBooks(ref Data data){
            var book = new Book("The Lord of the Rings", "J. R. R. Tolkien", 12);
            data.Add(book,
                book.Review("Unique!", "A unique, wholly realized other world, evoked from deep in the well of Time, massively detailed, absorbingly entertaining, profound in meaning."),
                book.Review("Epic fantasy by the master", "What can you say about a classic?")
            );
            book = new Book("The Linux Programming Interface", "Michael Kerrisk", 27);
            data.Add(book,
                book.Review("All around an excellent book!", "Great content, relevance, code examples and explanation."),
                book.Review("The Holy Grail Of Linux Programming ", "I love this book. It is a great reference for all things Linux."),
                book.Review("Very detailed.", "The amount of detail in this book is godsend.")
            );
        }
        private static void AddShoes(ref Data data){
            var shoe = new Shoe("Aris Allen");
            data.Add(shoe.With(42, -1));
            data.Add(shoe.With(44, 9));
            data.Add(shoe.With(45, 2));         
        }
        private static void Add<TItem>(this Dictionary<Guid, TItem> d, TItem item)
            where TItem: IIdentifiable
        {
            d.Add(item.Id, item);
        }
    }
}