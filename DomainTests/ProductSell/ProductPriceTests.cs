using System.Collections.Immutable;
using Domain;
using Domain.ProductSells;
using DomainTests.Common;
using FluentAssertions;
using Xunit;

namespace ProductSellsTests
{
    public class ProductPriceTests
    {
        private ProductPrice productPrice;

        public ProductPriceTests()
        {
            productPrice = Builder.GetProducePrice((decimal)1, 100,20);
        }

        [Theory]
        [InlineData(10, 90)]
        [InlineData(100, 20)]
        [InlineData(4, 96)]
        public void CalculateProductPrice_Should_ReturnExpected(int count, decimal expected)
        {
            var newPrice = productPrice.CalculatePrice(count);

            newPrice.Should().Be((decimal) expected);
        }
    }
}