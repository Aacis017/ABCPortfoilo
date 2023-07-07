using ABCPortfolio.ViewModel;

namespace ABCPortfolio.Services.IServices
{
    public interface IserviceServices
    {
        public List<ServiceVM> GetServices();
        public bool AddServices(ServiceCreateVM service);
        public bool DeleteServices( int id);
        public bool UpdateService(ServiceCreateVM service);
    }
}
