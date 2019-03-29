using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common.Domain;

namespace Domain.Product
{
    public class Product: Entity
    {
        public IEnumerable<ProductAttribute> Attributes { get; }
        
        public IList<ProductSell> ProductSells { get; }
        
        public Decimal BasePrice { get; }

        public Product(IEnumerable<ProductAttribute> attributes, Decimal basePrice)
            : this(attributes, basePrice, new List<ProductSell>())
        {
        }
        
        public Product(IEnumerable<ProductAttribute> attributes, Decimal basePrice, IList<ProductSell> productSells)
        {
            Attributes = attributes;
            BasePrice = basePrice;
            ProductSells = productSells;
        }

        public void AddProductSell(ProductSell productSell)
        {
            ProductSells.Add(productSell);
        }

        public bool AreAttributesCombinationValidChoice(IEnumerable<SelectedAttribute> combination)
        {
            var combinationNames = combination.Select(x => x.Name);
            var attributesNames = Attributes.Select(x => x.Name);
            if (combinationNames.Except(attributesNames).Any() || attributesNames.Except(combinationNames).Any())
            {
                return false;
            }

            foreach (var attribute in Attributes)
            {
                var combinationAttribute = combination.First(x => x.Name == attribute.Name);
                if (!attribute.isValidOption(combinationAttribute.SelectedOption))
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}