using System;
using System.Linq;

namespace Domain.Common.Domain
{
    public abstract class IEntity
    {
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