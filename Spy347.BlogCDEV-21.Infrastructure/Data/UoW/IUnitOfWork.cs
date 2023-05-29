using Spy347.BlogCDEV_21.Infrastructure.Data.Repository;

namespace Spy347.BlogCDEV_21.Infrastructure.Data.UoW
{
    public interface IUnitOfWork: IDisposable
    {
        int SaveChanges(bool ensureAutoHistory = false);

        IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class;
    }
}
