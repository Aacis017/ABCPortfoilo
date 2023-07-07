using System.ComponentModel.DataAnnotations;

namespace ABCPortfolio.ViewModel
{
    public class ServiceVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public string? ImageName { get; set; }
    }
    public class ServiceCreateVM
    {
        public int Id { get; set; }
        public IFormFile? Images { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
    public class ParentServiceVM
    {
     public IEnumerable<ServiceVM> getService { get; set; }
     public ServiceCreateVM createService { get; set; }
    }
}
