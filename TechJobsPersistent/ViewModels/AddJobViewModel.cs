using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        [Required]
        public string JobName { get; set; }
        [Required]
        public int EmployerId { get; set; }
        public List<SelectListItem> Employers { get; set;  }
        public List<Skill> Skills { get; set; }
        public AddJobViewModel(List<Employer> employers, List<Skill> skills)
        {
            Employers = new List<SelectListItem>();
            Skills = skills;

            foreach (var employer in employers)
            {
                Employers.Add(new SelectListItem
                {
                    Value = employer.Id.ToString(),
                    Text = employer.Name
                });
            }
            return;
        }

        public AddJobViewModel()
        {
        }
    }
}
