using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationSearch.Data.Entities;

namespace WebApplicationSearch.Data
{
    public class MockRepository : IRepository
    {
        public void AddJob(Job job)
        {
            throw new NotImplementedException();
        }

        public Job GetJob(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Job> GetJobs()
        {
            var jobs = new List<Job>();
            jobs.Add(new Job { Name = "Prueba Mock job", Id = 1 });
            jobs.Add(new Job { Name = "Prueba Mock job", Id = 2 });
            jobs.Add(new Job { Name = "Prueba Mock job", Id = 3 });
            return jobs;
        }

        public bool JobExists(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveJob(Job job)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateJob(Job job)
        {
            throw new NotImplementedException();
        }
    }
}
