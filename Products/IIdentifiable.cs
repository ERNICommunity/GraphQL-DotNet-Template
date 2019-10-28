using System;

namespace productsWebapi.Products
{
    public interface IIdentifiable
    {
        // This should not be the primary key used in the DB!!
        // problems occur when we do data migration...
        // Especially when a solution has to be scaled

        // Solution: Do not expose this ID in the API!
        // Use GUID's as datafield on the product!
        // Practical hint: use a library that generates url compatible UUIDs that are approx six chars long.
        Int32 Id {get;}
    }
}