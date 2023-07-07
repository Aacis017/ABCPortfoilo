using ABCPortfolio.Models;

namespace ABCPortfolio.ViewModel
{
    public class AboutUsViewModel
    {
        public IList<Team> teams { get; set; }
        public Projects? projects { get; set; }

    }
}
