using System;
using Domain.Common.Domain;

namespace Domain.ProductAttributes
{
    public class Reasons
    {
        public const string UNPARSEABLE = "Unparseable";
    }
    public class ProductAttributeWithContinousValue: ProductAttribute
    {
        public ProductAttributeOption MinValue { get; }
        
        public ProductAttributeOption MaxValue { get; }
        
        public ProductAttributeWithContinousValue(string name,ProductAttributeOption minValue, ProductAttributeOption maxValue ) : base(name)
        {
            AssertionConcerns.AssertArgumentCanBeDouble(minValue.Value,
                $"{Reasons.UNPARSEABLE}: Value is not parseable as double {minValue.Value}");
            AssertionConcerns.AssertArgumentCanBeDouble(maxValue.Value,
                $"{Reasons.UNPARSEABLE}: Value is not parseable as double {maxValue.Value}");
            MaxValue = maxValue;
            MinValue = minValue;

        }

        protected override bool checkIsValidOption(ProductAttributeOption option)
        {
            
            AssertionConcerns.AssertArgumentCanBeDouble(option.Value,
                $"{Reasons.UNPARSEABLE}: Value is not parseable as double {option.Value}");
            var min = Double.Parse(MinValue.Value);
            var max = Double.Parse(MaxValue.Value);
            var val = Double.Parse(option.Value);
            return val >= min && val <= max;
        }

        public override AttributeType GetAttributeType()
        {
            return AttributeType.Continuous;
        }
    }
}