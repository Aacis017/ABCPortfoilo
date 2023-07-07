using System.ComponentModel.DataAnnotations;

namespace ABCPortfolio.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name ="Image Path")]
        public string ImagePath { get; set; }

    }
}
