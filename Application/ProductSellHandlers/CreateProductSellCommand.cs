using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Domain.ProductSells;
using MediatR;

namespace Application.ProductSellHandlers
{
    [DataContract]
    public class CreateProductSellCommand : IRequest<CreateProductSellResponse>
    {
        [DataMember] public string ProductId { get; set; }
        [DataMember] public DateTime StartTime { get; set; }
        [DataMember] public DateTime EndTime { get; set; }
        
        [DataMember] public IList<ProductCombinationDto> ProductCombination { get; set; }
    }

    public class ProductCombinationDto
    {
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public decimal LowestPrice { get; set; }

        public IList<SelectedAttribute> SpecificAttributes { get; set; }
    }
    
    public class CreateProductSellResponse
    {
        public string Id { get; set; }
        public static implicit operator CreateProductSellResponse(string id)
        {
            return new CreateProductSellResponse
            {
                Id = id
            };
        }
    }
}