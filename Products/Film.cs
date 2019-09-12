using System;

namespace productsWebapi.Products
{
    public class Film : Product
    {
        public String Director {get;}
        public FilmKind Kind {get;}
        private Film(String name, String director, Int32 stock, FilmKind kind) : base(name, stock)
        {
            Kind = kind;
            Director = director;
        }

        public static Film SciFi(String name, String director, Int32 stock) => new Film(name, director, stock, FilmKind.SciFi);
        public static Film Comendy(String name, String director, Int32 stock) => new Film(name, director, stock, FilmKind.Comendy);
        public static Film Fantasy(String name, String director, Int32 stock) => new Film(name, director, stock, FilmKind.Fantasy);
        public static Film Documnetary(String name, String director, Int32 stock) => new Film(name, director, stock, FilmKind.Documnetary);
    }

    public enum FilmKind{
        Documnetary,
        Fantasy,
        Comendy,
        SciFi
    }
}