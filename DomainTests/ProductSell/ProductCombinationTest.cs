using System;
using System.Collections.Immutable;
using Domain;
using Domain.Common.Exception;
using Domain.ProductSells;
using DomainTests.Common;
using FluentAssertions;
using Xunit;

namespace ProductSellsTests
{
    public class ProductCombinationTest
    {
        Func<ProductCombination> ProductCombination = () => 
            Builder.GetProductCombination(new string[] {"firstAttrb"}, new string[] {"value"});
        
        [Fact]
        public void ProductCombination_WithAll_ProductAttributeOption_ANY_IsBaseCombination()
        {
            var productCombination = Builder.GetBaseCombination();
            productCombination.IsBaseCombination().Should().Be(true);
        }

        [Fact]
        public void CannotAdd_DuplicatedSignup()
        {
            var productCombination = ProductCombination();
            productCombination.AddSellSignUp(Builder.GetSellSignup("emailOne"));
            productCombination.Invoking(x => x.AddSellSignUp(Builder.GetSellSignup("emailOne")))
                .Should().Throw<DomainException>().Where(x => x.Message.Contains("emailOne"));
        }

        [Fact]
        public void CannotAdd_NullSellSignup()
        {
            var productCombination = ProductCombination();
            productCombination.Invoking(x => x.AddSellSignUp(null))
                .Should().Throw<DomainException>();
        }
    }
}