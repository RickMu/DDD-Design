using System.Collections.Immutable;
using System.Linq;
using Domain.Common.Exception;
using Domain.ProductSells.Events;
using Domain.ProductSells;
using DomainTests.Common;
using FluentAssertions;
using Xunit;

namespace DomainTests.ProductSellTests
{
    public class ProductSellTests
    {
        public ProductSellTests()
        {
            
        }
        
        [Fact]
        public void Cannot_AddDuplicateProductCombination()
        {
            var combination = Builder.GetProductCombination(new[] {"name1", "name2"}, new[] {"value1", "value2"});
            var discount = Builder.GetProducePrice(1, 30, 0);
            combination.SetProductDiscountAndPrice(discount);
            var productSell = ProductSell.GetProductSell(10);
            productSell.AddProductCombination(combination);

            productSell.Invoking(x => x.AddProductCombination(combination))
                .Should().Throw<DomainException>().Where(e => e.Message.Contains(ProductSell.Reasons.DUPLICATE));
        }

        [Fact]
        public void Cannot_AddAdditionalProductCombination_OnceReleased()
        {
            var productSell = ProductSell.GetProductSell(10,isReleased: true);
            var discount = Builder.GetProducePrice(1, 30, 0);
            var combination = Builder.GetProductCombination(new[] {"name1", "name2"}, new[] {"value1", "value2"});
            
            combination.SetProductDiscountAndPrice(discount);
            
            productSell.Invoking(x => x.AddProductCombination(combination))
                .Should().Throw<DomainException>().Where(e => e.Message.Contains(ProductSell.Reasons.RELEASED)); 
        }

        [Fact]
        public void NonBaseCombination_CannotSetProductSellState_Releasable()
        {
            var productSell = ProductSell.GetProductSell(10);
            var discount = Builder.GetProducePrice(1, 30, 0);
            var combination = Builder.GetProductCombination(new[] {"name1", "name2"}, new[] {"value1", "value2"});
            
            combination.SetProductDiscountAndPrice(discount);
            
            productSell.AddProductCombination(combination);
            productSell.IsReleasable.Should().BeFalse();
        }
        
        [Fact]
        public void BaseCombination_WillSetProductSellState_Releasable()
        {
            var productSell = ProductSell.GetProductSell(10);
            var discount = Builder.GetProducePrice(1, 30, 0);
            var combination = Builder.GetBaseCombination();
            
            combination.SetProductDiscountAndPrice(discount);
            
            productSell.AddProductCombination(combination);
            productSell.IsReleasable.Should().BeTrue();
        }

        [Fact]
        public void UnreleasableProductSell_CannotBeReleased()
        {
            var productSell = ProductSell.GetProductSell(10,isReleasable: false);
            productSell.Invoking(x => x.ReleaseProductSell())
                .Should().Throw<DomainException>().Where(e => e.Message.Contains(ProductSell.Reasons.NOT_RELEASABLE));
        }
        [Fact]
        public void ReleasableProductSell_CanBeReleased()
        {
            var productSell = ProductSell.GetProductSell(10,isReleasable: true);
            productSell.ReleaseProductSell();
            productSell.IsReleased.Should().BeTrue();
        }

        [Fact]
        public void Signup_Should_WorkAsExpected()
        {
            var productSell = Builder.GetReleasedProductSell();
            var combination = productSell.Combinations.First();
            var price = combination.ProductPrice;
            var signup = Builder.GetSellSignup("email1");
            
            productSell.RegisterInterest(combination, signup);
            combination.GetCurrentPrice()
                .Should().Be(price.CalculatePrice(1));

            productSell.DomainEvents.Count.Should().Be(1);
            productSell.DomainEvents.First().Equals(new SellSignupCreated(productSell.Id, combination, signup));
        }
        
        [Fact]
        public void CannotAdd_DuplicateSignups()
        {
            var productSell = Builder.GetReleasedProductSell();
            var combination = productSell.Combinations.First();
            var signup = Builder.GetSellSignup("email1");
            productSell.RegisterInterest(combination, signup);
            productSell.Invoking(x => x.RegisterInterest(combination, signup))
                .Should().Throw<DomainException>().Where(x => x.Message.Contains(ProductSell.Reasons.DUPLICATE));
        }
    }
}