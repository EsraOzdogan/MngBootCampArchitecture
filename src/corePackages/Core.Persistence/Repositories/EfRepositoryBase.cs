using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
           return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IPaginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, 
                                                          Func<IQueryable<TEntity>,
                                                          IOrderedQueryable<TEntity>> orderBy = null, 
                                                          Func<IQueryable<TEntity>, 
                                                          IIncludableQueryable<TEntity, 
                                                          object>> include = null, 
                                                          int index = 0, 
                                                          int size = 10, 
                                                          bool enablTracking = true, 
                                                          CancellationToken cancellationToken = default)
        {
           /*return predicate == null;
            ? await Context.Set<TEntity>().ToListAsync(include,) //-->ToListAsyncs--oldugu gibi datayi döndür*/
           IQueryable<TEntity> queryable = Query(); //istek bitene kadar aql sorgusu bitene kadar bekliyor 
            if (!enablTracking) 
                queryable = queryable.AsNoTracking(); //eger kullanici enableTrackingi false göndermisse (yani göndermemisse) queryableyi kapatiyoruz
            if (include!=null) 
                queryable = include(queryable);  //eger join edilcek bir sey yoksa include da gönderileni gönder
            if (predicate != null) 
                queryable = queryable.Where(predicate);

            if (orderBy  != null)
                return await orderBy(queryable).ToPaginateAsync(index,size,0, cancellationToken);  ///Elimde br quwryable var bunu paginatee cevirmek icin extent yapıyoruz
            
            
            return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
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
