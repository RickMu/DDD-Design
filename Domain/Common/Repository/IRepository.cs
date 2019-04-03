using Domain.Common.Domain;

namespace Domain.Common.Repository
{
    public interface IRepository <TEntity>
        where TEntity: IEntity
    {
        TEntity FindById(Identity id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        IUnitOfWork UnitOfWork { get; }
    }
}