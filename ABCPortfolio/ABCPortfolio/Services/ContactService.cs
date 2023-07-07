using ABCPortfolio.Data;
using ABCPortfolio.Models;

namespace ABCPortfolio.Services
{
    public class ContactService : IContactService
    {
        
        private readonly AppDBContext _service;

        public ContactService(AppDBContext service) 
        {
           
            _service = service;
        }

        public int Contact(ContactUsViewModel contactUsViewModel)
        {

            _service.ContactUsTable.Add(contactUsViewModel);
            return _service.SaveChanges();
            
        }

        public ICollection<ContactUsViewModel> GetContact()
        {
            return _service.ContactUsTable.ToList();
        }
    }
}
