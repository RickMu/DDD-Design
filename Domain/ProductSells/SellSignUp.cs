using System.Collections.Generic;
using Domain.Common.Domain;

namespace Domain.Customer
{
    public class SellSignup: ValueObject
    {
        public string SignupEmail { get; private set; }
        
        private SellSignup(){}

        public SellSignup(string signupEmail)
        {
            SignupEmail = signupEmail;
        }
        public override IEnumerable<object> GetMembersForEqualityComparision()
        {
            yield return SignupEmail;
        }
    }
}