using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationSearch.Data.Entities;

namespace WebApplicationSearch.Data
{
    public class SeedDb
    {
        private readonly DataContext context;
        private Random random;
        public SeedDb(DataContext context)
        {
            this.context = context;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            if (!this.context.Jobs.Any())
            {
                this.AddJobs("Job");
                this.AddJobs("Job");
            }
        }

        private void AddJobs(string name)
        {
            int num = this.random.Next(300);
            this.context.Jobs.Add(new Job { 
                Name = name + "-" + num,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                State = false
            });
        }
    }
}
