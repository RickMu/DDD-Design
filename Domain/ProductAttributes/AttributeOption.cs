using System.Collections.Generic;
using Domain.Common.Domain;

namespace Domain.ProductAttributes.Factory
{
    public class AttributeOption: ValueObject
    {
        public static AttributeOption AnyValue = new AttributeOption("ANY");
        public string Value { get; private set; }
        
        private AttributeOption(){}

        public static implicit operator AttributeOption(string value)
        {
            return new AttributeOption(value);
        }

        public AttributeOption(string value)
        {
            Value = value;
        }

        public override IEnumerable<object> GetMembersForEqualityComparision()
        {
            yield return Value;
        }
    }
}