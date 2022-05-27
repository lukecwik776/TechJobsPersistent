using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            List<Employer> employers = context.Employers.ToList();
            List<Skill> skills = context.Skills.ToList();
            AddJobViewModel viewModel = new AddJobViewModel(employers, skills);
            return View("AddJob", viewModel);
        }

        [HttpPost]
        public IActionResult ProcessAddJobForm(AddJobViewModel viewModel, string[] selectedSkills)
        {
            if (ModelState.IsValid)
            {
                Job jobModel = new Job 
                { 
                    Name = viewModel.JobName,
                    EmployerId = viewModel.EmployerId
                };
                context.Jobs.Add(jobModel);
                context.SaveChanges();
                for (int i = 0; i< selectedSkills.Length; i++ )
                {
                    int number = Int32.Parse(selectedSkills[i]);
                    JobSkill jobSkill = new JobSkill
                    {
                        JobId = jobModel.Id,
                        SkillId = number
                    };
                    context.JobSkills.Add(jobSkill);
                }
                context.SaveChanges();
                return Redirect("/Home");
            }
            return View("AddJob", viewModel);
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
