using System.Collections.Generic;
using System.Linq;
using Domain.Common.Domain;
using Domain.Products;
using Domain.ProductSells;

namespace Domain.Service
{
    public class Reasons
    {
        public const string CantFind = "Can't Be Found";
    }
    
    public class ProductService
    {
        public bool AreAttributesValidProductChoice(IEnumerable<SelectedAttribute> combination, Product product)
        {
            if (!AreAttributeNamesSame(combination, product)) return false;
            
            foreach (var attribute in product.Attributes)
            {
                var combinationAttribute = combination.First(x => x.Name == attribute.Name);
                if (!attribute.isValidOption(combinationAttribute.SelectedOption))
                {
                    return false;
                }
            }
            return true;
        }

        private bool AreAttributeNamesSame(IEnumerable<SelectedAttribute> combination, Product product)
        {
            var combinationNames = combination.Select(x => x.Name);
            var attributesNames = product.Attributes.Select(x => x.Name);
            //Are AttributeNames the same
            if (combinationNames.Except(attributesNames).Any() || attributesNames.Except(combinationNames).Any())
            {
                return false;
            }
            return true;
        }

        public bool AreAttributeCombinationBaseCombination(IEnumerable<SelectedAttribute> combination, Product product)
        {
            if (!AreAttributeNamesSame(combination, product)) return false;   
            
            foreach (var selectedAttribute in combination)
            {
                var attribute = product.Attributes.First(x => x.Name == selectedAttribute.Name);
                AssertionConcerns.AssertArugmentNotNull(attribute, 
                    $"{Reasons.CantFind}: Attribute of Name {selectedAttribute.Name} can't be found");
                if (!attribute.isBaseAttribute(selectedAttribute.SelectedOption))
                {
                    return false;
                }
            }
            return true;
        }
    }
}