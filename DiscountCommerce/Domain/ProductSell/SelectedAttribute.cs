using System.Collections.Generic;
using Domain.Common.Domain;

namespace Domain
{
    public class SelectedAttribute: ValueObject
    {
        public string Name{ get;  }
        public ProductAttributeOption SelectedOption { get; }

        public SelectedAttribute(string name, ProductAttributeOption selectedOption)
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