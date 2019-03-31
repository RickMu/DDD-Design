using System.Collections.Generic;
using System.Linq;
using Domain.Common.Domain;
using Domain.Customer;

namespace Domain.ProductSells
{
    public class ProductCombination: EntityWithCompositeIdentity
    {
        //TO-DO Concern on large amount of SellSignUps. probably would be better if it's an external cache
        public IList<SellSignup> SellSignUps { get; }
        public IEnumerable<SelectedAttribute> Combination { get; }

        public ProductCombination(IList<SelectedAttribute> combination, IList<SellSignup> sellSignUps = null)
        {
            SellSignUps = sellSignUps ?? new List<SellSignup>();
            Combination = combination;
        }
        
        public bool IsBaseCombination()
        {
            return Combination.All(x => x.SelectedOption.Equals(ProductAttributeOption.ANY));
        }
        public void AddSellSignUp(SellSignup signup)
        {
            AssertionConcerns.AssertArugmentNotNull(signup, "SellSignUp cannot be null");
            AssertionConcerns.AssertArgumentNotIn(signup,SellSignUps, $"Sign up with Email:{signup.SignupEmail} already exists");
            SellSignUps.Add(signup);
        }
        
        public override IEnumerable<object> GetCompositeComponents()
        {
            return Combination;
        }
    }
}