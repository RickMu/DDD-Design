using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Domain.Common.Domain;
using Domain.Common.Exception;

namespace Domain
{
    public class ProductSell: Entity
    {
        public IReadOnlyList<ProductCombinationDiscount> CombinationDiscounts => _combinationDiscounts.AsReadOnly();
        //ProductSell with a base combination is releasable. 
        private bool _isReleasable;
        private List<ProductCombinationDiscount> _combinationDiscounts;

        public ProductSell(List<ProductCombinationDiscount> combinationDiscounts, bool isReleasable = false)
        {
            _isReleasable = isReleasable;
            combinationDiscounts.ForEach(AddProductCombination);
        }
        
        public void AddProductCombination(ProductCombinationDiscount combinationDiscount)
        {
            if (_combinationDiscounts.Contains(combinationDiscount))
            {
                throw new DomainException($"Product Combination already exists: {_combinationDiscounts}");
            }
            
            _combinationDiscounts.Add(combinationDiscount);
            CheckAndSetReleasable(combinationDiscount);
        }

        private void CheckAndSetReleasable(ProductCombinationDiscount combinationDiscount)
        {
            if (!_isReleasable)
            {
                _isReleasable = combinationDiscount.IsBaseCombination();
            }
        }
    }
}