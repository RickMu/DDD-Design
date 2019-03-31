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
            var productSell = new ProductSell();
            productSell.AddProductCombinationAndDiscount(combination, discount);

            productSell.Invoking(x => x.AddProductCombinationAndDiscount(combination, null))
                .Should().Throw<DomainException>().Where(e => e.Message.Contains(ProductSell.Reasons.DUPLICATE));
        }

        [Fact]
        public void Cannot_AddAdditionalProductCombination_OnceReleased()
        {
            var productSell = new ProductSell(isReleased: true);
            var discount = Builder.GetProducePrice(1, 30, 0);
            var combination = Builder.GetProductCombination(new[] {"name1", "name2"}, new[] {"value1", "value2"});

            productSell.Invoking(x => x.AddProductCombinationAndDiscount(combination, discount))
                .Should().Throw<DomainException>().Where(e => e.Message.Contains(ProductSell.Reasons.RELEASED)); 
        }

        [Fact]
        public void NonBaseCombination_CannotSetProductSellState_Releasable()
        {
            var productSell = new ProductSell();
            var discount = Builder.GetProducePrice(1, 30, 0);
            var combination = Builder.GetProductCombination(new[] {"name1", "name2"}, new[] {"value1", "value2"});
            productSell.AddProductCombinationAndDiscount(combination,discount);
            productSell.IsReleasable.Should().BeFalse();
        }
        
        [Fact]
        public void BaseCombination_WillSetProductSellState_Releasable()
        {
            var productSell = new ProductSell();
            var discount = Builder.GetProducePrice(1, 30, 0);
            var combination = Builder.GetBaseCombination();
            productSell.AddProductCombinationAndDiscount(combination,discount);
            productSell.IsReleasable.Should().BeTrue();
        }

        [Fact]
        public void UnreleasableProductSell_CannotBeReleased()
        {
            var productSell = new ProductSell();
            productSell.Invoking(x => x.ReleaseProductSell())
                .Should().Throw<DomainException>().Where(e => e.Message.Contains(ProductSell.Reasons.NOT_RELEASABLE));
        }
        [Fact]
        public void ReleasableProductSell_CanBeReleased()
        {
            var productSell = new ProductSell(isReleasable: true);
            productSell.ReleaseProductSell();
            productSell.IsReleased.Should().BeTrue();
        }

        [Fact]
        public void Signup_Should_WorkAsExpected()
        {
            var productSell = Builder.GetReleasedProductSell();
            var combination = productSell.CombinationAndPrice.Keys.First();
            var price = productSell.CombinationAndPrice[combination];
            var signup = Builder.GetSellSignup("email1");
            
            productSell.RegisterInterest(combination, signup);
            productSell.GetCombinationCurrentPrice(combination)
                .Should().Be(price.CalculatePrice(1));

            productSell.DomainEvents.Count.Should().Be(1);
            productSell.DomainEvents.First().Equals(new SellSignupCreated(productSell.Id.Id.ToString(), combination, signup));
        }
        
        [Fact]
        public void CannotAdd_DuplicateSignups()
        {
            var productSell = Builder.GetReleasedProductSell();
            var combination = productSell.CombinationAndPrice.Keys.First();
            var signup = Builder.GetSellSignup("email1");
            productSell.RegisterInterest(combination, signup);
            productSell.Invoking(x => x.RegisterInterest(combination, signup))
                .Should().Throw<DomainException>().Where(x => x.Message.Contains(ProductSell.Reasons.DUPLICATE));
        }
    }
}