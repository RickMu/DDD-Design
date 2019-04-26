using System.Collections.Generic;
using System.Linq;
using Domain.Common.Domain;
using Domain.Products;
using Domain.ProductSells;
using Domain.ProductSells.EntityValidator;

namespace Domain.Service
{
    public class Reasons
    {
        public const string CantFind = "Can't Be Found";
    }
    
    public class ProductCombinationValidator: IProductCombinationValidator
    {
        public bool EnsureCombinationContainsAllAttributes(ProductCombination productCombination, Product product)
        {
            var combinationNames = productCombination.SelectedAttributes.Select(x => x.Name);
            var attributesNames = product.Attributes.Select(x => x.Name);
            //Are AttributeNames the same
            if (combinationNames.Except(attributesNames).Any() || attributesNames.Except(combinationNames).Any())
            {
                return false;
            }
            return true;
        }

        public bool EnsureCombinationContainsValidOptions(ProductCombination productCombination, Product product)
        {
            foreach (var selectedAttribute in productCombination.SelectedAttributes)
            {
                var isValidOption = product.Attributes
                    .Any(x => x.Name == selectedAttribute.Name
                              && x.checkIsValidOption(selectedAttribute.SelectedOption));
                
                if (!isValidOption)
                {
                    return false;
                }
            }

            return true;
        }
    }
}