using System;
using System.Collections.Generic;
using Domain.Common.Domain;
using Domain.ProductAttributes.Factory;

namespace Domain.ProductAttributes
{
    public class Reasons
    {
        public const string UNPARSEABLE = "Unparseable";
        public const string UNEXPECTED_VALUE = "Unexpected Value";
    }
    public class ProductAttributeWithContinousValue: ProductAttribute
    {
        public double MinValue => Double.Parse(AttributeOptions[0].Value);

        public double MaxValue => Double.Parse(AttributeOptions[1].Value);

        private ProductAttributeWithContinousValue() {}
        public ProductAttributeWithContinousValue(string name, AttributeOption valueIsAny) : base(name)
        {
            AssertionConcerns.AssertArgumentIs<AttributeOption>(valueIsAny, AttributeOption.AnyValue, 
                $"{Reasons.UNEXPECTED_VALUE}: Value can only be ANY cannot be anything else");
            AttributeOptions = new List<AttributeOption>() {valueIsAny};
        }
        public ProductAttributeWithContinousValue(string name,AttributeOption minValue, AttributeOption maxValue ) : base(name)
        {
            AssertionConcerns.AssertArgumentCanBeDouble(minValue.Value,
                $"{Reasons.UNPARSEABLE}: Value is not parseable as double {minValue}");
            AssertionConcerns.AssertArgumentCanBeDouble(maxValue.Value,
                $"{Reasons.UNPARSEABLE}: Value is not parseable as double {maxValue}");
            AttributeOptions = new List<AttributeOption>() {minValue, maxValue};
        }

        public override bool checkIsValidOption(AttributeOption option)
        {
            
            AssertionConcerns.AssertArgumentCanBeDouble(option.Value,
                $"{Reasons.UNPARSEABLE}: Value is not parseable as double {option}");
           
            var val = Double.Parse(option.Value);
            return val >= MinValue && val <= MaxValue;
        }

        public override AttributeType AttributeType => AttributeType.Continuous;
    }
}