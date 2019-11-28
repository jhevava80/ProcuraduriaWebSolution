using System;
using Microsoft.AspNetCore.Mvc;
using WebApplicationSearch.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApplicationSearch.Controllers.API
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
