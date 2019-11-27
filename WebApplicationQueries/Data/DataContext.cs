using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationSearch.Data.Entities;

namespace WebApplicationSearch.Data
{
    //Normal dbcontext
    //public class DataContext : DbContext

    //Include udentity user
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Service> Services { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Job>()
            //    .Property(p => p.Price)
            //    .HasColumnType("decimal(18,2)");

            // Desabilitar borrado en cascada
            var cascadeFKs = builder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);
        }
    }
}
