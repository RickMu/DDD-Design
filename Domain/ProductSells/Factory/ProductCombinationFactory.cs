using System.Collections.Generic;
using Domain.Common.Domain;
using Domain.Products;
using Domain.ProductSells.EntityValidator;

namespace Domain.ProductSells.Factory
{
    public class ProductCombinationFactory: IProductCombinationFactory
    {
        public class Reasons
        {
            public const string InvalidOptions = "Invalid Options for Given Product";
        }
        private readonly IProductCombinationValidator _validator;

        public ProductCombinationFactory(IProductCombinationValidator validator)
        {
            _validator = validator;
        }

        public ProductCombination Create( Product product,ProductPrice price, IList<SelectedAttribute> attributes)
        {
            var productCombination = new ProductCombination(price, attributes);
            var containsValidOptions = _validator.EnsureCombinationContainsValidOptions(productCombination, product);
            AssertionConcerns.AssertArgumentToBeTrue(containsValidOptions, $"{Reasons.InvalidOptions}: given product {product}");
            return productCombination;
        }
    }
}