using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Customer;
using Domain.ProductAttributes;
using Domain.ProductAttributes.Factory;
using Domain.Products;
using Domain.ProductSells;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace DomainTests.Common
{
    public class Builder
    {

        public static ProductAttribute GetProductAttributeWithDiscreteValue(string name, IList<AttributeOption> values)
        {
            return new ProductAttributeWithDiscreteValue(name, values);
        }

        public static ProductAttribute GetProductAttributeWithContinuousValue(string name, double min, double max)
        {
            return new ProductAttributeWithContinousValue(name, min.ToString(), max.ToString());
        }
        
        public static Product GetProduct(Decimal basePrice, string attrbName, string[] values)
        {
            var attrb = GetProductAttributeWithDiscreteValue(attrbName, values.Select(x => new AttributeOption(x)).ToList());
            return new Product(basePrice,new List<ProductAttribute> {attrb}, null);
        }

        public static SelectedAttribute GetSelectedAttrbs(string name, string value)
        {
            return new SelectedAttribute(name, value);
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
                new SelectedAttribute("ANY1", "ANY"), 
                new SelectedAttribute("ANY2", "ANY"), 
                new SelectedAttribute("ANY3", "ANY")
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
            combination.SetProductDiscountAndPrice(discount);
            var productSell = ProductSell.GetProductSell(10);   
            productSell.AddProductCombination(combination);
            productSell.ReleaseProductSell();
            return productSell;
        }
    }
}