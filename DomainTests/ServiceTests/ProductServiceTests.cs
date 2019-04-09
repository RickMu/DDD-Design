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
        private ProductService _sut;

        public ProductServiceTests()
        {
            _sut = new ProductService();
        }
        
        [Theory]
        [InlineData("2", "3", 15, true)]
        [InlineData("-1","5", 15, false)]
        [InlineData("2","3", 5, false)]
        public void AreAttributesCombinationValidChoice_Should_ReturnTrue(string firstOption, string secondOption, double thirdOption, bool expected)
        {
            var product = Builder.GetProduct(1, "firstAttrb", new string[] {"1", "2", "3"});
            
            var secondAttribute = Builder.GetProductAttributeWithDiscreteValue("secondAttrb", new AttributeOption[] {"1","2","3"});
            product.AddAttribute(secondAttribute);

            var thirdAttribute = Builder.GetProductAttributeWithContinuousValue("thirdAttrb", 10, 20);
            product.AddAttribute(thirdAttribute);

            var attrbCombination = Builder.GetListSelectedAttrbs(new string[] {"firstAttrb", "secondAttrb", "thirdAttrb"},
                new string[] {firstOption, secondOption, thirdOption.ToString()});

            _sut.AreAttributesValidProductChoice(attrbCombination, product)
                .Should().Be(expected);
        }
        
        [Fact]
        public void AreAttributeCombinationBaseCombination_And_AreAttributesValidProductChoice_ShouldReturnTrue()
        {
            var product = Builder.GetProduct(1, "firstAttrb", new string[] {"1", "2", "3"});
            
            var secondAttribute = Builder.GetProductAttributeWithDiscreteValue("secondAttrb", new AttributeOption[] {"1","2","3"});
            product.AddAttribute(secondAttribute);

            var thirdAttribute = Builder.GetProductAttributeWithContinuousValue("thirdAttrb", 10, 20);
            product.AddAttribute(thirdAttribute);

            var attrbs = new[]
            {
                new SelectedAttribute("firstAttrb", "ANY"),
                new SelectedAttribute("secondAttrb", "ANY"),
                new SelectedAttribute("thirdAttrb", "ANY"),
            };

            _sut.AreAttributeCombinationBaseCombination(attrbs, product)
                .Should().BeTrue();
            _sut.AreAttributesValidProductChoice(attrbs, product)
                .Should().BeTrue();
        }
    }
}