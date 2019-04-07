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
        
        public abstract string Identity { get; }
    }
    
    public class Entity: IEntity
    {
         
        public string Id { get; protected set; }
        
        public Entity()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Entity(string id)
        {
            Id = id;
        }

        public override string Identity => Id;
    }
    
    public abstract class AggregateEntity: IEntity 
    {
        
    }
}