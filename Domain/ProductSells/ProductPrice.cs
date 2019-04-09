using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Domain.Common.Domain;
using Domain.Common.Exception;
using Domain.Customer;
using Newtonsoft.Json;

namespace Domain.ProductSells
{
    public class ProductPrice : ValueObject
    {
        public decimal Discount { get; private set; }
        public decimal Price { get; private set; }
        
        public decimal LowestPrice { get; private set; }

        private ProductPrice()
        {
        }
        
        public ProductPrice(decimal discount, decimal price, decimal lowestPrice)
        {
            AssertionConcerns.AssertArgumentRange(discount, 0, Decimal.MaxValue, "Discount cannot be negative");
            AssertionConcerns.AssertArgumentRange(price, 0, Decimal.MaxValue, "Price cannot be negative");
            AssertionConcerns.AssertArgumentRange(lowestPrice, 0, Decimal.MaxValue, "LowestPrice cannot be negative");
            Discount = discount;
            Price = price;
            LowestPrice = lowestPrice;
        }

        public decimal CalculatePrice(decimal numberOfSignUps)
        {
            var calculatedPrice = Price - numberOfSignUps * Discount;

            if (calculatedPrice < LowestPrice)
            {
                calculatedPrice = LowestPrice;
            }

            return calculatedPrice;
        }

        public override IEnumerable<object> GetMembersForEqualityComparision()
        {
            yield return Price;
            yield return Discount;
        }
    }
}