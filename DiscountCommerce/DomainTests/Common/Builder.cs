using System;
using System.Linq;
using Domain;
using Domain.Product;

namespace DomainTests.Common
{
    public class Builder
    {
        public static ProductAttributeOption GetProductAttributeOption(string value)
        {
            return new ProductAttributeOption(value);
        }

        public static ProductAttribute GetProductAttribute(string name, string[] values)
        {
            var options = values.Select(GetProductAttributeOption).ToList();
            return new ProductAttribute(name, options);
        }

        public static Product GetProduct(Decimal basePrice, string attrbName, string[] values)
        {
            var attrb = GetProductAttribute(attrbName, values);
            return new Product(basePrice,new[] {attrb}, null);
        }
    }
}