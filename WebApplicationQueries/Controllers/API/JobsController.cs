using System;
using Microsoft.AspNetCore.Mvc;
using WebApplicationSearch.Data;
using System.Linq;

namespace WebApplicationSearch.Controllers.API
{
    [Route("api/[Controller]")]
    public class JobsController : Controller
    {
        private readonly IJobRepository jobRepository;
        public JobsController(IJobRepository jobRepository)
        {
            this.jobRepository = jobRepository;
        }

        [HttpGet]
        public IActionResult GetJobs()
        {
            return Ok(this.jobRepository.GetAllWithUsers());
        }        
    }
}
