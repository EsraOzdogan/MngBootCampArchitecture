using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BrandRapository : EfRepositoryBase<Brand, BaseDbContext>, IBrandRepository
    {
        public BrandRapository(BaseDbContext context) : base(context)
        {
        }
    }
}
