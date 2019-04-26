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
        public class Reasons
        {
            public const string InvalidProductCombination = "InvalidProductCombination";
        }
        
        public List<ProductAttribute> Attributes { get; private set; }
        
        public IList<ProductSell> ProductSells { get; private set; }
        
        public Decimal BasePrice { get; private set; }

        public Product()
        {
            Attributes = new List<ProductAttribute>();
            ProductSells = new List<ProductSell>();
        }
        
        public Product(Decimal basePrice, List<ProductAttribute> attributes, IList<ProductSell> productSells)
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
            //each combination in product sell has to be asserted that it contains all changeable attributes
            ProductSells.Add(productSell);
        }

        public bool CanBeDeleted()
        {
            return ProductSells.All(x => !x.IsStillActive());
        }
    }
}