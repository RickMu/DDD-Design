using System.Collections.Generic;
using Domain.Common.Domain;

namespace Domain.ProductAttributes
{
    public class ProductAttributeOption: ValueObject
    {
        public static ProductAttributeOption ANY = new ProductAttributeOption(int.MinValue.ToString());
        public string Value { get; private set; }

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