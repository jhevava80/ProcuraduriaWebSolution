using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationSearch.Data.Entities;

namespace WebApplicationSearch.Data
{
    public class Repository : IRepository
    {
        public readonly DataContext context;
        public Repository(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Job> GetJobs()
        {
            return this.context.Jobs.OrderBy(j => j.Name);
        }

        public Job GetJob(int id)
        {
            return this.context.Jobs.Find(id);
        }

        public void AddProduct(Job job)
        {
            this.context.Jobs.Add(job);
        }

        public void UpdateProduct(Job job)
        {
            this.context.Jobs.Update(job);
        }

        public void RemoveProduct(Job job)
        {
            this.context.Jobs.Remove(job);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        public bool JobExists(int id)
        {
            return this.context.Jobs.Any(j => j.Id == id);
        }
    }
}
