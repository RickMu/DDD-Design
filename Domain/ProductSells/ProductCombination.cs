using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Domain.Common.Domain;
using Domain.Customer;
using Domain.ProductAttributes;
using Domain.ProductAttributes.Factory;

namespace Domain.ProductSells
{
    //TODO - NotSure what it's type is supposed to be Entity or ValueObject
    public class ProductCombination: Entity
    {
        public class Reasons
        {
            public const string NotNullable = "Not Nullable";
        }
        public IEnumerable<SelectedAttribute> SelectedAttributes { get; }
        
        public long SignupCount { get; private set; }
        
        public ProductPrice ProductPrice { get; private set; }

        public ProductCombination()
        {
            SelectedAttributes = new List<SelectedAttribute>();
        }
        
        public ProductCombination(ProductPrice productPrice, IList<SelectedAttribute> combination)
        {
            ProductPrice = productPrice;
            SelectedAttributes = combination;
        }
        
        public bool IsBaseCombination()
        {
            return SelectedAttributes.All(x => x.SelectedOption.Equals(AttributeOption.AnyValue));
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
    }
}