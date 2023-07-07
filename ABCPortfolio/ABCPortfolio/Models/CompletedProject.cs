using System.ComponentModel.DataAnnotations;

namespace ABCPortfolio.Models
{
    public class CompletedProject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Imageforproject { get; set; }
    }
}
