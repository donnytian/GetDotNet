using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Gdn.EntityFramework.Repositories
{
    public abstract class GdnRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<GdnDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected GdnRepositoryBase(IDbContextProvider<GdnDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class GdnRepositoryBase<TEntity> : GdnRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected GdnRepositoryBase(IDbContextProvider<GdnDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
