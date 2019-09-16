using System;

namespace productsWebapi.GraphQl.Messaging
{
    public sealed class ReviewAddedMessage
    {
        public String ProductName {get; set;}
        public String Title {get; set;}
    }
}