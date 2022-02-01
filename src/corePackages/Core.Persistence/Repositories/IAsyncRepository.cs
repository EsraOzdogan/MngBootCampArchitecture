using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repositories
{
    public interface IAsyncRepository
    {
        public interface IAsyncRepository<T> where T : class // Generic
        {
            //Bunlari ezberlemeye gerek yok, bakip yapilacak bir sey
            //Linq, Predicate, Expression, Func
            Task<T> GetAsync(Expression<Func<T, bool>> expression); //GetAsyn(m==>m.id=1 & m>10) bunu yapmamizi sagliyor. m==>m.id vs Espression demek //--> tek data getiriyor
            Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, //Sayfalama icin //Select * from colors.id
                                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,   //--> en cok kullanılan orderby icin 
                                  Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, // --> Join icin //IIncludableQueryable --> EntityFramewrokCore'dan geldi
                                  int index = 0,
                                  int size = 10,
                                  bool enablTracking = true,
                                  CancellationToken cancellationToken = default);       

           //Expression<Func<T, bool>> predicate=null --> Link Expression
           // Func<IQueryable<T>, IOrderedQueryable<>> --> Sorgularimiz
           //orderBy=null --> Defaultu null


            IQueryable<T> Query();
            Task<T> AddAsync(T Entity);
            Task UpdateAsync(T Entity);  
            Task DeleteAsync(T Entity);
        }
    }
}
