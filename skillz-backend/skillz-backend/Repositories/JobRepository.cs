using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using skillz_backend.data;
using skillz_backend.models;

namespace skillz_backend.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly SkillzDbContext _dbContext;

        public JobRepository(SkillzDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Job> GetJobByIdAsync(int jobId)
        {
            return await _dbContext.Jobs.FindAsync(jobId);
        }

        public async Task<List<Job>> GetAllJobsAsync()
        {
            return await _dbContext.Jobs.ToListAsync();
        }

        public async Task<List<Job>> GetJobsByTitleAsync(string jobTitle)
        {
            return await _dbContext.Jobs.Where(j => j.JobTitle == jobTitle).ToListAsync();
        }

        public async Task<List<Job>> GetJobsByUserAsync(int userId)
        {
            return await _dbContext.Jobs.Where(j => j.IdUser == userId).ToListAsync();
        }

        public async Task<List<Job>> GetJobsByExperienceAsync(int experiencedYears)
        {
            return await _dbContext.Jobs.Where(j => j.ExperiencedYears == experiencedYears).ToListAsync();
        }

        public async Task CreateJobAsync(Job job)
        {
            _dbContext.Jobs.Add(job);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateJobAsync(Job job)
        {
            _dbContext.Entry(job).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteJobAsync(int jobId)
        {
            var jobToDelete = await _dbContext.Jobs.FindAsync(jobId);
            if (jobToDelete != null)
            {
                _dbContext.Jobs.Remove(jobToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}