using System.ComponentModel.DataAnnotations;

namespace ABCPortfolio.ViewModel
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile ImagePath { get; set; }
        [Required]
        public string Post { get; set; }
    }
}
