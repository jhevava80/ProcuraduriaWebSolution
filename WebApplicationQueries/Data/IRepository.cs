using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationSearch.Data.Entities;

namespace WebApplicationSearch.Data
{
    public interface IRepository
    {
        void AddJob(Job job);
        
        Job GetJob(int id);
        
        IEnumerable<Job> GetJobs();
        
        bool JobExists(int id);
        
        void RemoveJob(Job job);
        Task<bool> SaveAllAsync();
        
        void UpdateJob(Job job);
    }
}