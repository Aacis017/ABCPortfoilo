

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABCPortfolio.ViewModel
{
    public class CompletedProjectsVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

       

        public IFormFile Imageforproject { get; set; }
    }
}
