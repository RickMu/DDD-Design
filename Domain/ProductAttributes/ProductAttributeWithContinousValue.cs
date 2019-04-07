using System;
using Domain.Common.Domain;

namespace Domain.ProductAttributes
{
    public class Reasons
    {
        public const string UNPARSEABLE = "Unparseable";
        public const string UNEXPECTED_VALUE = "Unexpected Value";
    }
    public class ProductAttributeWithContinousValue: ProductAttribute
    {
        public double MinValue => Double.Parse(AttributeOptions[0]);

        public double MaxValue => Double.Parse(AttributeOptions[1]);

        public ProductAttributeWithContinousValue(string name, string valueIsAny) : base(name)
        {
            AssertionConcerns.AssertArgumentIs(valueIsAny, "ANY", 
                $"{Reasons.UNEXPECTED_VALUE}: Value can only be ANY cannot be anything else");
            AttributeOptions = new[] {valueIsAny};
        }
        public ProductAttributeWithContinousValue(string name,string minValue, string maxValue ) : base(name)
        {
            AssertionConcerns.AssertArgumentCanBeDouble(minValue,
                $"{Reasons.UNPARSEABLE}: Value is not parseable as double {minValue}");
            AssertionConcerns.AssertArgumentCanBeDouble(maxValue,
                $"{Reasons.UNPARSEABLE}: Value is not parseable as double {maxValue}");
            AttributeOptions = new[] {minValue, maxValue};
        }

        protected override bool checkIsValidOption(string option)
        {
            
            AssertionConcerns.AssertArgumentCanBeDouble(option,
                $"{Reasons.UNPARSEABLE}: Value is not parseable as double {option}");
           
            var val = Double.Parse(option);
            return val >= MinValue && val <= MaxValue;
        }

        public override AttributeType GetAttributeType()
        {
            return AttributeType.Continuous;
        }
    }
}