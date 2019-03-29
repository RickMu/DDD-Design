using Domain.Common.Exception;
using Domain.Product;
using DomainTests.Common;
using FluentAssertions;
using Xunit;

namespace DomainTests.ProductTests
{
    public class ProductTests
    {
        private Product product;

        public ProductTests()
        {
            product = Builder.GetProduct(1, "firstAttrb", new string[] {"1", "2", "3"});
        }

        [Fact]
        public void CannotAdd_AnotherAttribute_OfSameName()
        {
            var duplicateAttrb = Builder.GetProductAttribute("firstAttrb", new string[] {"value"});
            product.Invoking(product =>
                    product.AddAttribute(duplicateAttrb))
                .Should().Throw<DomainException>().Where(e => e.Message.Contains($"{duplicateAttrb}"));
        }
    }
}