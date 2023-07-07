using System.ComponentModel.DataAnnotations;

namespace ABCPortfolio.ViewModel
{
    public class UpdateTeamViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Post { get; set; }

        public IFormFile? NewImage { get; set; }

        public string? ExistingImagePath { get; set; }
    }
}

