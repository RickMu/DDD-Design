using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Domain.Common.Domain;
using Domain.Customer;
using Domain.ProductAttributes;

namespace Domain.ProductSells
{
    //TODO - NotSure what it's type is supposed to be Entity or ValueObject
    public class ProductCombination: AggregateEntity
    {
        public class Reasons
        {
            public const string NotNullable = "Not Nullable";
        }
        public IEnumerable<SelectedAttribute> Combination { get; }
        
        public long SignupCount { get; private set; }
        
        public ProductPrice ProductPrice { get; private set; }

        public ProductCombination()
        {
            Combination = new List<SelectedAttribute>();
        }
        
        public ProductCombination(IList<SelectedAttribute> combination)
        {
            Combination = combination;
        }
        
        public bool IsBaseCombination()
        {
            return Combination.All(x => x.SelectedOption.Equals("ANY"));
        }

        public void AddSignupCount()
        {
            SignupCount++;
        }

        public void CancelSignup()
        {
            SignupCount--;
        }

        public void SetProductDiscountAndPrice(ProductPrice price)
        {
            AssertionConcerns.AssertArugmentNotNull(price, $"{Reasons.NotNullable}: ProductPrice cannot be set to null");
            ProductPrice = price;
        }

        public decimal GetCurrentPrice()
        {
            return ProductPrice.CalculatePrice(SignupCount);
        }

        public override string Identity => Combination.SelectMany(x => $"{x.Name}{x.SelectedOption}").ToString().GetHashCode().ToString();
    }
}