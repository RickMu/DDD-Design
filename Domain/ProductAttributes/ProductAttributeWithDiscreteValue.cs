using System.Collections.Generic;
using Domain.ProductAttributes.Factory;

namespace Domain.ProductAttributes
{
    public class ProductAttributeWithDiscreteValue: ProductAttribute
    {
        private ProductAttributeWithDiscreteValue() {}
        public ProductAttributeWithDiscreteValue(string name, IList<AttributeOption> attributeOptions) : base(name)
        {
            AttributeOptions = attributeOptions;
        }

        protected override bool checkIsValidOption(AttributeOption option)
        {
            return AttributeOptions.Contains(option);
        }

        public override AttributeType GetAttributeType()
        {
            return AttributeType.Discrete;
        }
    }
}