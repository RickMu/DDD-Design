using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Common.Domain
{
    public abstract class IEntity
    {
        public IList<DomainEvent> DomainEvents;

        protected void AddDomainEvents(DomainEvent domainEvent)
        {
            DomainEvents = DomainEvents ?? new List<DomainEvent>();
            DomainEvents.Add(domainEvent);
        }
    }
    
    public class Entity: IEntity
    {
        public Identity Id { get; }

        public Entity()
        {
            Id = new Identity(Guid.NewGuid().ToString());
        }

        public Entity(string id)
        {
            Id = new Identity(id);
        }
    }
}