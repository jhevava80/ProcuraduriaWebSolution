using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationSearch.Data.Entities;

namespace WebApplicationSearch.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
