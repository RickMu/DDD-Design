using System.Collections.Generic;

namespace Domain.ProductAttributes
{
    public class ProductAttributeWithDiscreteValue: ProductAttribute
    {
        public ProductAttributeWithDiscreteValue(string name, IList<string> attributeOptions) : base(name)
        {
            AttributeOptions = attributeOptions;
        }

        protected override bool checkIsValidOption(string option)
        {
            return AttributeOptions.Contains(option);
        }

        public override AttributeType GetAttributeType()
        {
            return AttributeType.Discrete;
        }
    }
}