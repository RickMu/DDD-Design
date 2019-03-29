using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common.Domain;

namespace Domain
{
    public class ProductAttribute: ValueObject
    {
        public string Name { get; }
        public IEnumerable<ProductAttributeOption> AttributeOptions { get; }

        public ProductAttribute(string name, IEnumerable<ProductAttributeOption> attributeOptions)
        {
            Name = name;
            AttributeOptions = attributeOptions;
        }

        public bool isValidOption(ProductAttributeOption option)
        {
            return AttributeOptions.Contains(option);
        }
        
        public override IEnumerable<object> GetMembersForEqualityComparision()
        {
            yield return Name;

            foreach (var option in AttributeOptions)
            {
                yield return option.GetMembersForEqualityComparision();
            }
        }
         
    }
}