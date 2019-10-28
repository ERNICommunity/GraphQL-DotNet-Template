using System;

namespace productsWebapi.Products
{
    // Or have a hierarchical structure (there are libs that do that for me)
    public interface IRelatable
    {
        // This is a Product ID!! :-)  
        // If an interface it would be a Principal...
         Int32 RelationId {get;}
    }
}