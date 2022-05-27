using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistent.Controllers
{
    public class EmployerController : Controller
    {
        private JobDbContext context;

        public EmployerController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Employer> employers = context.Employers.ToList();

            return View(employers);
        }

        public IActionResult Add()
        {
            AddEmployerViewModel addEmployerViewModel = new AddEmployerViewModel();
            return View("Add", addEmployerViewModel);
        }

        [HttpPost]
        public IActionResult ProcessAddEmployerForm(AddEmployerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Employer employerModel = new Employer 
                { 
                    Name = viewModel.Name,
                    Location = viewModel.Location,
                };
                context.Employers.Add(employerModel);
                context.SaveChanges();
                return Redirect("/Employer");
            }
            return View("Add", viewModel);
        }

        public IActionResult About(int id)
        {
            Employer employer = context.Employers.Find(id);

            return View(employer);
        }
    }
}
