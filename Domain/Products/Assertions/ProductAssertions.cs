using System.Linq;
using System.Xml.Linq;
using Domain.Common.Domain;
using Domain.ProductSells;

namespace Domain.Products.Assertions
{
    public static class ProductAssertions
    {
        //Potentially bad performance
        public static void AssertProductCombinationsContainAllValidProductAttributes(ProductSell productSell, Product product, string msg)
        {
            foreach (var productCombination in productSell.Combinations)
            {
                foreach (var attribute in product.Attributes)
                {
                    var attrbInCombinationExists = productCombination.SelectedAttributes
                        .Any(x => x.Name.Equals(attribute.Name) &&
                                attribute.checkIsValidOption(x.SelectedOption));
                    AssertionConcerns.AssertArgumentToBeTrue(attrbInCombinationExists, $"{msg}, Attribute: {attribute}");
                }
            }
        }
    }
}