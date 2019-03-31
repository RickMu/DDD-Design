using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common.Domain;
using Domain.Customer;
using Domain.ProductSells.Events;

namespace Domain.ProductSells
{
    public class ProductSell: Entity
    {
        public class Reasons
        {
            public const string DUPLICATE = "Duplicate";
            public const string RELEASED = "RELEASED";
            public const string NOT_RELEASABLE = "Not Releasable";
        }
        public IDictionary<ProductCombination, ProductPrice> CombinationAndPrice { get; }
        public IDictionary<ProductCombination, IList<SellSignup>> Signups { get; private set; }
        //ProductSell with a base combination is releasable. 
        public bool IsReleasable { get; private set; }
        public bool IsReleased { get; private set; }

        public ProductSell(IDictionary<ProductCombination, ProductPrice> combinationAndPrice = null, 
            IDictionary<ProductCombination, IList<SellSignup>> signups = null,
            bool isReleasable = false, bool isReleased = false)
        {
            IsReleasable = isReleasable;
            IsReleased = isReleased;
            CombinationAndPrice = combinationAndPrice ?? new Dictionary<ProductCombination, ProductPrice>();
            Signups = signups ?? new Dictionary<ProductCombination, IList<SellSignup>>();
        }

        public void AddProductCombinationAndDiscount(ProductCombination productCombination, ProductPrice discount)
        {
            AssertionConcerns.AssertArgumentToBeFalse(IsReleased, $"{Reasons.RELEASED}: Product Sell is already released, cannot add additional combination and discount");
            AssertionConcerns.AssertArgumentNotIn(productCombination, CombinationAndPrice.Keys.ToList(), $"{Reasons.DUPLICATE}: Cannot add a product combination that already is added");

            if (!IsReleasable)
            {
                IsReleasable = productCombination.IsBaseCombination();
            }
            CombinationAndPrice.Add(productCombination, discount);
        }

        public void ReleaseProductSell()
        {
            AssertionConcerns.AssertArgumentToBeTrue(IsReleasable, $"{Reasons.NOT_RELEASABLE}: Currently not releasable, Product Sell need to contain a base combination to be releasable");
            if (!IsReleased)
            {
                 IsReleased = true;
                 Signups = new Dictionary<ProductCombination, IList<SellSignup>>();
                 foreach (var combination in CombinationAndPrice.Keys)
                 {
                     Signups.Add(combination, new List<SellSignup>());
                 }
            }
       }
        
        public void RegisterInterest(ProductCombination combination, SellSignup interestSignup)
        {
            AssertionConcerns.AssertArgumentToBeTrue(IsReleased, "ProductSell has to be released to register for sign ups");
            AssertionConcerns.AssertArgumentIn(combination, Signups.Keys.ToList(), "Product Combination not in this Product Sell");
            AssertionConcerns.AssertArgumentNotIn(interestSignup,
                Signups[combination],
                $"{Reasons.DUPLICATE}: Sign up with {interestSignup} already exists");

            if (Signups[combination] == null)
            {
                Signups[combination] = new List<SellSignup>();
            }
            Signups[combination].Add(interestSignup);
            AddDomainEvents(new SellSignupCreated(this.Id.Id.ToString(),combination,interestSignup));
        }

        public decimal GetCombinationCurrentPrice(ProductCombination combination)
        {
            return CombinationAndPrice[combination].CalculatePrice(Signups[combination].Count);
        }
    }
}