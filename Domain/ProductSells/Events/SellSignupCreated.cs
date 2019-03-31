using Domain.Common.Domain;
using Domain.Customer;

namespace Domain.ProductSells.Events
{
    public class SellSignupCreated: DomainEvent
    {
        public string ProductSellId { get; }
        public ProductCombination ProductCombination { get; }
        public SellSignup Signup { get; }

        public SellSignupCreated(string productSellId, ProductCombination productCombination, SellSignup signup)
        {
            ProductSellId = productSellId;
            ProductCombination = productCombination;
            Signup = signup;
        }
    }
}