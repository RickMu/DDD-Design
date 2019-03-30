using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common.Domain;
using Newtonsoft.Json;

namespace Domain
{
    public class ProductAttribute: ValueObject
    {
        public string Name { get; }
        public IList<ProductAttributeOption> AttributeOptions { get; }

        public ProductAttribute(string name, IList<ProductAttributeOption> attributeOptions)
        {
            AssertionConcerns.AssertArgumentNotEmpty(name,"ProductAttribute Name cannot be Empty");
            Name = name;
            AttributeOptions = attributeOptions ?? new List<ProductAttributeOption>();
        }

        public bool isValidOption(ProductAttributeOption option)
        {
            return AttributeOptions.Contains(option);
        }
        
        public override IEnumerable<object> GetMembersForEqualityComparision()
        {
            yield return Name;
        }

        public override string ToString()
        {
            return $"AttributeName: {Name}, Value {JsonConvert.SerializeObject(AttributeOptions.Select(x => x.Value))}";
        }
    }
}