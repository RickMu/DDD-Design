using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;

namespace Application.ProductHandlers
{
    [DataContract]
    public class CreateProductCommand : IRequest<string>
    {
        [DataMember] public Decimal BasePrice { get; set; }
        [DataMember] public IList<AttributeOptionDto> attributes { get; set; }
    }
    public class AttributeOptionDto {
        public string Name { get; set; }
        public int AttributeType { get; set; }
        public string[] AttributeOptions { get; set; }
    }
}