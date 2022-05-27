using System.ComponentModel.DataAnnotations;

namespace TechJobsPersistent.ViewModels
{
    public class AddEmployerViewModel
    {
        [Required(ErrorMessage ="Name Field Required")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Location Field Required")]
        public string Location { get; set; }
    }
}
