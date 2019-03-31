using System.Collections.Generic;
using Domain.Common.Domain;

namespace Domain.Customer
{
    public class SellSignup: ValueObject
    {
        public string SignupEmail { get; }

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