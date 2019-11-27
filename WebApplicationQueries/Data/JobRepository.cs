using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationSearch.Data.Entities;

namespace WebApplicationSearch.Data
{
    public class JobRepository : GenericRepository<Job> , IJobRepository 
    {
        private readonly DataContext context;

        public JobRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return this.context.Jobs.Include(j=> j.User );
        }
    }
}
