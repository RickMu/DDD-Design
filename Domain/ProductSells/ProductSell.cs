using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            public const string RELEASED = "Released";
            public const string NOT_RELEASABLE = "Not Releasable";
            public const string NOT_NULLABLE = "Not Nullable";
        }

        public static ProductSell GetProductSell(int lastForDays, bool isReleased=false ,bool isReleasable=false)
        {
            var startDate = DateTime.UtcNow;
            var endDate = startDate.AddDays(lastForDays);
            return new ProductSell((startDate,endDate), isReleased: isReleased,isReleasable: isReleasable);
        }
        
        public IList<ProductCombination> Combinations { get; }
        public IList<SellSignup> Signups { get; private set; }
        
        public (DateTime StartDateTime, DateTime EndDateTime) ActiveDateTime { get; }
        public bool IsReleasable { get; private set; }
        public bool IsReleased { get; private set; }

        private ProductSell()
        {
            Combinations = new List<ProductCombination>();
            Signups = new List<SellSignup>();
        }
        
        public ProductSell(
            (DateTime StartDateTime, DateTime EndDateTime) activeDateTime,
            IList<ProductCombination> combinations = null, 
            IList<SellSignup> signups = null,
            bool isReleasable = false, bool isReleased = false)
        {
            ActiveDateTime = activeDateTime;
            IsReleasable = isReleasable;
            IsReleased = isReleased;
            Combinations = combinations ?? new List<ProductCombination>();
            Signups = signups ?? new List<SellSignup>();
        }

        public bool IsStillActive()
        {
            return DateTime.Now >= ActiveDateTime.StartDateTime && DateTime.Now <= ActiveDateTime.EndDateTime;
        }

        public void AddProductCombination(ProductCombination productCombination)
        {
            AssertionConcerns.AssertArgumentToBeFalse(IsReleased, $"{Reasons.RELEASED}: Product Sell is already released, cannot add additional combination and discount");
            
            if (!IsReleasable)
            {
                IsReleasable = productCombination.IsBaseCombination();
            }
            Combinations.Add(productCombination);
        }

        public void UpdateProductCombination(ProductCombination productCombination)
        {
            AssertionConcerns.AssertArgumentToBeFalse(IsReleased, $"{Reasons.RELEASED}: Product Sell is already released, cannot add additional combination and discount");
            AssertionConcerns.AssertArgumentIn(productCombination.Identity, Combinations.Select(x => x.Identity).ToList() , $"{Reasons.DUPLICATE}: Cannot add a product combination that already is added");
            var oldModel = Combinations.First(x => x.Identity == productCombination.Identity);
            Combinations.Remove(oldModel);
            Combinations.Add(productCombination);
        }
        
        public void ReleaseProductSell()
        {
            AssertionConcerns.AssertArgumentToBeTrue(IsReleasable, $"{Reasons.NOT_RELEASABLE}: Currently not releasable, Product Sell need to contain a base combination to be releasable");
            if (!IsReleased)
            {
                 IsReleased = true;
                 Signups = new List<SellSignup>();
            }
            AssertionConcerns.AssertArugmentNotNull(Signups, $"{Reasons.NOT_NULLABLE}: ProductSell is released but Signups still null");
       }
        
        public void RegisterInterest(ProductCombination combination, SellSignup interestSignup)
        {
            AssertionConcerns.AssertArgumentToBeTrue(IsReleased, "ProductSell has to be released to register for sign ups");
            AssertionConcerns.AssertArgumentIn(combination.Identity, Combinations.Select(x => x.Identity).ToList(), "Product Combination not in this Product Sell");
            AssertionConcerns.AssertArgumentNotIn(interestSignup,
                Signups,
                $"{Reasons.DUPLICATE}: Sign up with {interestSignup} already exists");
            
            Signups.Add(interestSignup);
            combination.AddSignupCount();
            AddDomainEvents(new SellSignupCreated(Identity,combination,interestSignup));
        }
    }
}