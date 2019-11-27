using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationSearch.Data.Entities;

namespace WebApplicationSearch.Data
{
    public class StatusRepository : GenericRepository<Status>,IStatusRepository
    {
        public StatusRepository(DataContext context) :base(context)
        {

        }
    }
}
