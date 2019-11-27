using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using WebApplicationSearch.Data.Entities;

namespace WebApplicationSearch.Data
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        IQueryable GetAllWithUsers();
    }
}
