using System.Threading.Tasks;
using Domain.Common.Domain;
using Domain.Products;

namespace Domain.Common.Repository
{
    public interface IRepository <TEntity>
        where TEntity: IEntity
    {
        Task<TEntity> FindById(string id);
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        IUnitOfWork UnitOfWork { get; }
    }
}