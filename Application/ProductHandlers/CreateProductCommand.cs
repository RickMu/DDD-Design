using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;

namespace Application.ProductHandlers
{
    [DataContract]
    public class CreateProductCommand : IRequest<CreateProductResponse>
    {
        [DataMember] public Decimal BasePrice { get; set; }
        [DataMember] public IList<AttributeOptionDto> attributes { get; set; }
    }
    public class AttributeOptionDto {
        public string Name { get; set; }
        public int AttributeType { get; set; }
        public string[] AttributeOptions { get; set; }
    }
    
    public class CreateProductResponse
    {
        public string Id { get; set; }

        public static implicit operator CreateProductResponse(string id)
        {
            return new CreateProductResponse
            {
                Id = id
            };
        }
    }
}