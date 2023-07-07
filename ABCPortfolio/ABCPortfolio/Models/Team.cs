using System.ComponentModel.DataAnnotations;

namespace ABCPortfolio.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string ImagePath { get; set; }
        [Required]
        public string Post { get; set; }    
    }
}
