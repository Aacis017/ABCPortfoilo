using ABCPortfolio.ViewModel;

namespace ABCPortfolio.Services.IServices
{
    public interface ICareerServices
    {
        public IQueryable<JobVM> GetJobs();
        public bool AddJob(CareerVM jobData);
        public bool AddRequirement(int id, string requirement);
        public bool DeleteRequirement(int id);
        public bool DeleteJob(int id);
    }
}
