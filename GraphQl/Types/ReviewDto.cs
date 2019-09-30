using System;
namespace productsWebapi.GraphQl.Types
{
    public sealed class ReviewDto
    {
        public Int32 ProductId {get; set;}
        public String Title {get; set;}
        public String Text {get; set;}        
    }
}