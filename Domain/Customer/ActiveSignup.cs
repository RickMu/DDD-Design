using System.Collections.Generic;
using Domain.Common.Domain;
using Domain.ProductSells;

namespace Domain.Customer
{
    public class ActiveSignup: AggregateEntity
    {
        public string ProductSellId;
        public ProductCombination combinationSignedUpFor;
    }
}