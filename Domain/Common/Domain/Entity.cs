using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Common.Domain
{
    public abstract class IEntity
    {
        [NotMapped]
        public IList<DomainEvent> DomainEvents { get; private set; }

        protected void AddDomainEvents(DomainEvent domainEvent)
        {
            DomainEvents = DomainEvents ?? new List<DomainEvent>();
            DomainEvents.Add(domainEvent);
        }
        
        public virtual string Identity { get; protected set; }
    }
    
    public class Entity: IEntity
    {   
        private Entity()
        {
        }

        public Entity(string id=null)
        {
            Identity = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
        }
    }
    
    public abstract class AggregateEntity: IEntity 
    {
    }
}