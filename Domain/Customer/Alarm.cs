using System.Collections.Generic;
using Domain.Common.Domain;

namespace Domain.Customer
{
    public class Alarm: AggregateEntity
    {
        public override string Identity { get; }
    }
}