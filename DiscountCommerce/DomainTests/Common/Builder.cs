using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Product;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

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
            return new Product(basePrice,new List<ProductAttribute> {attrb}, null);
        }

        public static SelectedAttribute GetSelectedAttrbs(string name, string value)
        {
            return new SelectedAttribute(name, new ProductAttributeOption(value));
        }

        public static IList<SelectedAttribute> GetListSelectedAttrbs(string[] names, string[] values)
        {
            var nameAndValues = names.Zip(values, (n, v) => new {Name = n, Value = v}).ToList();
            var retValue = new List<SelectedAttribute>();
            foreach (var nameAndValue in nameAndValues)
            {
                var attrb = GetSelectedAttrbs(nameAndValue.Name, nameAndValue.Value);
                retValue.Add(attrb);
            }

            return retValue;
        }
    }
}