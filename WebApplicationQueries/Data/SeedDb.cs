using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationSearch.Data.Entities;
using WebApplicationSearch.Helpers;

namespace WebApplicationSearch.Data
{
    public class SeedDb
    {
        private readonly IUserHelper userHelper;
        private readonly IJobRepository jobRepository;
        private Random random;
        public SeedDb(IUserHelper userHelper, IJobRepository jobRepository)
        {
            this.userHelper = userHelper;
            this.jobRepository = jobRepository;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            //TODO: Crea roles al inicio - Remoe when publish to production
            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");
            await this.userHelper.CheckRoleAsync("Super");
            await this.userHelper.CheckRoleAsync("Root");
                       
            
            //await this.context.Database.EnsureCreatedAsync();
            //if (!this.context.Jobs.Any())
            //{
            //    this.AddJobs("Job");
            //    this.AddJobs("Job");
            //}
        }

        private void AddJobs(string name)
        {
            int num = this.random.Next(300);
            this.jobRepository.CreateAsync(new Job { 
                Name = name + "-" + num,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            });
        }
    }
}
