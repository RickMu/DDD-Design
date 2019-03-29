using System.Collections.Generic;
using System.Linq;
using Domain.Common.Domain;
using Domain.Common.Exception;
using Domain.Customer;
using Newtonsoft.Json;

namespace Domain
{
    public class ProductCombinationDiscount : EntityWithCompositeIdentity
    {
        public IEnumerable<SelectedAttribute> Combination { get; }
        public IList<SellSignUp> SellSignUps { get; }
        public decimal SignupDiscount { get; private set; }
        public decimal Price { get; private set; }
        
        public ProductCombinationDiscount(IEnumerable<SelectedAttribute> combination, decimal signupDiscount, decimal price)
        {
            Combination = combination;
            SignupDiscount = SignupDiscount;
            Price = price;
        }

        public bool IsBaseCombination()
        {
            return Combination.All(x => x.SelectedOption.Value == "Any");
        }

        public void AddSellSignUp(SellSignUp signUp)
        {
            if (SellSignUps.Contains(signUp))
            {
                throw new DomainException($"Sign up with Email:{signUp.SignupEmail} already exists");
            }
            SellSignUps.Add(signUp);
        }

        private void AdjustPrice(bool decrease)
        {
            if (decrease)
            {
                var newPrice = Price - SignupDiscount;
                Price = newPrice;
            }
            else
            {
                var newPrice = Price + SignupDiscount;
                Price = newPrice;
            }
        }
        
        public override string ToString()
        {
            var combinations = Combination.ToDictionary(x => (x.Name,x.SelectedOption.Value));
            return JsonConvert.SerializeObject(combinations);
        }

        public override IEnumerable<object> GetCompositeComponents()
        {
            return Combination;
        }
    }
}