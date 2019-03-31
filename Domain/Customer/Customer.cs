using System.Collections.Generic;
using Domain.Common.Domain;

namespace Domain.Customer
{
    public class Customer: Entity
    {
        public class Reasons
        {
            public const string DUPLICATE = "Duplicate";
        }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public IList<ActiveSignup> ActiveSignups { get; }
        public Customer(string firstName, string lastName, string email, IList<ActiveSignup> signups)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ActiveSignups = signups ?? new List<ActiveSignup>();
        }

        public void AddProductSellSignup(ActiveSignup signup)
        {
            AssertionConcerns.AssertArgumentNotIn(signup, ActiveSignups, $"{Reasons.DUPLICATE}: Cannot add a duplicate ActiveSignups");
            ActiveSignups.Add(signup);
        }
    }
}