using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationSearch.Data.Entities;

namespace WebApplicationSearch.Data
{
    public interface IRepository
    {
        void AddProduct(Job job);
        
        Job GetJob(int id);
        
        IEnumerable<Job> GetJobs();
        
        bool JobExists(int id);
        
        void RemoveProduct(Job job);
        Task<bool> SaveAllAsync();
        
        void UpdateProduct(Job job);
    }
}