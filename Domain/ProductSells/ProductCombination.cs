using System.Collections.Generic;
using System.Linq;
using Domain.Common.Domain;
using Domain.Customer;
using Domain.ProductAttributes;

namespace Domain.ProductSells
{
    //TODO - NotSure what it's type is supposed to be Entity or ValueObject
    public class ProductCombination: ValueObject
    {
        public IEnumerable<SelectedAttribute> Combination { get; }

        public ProductCombination(IList<SelectedAttribute> combination)
        {
            Combination = combination;
        }
        
        public bool IsBaseCombination()
        {
            return Combination.All(x => x.SelectedOption.Equals(ProductAttributeOption.ANY));
        }

        public override IEnumerable<object> GetMembersForEqualityComparision()
        {
            return Combination;
        }
    }
}