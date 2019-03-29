using System.Collections.Generic;
using Domain.Common.Domain;

namespace Domain.Customer
{
    public class Alarm: EntityWithCompositeIdentity
    {
        public override IEnumerable<object> GetCompositeComponents()
        {
            throw new System.NotImplementedException();
        }
    }
}