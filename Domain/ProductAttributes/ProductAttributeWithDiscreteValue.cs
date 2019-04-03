using System.Collections.Generic;

namespace Domain.ProductAttributes
{
    public class ProductAttributeWithDiscreteValue: ProductAttribute
    {
        private IList<ProductAttributeOption> AttributeOptions { get; }
        public ProductAttributeWithDiscreteValue(string name, IList<ProductAttributeOption> attributeOptions) : base(name)
        {
            AttributeOptions = attributeOptions;
        }

        protected override bool checkIsValidOption(ProductAttributeOption option)
        {
            return AttributeOptions.Contains(option);
        }

        public override AttributeType GetAttributeType()
        {
            return AttributeType.Discrete;
        }
    }
}