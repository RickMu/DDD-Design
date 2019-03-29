using System.Collections.Generic;
using System.Linq;

namespace Domain.Common.Domain
{
    public abstract class EntityWithCompositeIdentity: IEntity 
    {
        public abstract IEnumerable<object> GetCompositeComponents();
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return true;
            if (object.ReferenceEquals(null, obj)) return false;
            if (GetType() != obj.GetType()) return false;
            var other = obj as EntityWithCompositeIdentity;
            return GetCompositeComponents().SequenceEqual(other.GetCompositeComponents());
        }
    }
}