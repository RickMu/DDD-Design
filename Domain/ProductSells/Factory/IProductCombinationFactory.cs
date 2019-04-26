using System.Collections.Generic;
using Domain.Products;

namespace Domain.ProductSells.Factory
{
    public interface IProductCombinationFactory
    {
        ProductCombination Create(Product product, ProductPrice price, IList<SelectedAttribute> attributes);
    }
}