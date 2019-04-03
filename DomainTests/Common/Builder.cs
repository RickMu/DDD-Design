using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Customer;
using Domain.ProductAttributes;
using Domain.Products;
using Domain.ProductSells;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace DomainTests.Common
{
    public class Builder
    {
        public static ProductAttributeOption GetProductAttributeOption(string value)
        {
            return new ProductAttributeOption(value);
        }

        public static ProductAttribute GetProductAttributeWithDiscreteValue(string name, string[] values)
        {
            var options = values.Select(GetProductAttributeOption).ToList();
            return new ProductAttributeWithDiscreteValue(name, options);
        }

        public static ProductAttribute GetProductAttributeWithContinuousValue(string name, double min, double max)
        {
            var minOption = new ProductAttributeOption(min.ToString());
            var maxOption = new ProductAttributeOption(max.ToString());
            return new ProductAttributeWithContinousValue(name, minOption, maxOption);
        }
        
        public static Product GetProduct(Decimal basePrice, string attrbName, string[] values)
        {
            var attrb = GetProductAttributeWithDiscreteValue(attrbName, values);
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

        public static ProductCombination GetProductCombination(string[] names, string[] values)
        {
            var selectedAttrbs = GetListSelectedAttrbs(names, values);
            return new ProductCombination(selectedAttrbs);
        }

        public static ProductCombination GetBaseCombination()
        {
            return new ProductCombination(new[]
            {
                new SelectedAttribute("ANY1", ProductAttributeOption.ANY), 
                new SelectedAttribute("ANY2", ProductAttributeOption.ANY), 
                new SelectedAttribute("ANY3", ProductAttributeOption.ANY)
            });
        }
        
        public static IList<ProductCombination> GetListProductCombination(string[][] names, string[][] values)
        {
            var namesAndValues = names.Zip(values, (n, v) => new { Names = n, Values = v});
            var productCombinations = new List<ProductCombination>();

            foreach (var nV in namesAndValues)
            {
                productCombinations.Add(GetProductCombination(nV.Names, nV.Values));
            }
            return productCombinations;
        }
        
        public static ProductPrice GetProducePrice(decimal discount, decimal price, decimal lowestPrice)
        {
            return new ProductPrice(discount, price, lowestPrice);
        }

        public static SellSignup GetSellSignup(string email)
        {
            return new SellSignup(email);
        }

        public static IList<SellSignup> GetSellSignups(string[] emails)
        {
            var list = new List<SellSignup>();
            foreach (var email in emails)
            {
                list.Add(GetSellSignup(email));
            }

            return list;
        }

        public static ProductSell GetReleasedProductSell()
        {
            var discount = Builder.GetProducePrice(1, 30, 0);
            var combination = Builder.GetBaseCombination();
            var productSell = new ProductSell();   
            productSell.AddProductCombinationAndDiscount(combination,discount);
            productSell.ReleaseProductSell();
            return productSell;
        }
    }
}