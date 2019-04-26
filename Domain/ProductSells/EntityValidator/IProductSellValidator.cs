using Domain.Products;

namespace Domain.ProductSells.EntityValidator
{
    public interface IProductSellValidator
    {
        bool IsProductSellReleasable(ProductSell productSell, Product product);
    }
}