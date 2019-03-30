using System.Collections.Generic;
using Domain.Common.Domain;

namespace Domain.Customer
{
    public class SellSignUp: ValueObject
    {
        public string SignupEmail { get; }

        public SellSignUp(string signupEmail)
        {
            SignupEmail = signupEmail;
        }
        public override IEnumerable<object> GetMembersForEqualityComparision()
        {
            yield return SignupEmail;
        }
    }
}