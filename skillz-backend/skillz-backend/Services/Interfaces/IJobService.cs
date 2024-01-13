using System.Collections.Generic;
using System.Threading.Tasks;
using skillz_backend.models;

namespace skillz_backend.Services
{
    public interface IJobService
    {
        Task<Job> GetJobByIdAsync(int jobId);
        Task<List<Job>> GetAllJobsAsync();
        Task<List<Job>> GetJobsByTitleAsync(string jobTitle);
        Task<List<Job>> GetJobsByUserAsync(int userId);
        Task<List<Job>> GetJobsByExperienceAsync(int experiencedYears);
        Task CreateJobAsync(Job job);
        Task UpdateJobAsync(Job job);
        Task DeleteJobAsync(int jobId);
        Task<List<Job>> FilterJobsAsync(string jobTitle, DateTime date, string location);
    }
}
