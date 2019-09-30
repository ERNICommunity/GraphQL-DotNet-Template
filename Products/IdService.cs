using System;

namespace productsWebapi.Products
{
    internal static class IdService
    {
        public static Int32 NewId(){
            Guid guid = Guid.NewGuid();
            Int32 id = Math.Abs(guid.GetHashCode());
            return id % UInt16.MaxValue;
        }
    }
}