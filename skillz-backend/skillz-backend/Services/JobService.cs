// JobService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using skillz_backend.Repositories;
using skillz_backend.models;

namespace skillz_backend.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
        }

        public async Task<Job> GetJobByIdAsync(int jobId)
        {
            if (jobId <= 0)
            {
                throw new ArgumentException("JobId should be a positive integer.");
            }

            return await _jobRepository.GetJobByIdAsync(jobId);
        }

        public async Task<List<Job>> GetAllJobsAsync()
        {
            return await _jobRepository.GetAllJobsAsync();
        }

        public async Task<List<Job>> GetJobsByTitleAsync(string jobTitle)
        {
            if (string.IsNullOrEmpty(jobTitle))
            {
                throw new ArgumentException("JobTitle cannot be null or empty.");
            }

            return await _jobRepository.GetJobsByTitleAsync(jobTitle);
        }

        public async Task<List<Job>> GetJobsByUserAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("UserId should be a positive integer.");
            }

            return await _jobRepository.GetJobsByUserAsync(userId);
        }

        public async Task<List<Job>> GetJobsByExperienceAsync(int experiencedYears)
        {
            if (experiencedYears < 0)
            {
                throw new ArgumentException("ExperiencedYears should be a non-negative integer.");
            }

            return await _jobRepository.GetJobsByExperienceAsync(experiencedYears);
        }

        public async Task CreateJobAsync(Job job)
        {
            if (job == null)
            {
                throw new ArgumentNullException(nameof(job), "Job object cannot be null.");
            }

            if (string.IsNullOrEmpty(job.JobTitle))
            {
                throw new ArgumentException("JobTitle cannot be null or empty.");
            }

            if (job.ExperiencedYears < 0)
            {
                throw new ArgumentException("ExperiencedYears should be a non-negative integer.");
            }

            var existingJobById = await _jobRepository.GetJobByIdAsync(job.IdJob);
            if (existingJobById != null)
            {
                throw new InvalidOperationException("A job with the same JobId already exists.");
            }

            await _jobRepository.CreateJobAsync(job);
        }

        public async Task UpdateJobAsync(Job job)
        {
            if (job == null || job.IdJob <= 0)
            {
                throw new ArgumentException("Invalid job object or JobId.");
            }

            if (string.IsNullOrEmpty(job.JobTitle))
            {
                throw new ArgumentException("JobTitle cannot be null or empty.");
            }

            if (job.ExperiencedYears < 0)
            {
                throw new ArgumentException("ExperiencedYears should be a non-negative integer.");
            }

            await _jobRepository.UpdateJobAsync(job);
        }

        public async Task DeleteJobAsync(int jobId)
        {
            if (jobId <= 0)
            {
                throw new ArgumentException("JobId should be a positive integer.");
            }

            await _jobRepository.DeleteJobAsync(jobId);
        }
    }
}
