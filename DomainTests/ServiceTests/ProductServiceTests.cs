using System.Collections.Generic;
using Domain;
using Domain.ProductAttributes;
using Domain.ProductAttributes.Factory;
using Domain.Products;
using Domain.ProductSells;
using Domain.Service;
using DomainTests.Common;
using FluentAssertions;
using Xunit;

namespace DomainTests.ServiceTests
{
    public class ProductServiceTests
    {
        private ProductCombinationValidator _sut;

        public ProductServiceTests()
        {
            _sut = new ProductCombinationValidator();
        }
        
        [Theory]
        [InlineData("2", "3", 15, true)]
        [InlineData("-1","5", 15, false)]
        [InlineData("2","3", 5, false)]
        public void AreAttributesCombinationValidChoice_Should_ReturnTrue(string firstOption, string secondOption, double thirdOption, bool expected)
        {
            var product = Builder.GetProduct(1, "firstAttrb", new string[] {"1", "2", "3"});
            
            var secondAttribute = Builder.GetProductAttributeWithDiscreteValue("secondAttrb", new List<AttributeOption>() {"1", "2", "3"});
            product.AddAttribute(secondAttribute);

            var thirdAttribute = Builder.GetProductAttributeWithContinuousValue("thirdAttrb", 10, 20);
            product.AddAttribute(thirdAttribute);
            
            var prodCombination = Builder.GetProductCombination(new string[] {"firstAttrb", "secondAttrb", "thirdAttrb"},
                new string[] {firstOption, secondOption, thirdOption.ToString()});

            
            _sut.EnsureCombinationContainsAllAttributes(prodCombination, product);
            _sut.EnsureCombinationContainsValidOptions(prodCombination, product)
                .Should().Be(expected);
        }
        
        [Fact]
        public void AreAttributeCombinationBaseCombination_And_AreAttributesValidProductChoice_ShouldReturnTrue()
        {
            var product = Builder.GetProduct(1, "firstAttrb", new string[] {"1", "2", "3"});
            
            var secondAttribute = Builder.GetProductAttributeWithDiscreteValue("secondAttrb", new List<AttributeOption>() {"1", "2", "3"});
            product.AddAttribute(secondAttribute);

            var thirdAttribute = Builder.GetProductAttributeWithContinuousValue("thirdAttrb", 10, 20);
            product.AddAttribute(thirdAttribute);

            var attrbs = new[]
            {
                new SelectedAttribute("firstAttrb", "ANY"),
                new SelectedAttribute("secondAttrb", "ANY"),
                new SelectedAttribute("thirdAttrb", "ANY"),
            };
            var productCombination = Builder.GetProductCombination(new[] {"firstAttrb", "secondAttrb", "thirdAttrb"},
                new string[] {"ANY", "ANY", "ANY"});

            _sut.EnsureCombinationContainsAllAttributes(productCombination, product);
            _sut.EnsureCombinationContainsValidOptions(productCombination, product)
                .Should().BeTrue();
        }
    }
}