using System.Collections.Generic;
using Domain.Common.Domain;
using Domain.ProductAttributes;

namespace Domain.ProductSells
{
    public class SelectedAttribute: ValueObject
    {
        public string Name{ get;  }
        public string SelectedOption { get; }

        public SelectedAttribute(string name, string selectedOption)
        {
            Name = name;
            SelectedOption = selectedOption;
        }
        public override IEnumerable<object> GetMembersForEqualityComparision()
        {
            yield return Name;
            yield return SelectedOption;
        }
    }
}