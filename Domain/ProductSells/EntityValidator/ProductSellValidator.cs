using Domain.Common.Domain;
using Domain.Products;

namespace Domain.ProductSells.EntityValidator
{
    public class ProductSellValidator: IProductSellValidator
    {
        private readonly IProductCombinationValidator _productCombinationValidator;

        public ProductSellValidator(IProductCombinationValidator _productCombinationValidator)
        {
            this._productCombinationValidator = _productCombinationValidator;
        }

        public bool IsProductSellReleasable(ProductSell productSell, Product product)
        {
            foreach (var productComibination in productSell.Combinations)
            {
                var containsAllAttrbs =
                    _productCombinationValidator.EnsureCombinationContainsAllAttributes(productComibination, product);
                var containsValidOptions =
                    _productCombinationValidator.EnsureCombinationContainsValidOptions(productComibination, product);

                if (!(containsAllAttrbs && containsValidOptions))
                {
                    return false;
                }
            }
            return true;
        }
    }
}