using System;
using shortid;
using shortid.Configuration;

namespace productsWebapi.Products
{
    internal static class IdService
    {
        private static readonly GenerationOptions Options = new GenerationOptions{UseNumbers = true, UseSpecialCharacters = false};
        public static String NewId() => ShortId.Generate(Options);
    }
}