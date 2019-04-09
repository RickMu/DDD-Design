
using System.Linq;
using Domain.Common.Domain;
using Domain.Common.Exception;

namespace Domain.ProductAttributes.Factory
{
    public class Reasons
    {
        public const string WRONG_LENGTH = "Wrong Length";
        public const string AttributeTypeError = "ProductAttribute Type Error";
    }
    public class ProductAttributeFactory: IProductAttributeFactory
    {
        public ProductAttribute Create(string name, AttributeType type, AttributeOption[] values)
        {
            switch (type)
            {
                case AttributeType.Discrete:
                    return new ProductAttributeWithDiscreteValue(name,values.ToList());
                    
                case AttributeType.Continuous:
                    AssertionConcerns.AssertArgumentLength(values, 2, $"{Reasons.WRONG_LENGTH}: Given length is not 2, Name: {name}, Values: {values}");
                    return new ProductAttributeWithContinousValue(name, values[0], values[1]);
                
                default:
                    throw new DomainException($"{Reasons.AttributeTypeError}: No Such AttributeType");
            }
        }
    }
}