using System.Collections.Generic;
using Domain.Common.Domain;

namespace Domain
{
    public class ProductAttributeOption: ValueObject
    {
        public string Value { get; }

        public ProductAttributeOption(string value)
        {
            AssertionConcerns.AssertStringNotEmpty(value, "ProductAttributeValue cannot be empty");
            Value = value;
        }
        public override IEnumerable<object> GetMembersForEqualityComparision()
        {
            return new[] {Value};
        }
    }
}