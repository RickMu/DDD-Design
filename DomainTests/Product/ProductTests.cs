using System;
using Domain.Common.Exception;
using Domain.ProductAttributes.Factory;
using Domain.Products;
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
            var duplicateAttrb = Builder.GetProductAttributeWithDiscreteValue("firstAttrb", new AttributeOption[] {"value"});
            var product = this.product();
            
            product.Invoking(x =>
                    x.AddAttribute(duplicateAttrb))
                .Should().Throw<DomainException>().Where(e => e.Message.Contains($"{duplicateAttrb}"));
        }
        
        [Fact]
        public void AddProductAttribute_Should_Work()
        {
            var secondAttrb = Builder.GetProductAttributeWithDiscreteValue("secondAttrb", new AttributeOption[] {"value"});
            var product = this.product();
            product.AddAttribute(secondAttrb);    
        }
    }
}