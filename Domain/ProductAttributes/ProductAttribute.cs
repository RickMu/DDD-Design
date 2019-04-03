using System.Collections.Generic;
using System.Runtime.Serialization;
using Domain.Common.Domain;

namespace Domain.ProductAttributes
{
    public enum AttributeType
    {   
        [EnumMember(Value = "Discrete")]
        Discrete = 1,
        [EnumMember(Value = "Continuous")]
        Continuous = 2,
    }
    public abstract class ProductAttribute: ValueObject
    {
        public string Name { get; }

        public ProductAttribute(string name)
        {
            AssertionConcerns.AssertArgumentNotEmpty(name,"ProductAttribute Name cannot be Empty");
            Name = name;
        }

        public bool isValidOption(ProductAttributeOption option)
        {
            if (option.Equals(ProductAttributeOption.ANY)) return true;
            return checkIsValidOption(option);
        }
        
        protected abstract bool checkIsValidOption(ProductAttributeOption option);

        public bool isBaseAttribute(ProductAttributeOption option)
        {
            return option.Equals(ProductAttributeOption.ANY);
        }

        public abstract AttributeType GetAttributeType();
        
        public override IEnumerable<object> GetMembersForEqualityComparision()
        {
            yield return Name;
        }

        public override string ToString()
        {
            return $"AttributeName: {Name}";
        }
    }
}