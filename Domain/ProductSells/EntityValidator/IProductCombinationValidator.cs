using Domain.Products;

namespace Domain.ProductSells.EntityValidator
{
    public interface IProductCombinationValidator
    {
        bool EnsureCombinationContainsAllAttributes(ProductCombination productCombination, Product product);
        bool EnsureCombinationContainsValidOptions(ProductCombination productCombination, Product product);
    }
}