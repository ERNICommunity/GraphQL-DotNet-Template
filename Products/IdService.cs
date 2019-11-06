using System;
using shortid;

namespace productsWebapi.Products
{
    internal static class IdService
    {
        public static String NewId() => ShortId.Generate(true, false);
    }
}