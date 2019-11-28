using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationSearch.Data;
using WebApplicationSearch.Data.Entities;
using WebApplicationSearch.Helpers;

namespace WebApplicationSearch.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private readonly IJobRepository jobRepository;
        private readonly IUserHelper userHelper;

        public JobsController(IJobRepository jobRepository, IUserHelper userHelper)
        {
            this.jobRepository = jobRepository;
            this.userHelper = userHelper;
        }

        // GET: Jobs
        public IActionResult Index()
        {
            return View(this.jobRepository.GetAll().OrderBy(j => j.Name));
        }

        // GET: Jobs/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = this.jobRepository.GetByIdAsync(id.Value);
            if (job == null )
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        [Authorize(Roles = "Admin,Root,Super")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Job job)
        {
            if (ModelState.IsValid)
            {
                job.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await this.jobRepository.CreateAsync(job);
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        // GET: Jobs/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = this.jobRepository.GetByIdAsync(id.Value);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Job job)
        {
            if (id != job.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await this.jobRepository.UpdateAsync(job);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await JobExists(job.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        // GET: Jobs/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = this.jobRepository.GetByIdAsync(id.Value);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = this.jobRepository.GetByIdAsync(id);
            //await this.jobRepository.DeleteAsync(job);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> JobExists(int id)
        {
            return await this.jobRepository.ExistAsync(id);
        }
    }
}
