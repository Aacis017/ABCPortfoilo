using System.ComponentModel.DataAnnotations.Schema;

namespace ABCPortfolio.Models
{
    public class Requirement
    {
        public int Id { get; set; }
        public string Qualification { get; set; }
         
        public int JobId { get; set; }

    }
}