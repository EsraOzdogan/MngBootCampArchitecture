using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Core.Persistence.Repositories.IAsyncRepository;

namespace Core.Persistence.Repositories
{
    public class EfRepositoryBase<TEntity,TContext>:IAsyncRepository<TEntity>  //TEntity-->Veri tabani nesneleri,TCpntext-->Hybernet
         where TEntity : Entity
        where TContext : DbContext
    {
        protected TContext Context { get; }//Contextte sadece onu inherate eden kullansin


        public EfRepositoryBase(TContext context) //injection
        {
            Context = context;
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IPaginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int index = 0, int size = 10, bool enablTracking = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Query()
        {
            return Context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }


    }
}
