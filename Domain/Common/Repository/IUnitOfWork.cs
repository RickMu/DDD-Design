using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Common.Repository
{
    public interface IUnitOfWork: IDisposable
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}