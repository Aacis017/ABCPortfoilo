using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ABCPortfolio.Models
{
    public class ContactUsViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public ServiceType ServiceType { get; set; }

        [Required]
        public long PhoneNo { get; set; }

        public string Message { get; set; }
    }
    public enum ServiceType
    {
        [Description("This is service 1")]
        service1,
        [Description("This is service 2")]
        service2,

    }
}


