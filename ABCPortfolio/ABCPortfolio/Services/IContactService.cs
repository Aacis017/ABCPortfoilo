using ABCPortfolio.Models;

namespace ABCPortfolio.Services
{
    public interface IContactService
    {
        int Contact(ContactUsViewModel contactUsViewModel);

        //for view page

        ICollection<ContactUsViewModel> GetContact();
      

    }
}
