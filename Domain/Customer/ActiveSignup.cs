using System.Collections.Generic;
using Domain.Common.Domain;
using Domain.ProductSells;

namespace Domain.Customer
{
    public class ActiveSignup: EntityWithCompositeIdentity
    {
        public string ProductSellId;
        public ProductCombination combinationSignedUpFor;
        public override IEnumerable<object> GetCompositeComponents()
        {
            yield return ProductSellId;
        }
    }
}