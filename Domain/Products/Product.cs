using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common.Domain;
using Domain.ProductAttributes;
using Domain.ProductSells;

namespace Domain.Products
{
    public class Product: Entity
    {
        
        public IList<ProductAttribute> Attributes { get; }
        
        public IList<ProductSell> ProductSells { get; }
        
        public Decimal BasePrice { get; }
        
        
        
        public Product(Decimal basePrice, IList<ProductAttribute> attributes, IList<ProductSell> productSells)
        {
            AssertionConcerns.AssertArgumentRange(basePrice, 0, Decimal.MaxValue, "Product Base Price Cannot be Negative");
            BasePrice = basePrice;
            Attributes = attributes ?? new List<ProductAttribute>();
            ProductSells = productSells ?? new List<ProductSell>();
        }

        public void AddAttribute(ProductAttribute productAttribute)
        {
            AssertionConcerns.AssertArgumentNotIn(productAttribute, Attributes, $"Product Attribute Already Exists: {productAttribute}");
            Attributes.Add(productAttribute);
        }
        public void AddProductSell(ProductSell productSell)
        {
            ProductSells.Add(productSell);
        }
    }
}