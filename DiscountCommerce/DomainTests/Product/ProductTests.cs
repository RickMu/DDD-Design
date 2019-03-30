using System;
using Domain;
using Domain.Common.Exception;
using Domain.Product;
using DomainTests.Common;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace DomainTests.ProductTests
{
    public class ProductTests
    {
        private Func<Product> product;
        
        public ProductTests()
        {
            product = () => Builder.GetProduct(1, "firstAttrb", new string[] {"1", "2", "3"});
        }

        [Fact]
        public void CannotAdd_AnotherAttribute_OfSameName()
        {
            var duplicateAttrb = Builder.GetProductAttribute("firstAttrb", new string[] {"value"});
            var product = this.product();
            
            product.Invoking(x =>
                    x.AddAttribute(duplicateAttrb))
                .Should().Throw<DomainException>().Where(e => e.Message.Contains($"{duplicateAttrb}"));
        }
        
        [Fact]
        public void AddProductAttribute_Should_Work()
        {
            var secondAttrb = Builder.GetProductAttribute("secondAttrb", new string[] {"value"});
            var product = this.product();
            product.AddAttribute(secondAttrb);    
        }

        [Theory]
        [InlineData("2", "3", true)]
        [InlineData("-1","5", false)]
        public void AreAttributesCombinationValidChoice_Should_ReturnTrue(string firstOption, string secondOption, bool expected)
        {
            var product = this.product();
            var secondAttribute = Builder.GetProductAttribute("secondAttrb", new string[] {"1","2","3"});
            product.AddAttribute(secondAttribute);

            var attrbCombination = Builder.GetListSelectedAttrbs(new string[] {"firstAttrb", "secondAttrb"},
                new string[] {firstOption, secondOption});

            product.AreAttributesCombinationValidChoice(attrbCombination)
                .Should().Be(expected);
        }
    }
}