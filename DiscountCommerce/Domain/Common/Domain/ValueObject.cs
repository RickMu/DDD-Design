using System.Collections.Generic;
using System.Linq;

namespace Domain.Common.Domain
{
    public abstract class ValueObject
    {
        public abstract IEnumerable<object> GetMembersForEqualityComparision();
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return true;
            if (object.ReferenceEquals(null, obj)) return false;
            if (this.GetType() != obj.GetType()) return false;

            var o = obj as ValueObject;
            return GetMembersForEqualityComparision().SequenceEqual(o.GetMembersForEqualityComparision());
        }
    }
}