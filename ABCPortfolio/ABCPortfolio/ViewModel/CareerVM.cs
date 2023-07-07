using ABCPortfolio.Models;

namespace ABCPortfolio.ViewModel
{
    public class CareerVM 
    {
     
    public string Title { get; set; }
    
    public IFormFile? CreateImage { get; set; }
    
    }
    public class QualificationVM
    {
        public int Id { get; set; }
        public string QualificationText { get; set; }
    }
    public class JobVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public IQueryable<QualificationVM> Qualification { get; set; }

    }
    public class ParentVM
    {
        public string? requirement { get; set; }
        public CareerVM career { get; set; }
        public IQueryable<JobVM> job { get;set; }
    }
}
