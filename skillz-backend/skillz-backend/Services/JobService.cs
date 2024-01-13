// JobService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using skillz_backend.Repositories;
using skillz_backend.models;
using skillz_backend.Repositories.Interfaces;

namespace skillz_backend.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookingRepository _bookingRepository;

        public JobService(IJobRepository jobRepository, IUserRepository userRepository, IBookingRepository bookingRepository)
        {
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
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

            if (job.IdUser <= 0)
            {
                throw new ArgumentException("Id of User should be a non-negative integer.");
            }

            var existingJobById = await _jobRepository.GetJobByIdAsync(job.IdJob);
            if (existingJobById != null)
            {
                throw new InvalidOperationException("A job with the same JobId already exists.");
            }

            var existingUser = await _userRepository.GetUserByIdAsync(job.IdUser);
            if (existingUser == null)
            {
                throw new InvalidOperationException($"User with Id {job.IdUser} does not exist.");
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

            if (job.IdUser <= 0)
            {
                throw new ArgumentException("Id of User should be a non-negative integer.");
            }

            var existingUser = await _userRepository.GetUserByIdAsync(job.IdUser);
            if (existingUser == null)
            {
                throw new InvalidOperationException($"User with Id {job.IdUser} does not exist.");
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

        public async Task<List<Job>> FilterJobsAsync(string jobTitle, DateTime date, string location)
        {

            var allJobs = await _jobRepository.GetAllJobsAsync();
            var allBookings = await _bookingRepository.GetAllBookingsAsync();

            var filteredJobs = allJobs.ToList();

            if (!string.IsNullOrEmpty(jobTitle))
            {
                filteredJobs = filteredJobs
                    .Where(job => job.JobTitle?.Equals(jobTitle, StringComparison.OrdinalIgnoreCase) == true)
                    .ToList();
            }

            if (date != default)
            {
                filteredJobs = filteredJobs
                    .Where(job =>
                    {
                        var jobIdUser = job.IdUser; // Get IdUser from the job
                        var bookingsForJob = allBookings
                            .Where(booking =>
                                booking.DateTime.Date == date.Date &&
                                (booking.ProviderUserId == jobIdUser) && // Use ProviderUserId
                                booking.Status == BookingStatus.Accepted);

                        return !bookingsForJob.Any();
                    })
                    .ToList();
            }


            if (!string.IsNullOrEmpty(location))
            {
                var lowerCaseLocation = location.ToLower();

                // Get all unique IdUser values from filtered jobs
                var userIds = filteredJobs.Select(job => job.IdUser).Distinct();

                // Filter out jobs where the associated User's Location matches the specified location
                var filteredJobsWithLocation = new List<Job>();

                foreach (var userId in userIds)
                {
                    var userLocation = await _userRepository.GetUserLocationByIdAsync(userId);

                    if (userLocation != null && userLocation.ToLower() == lowerCaseLocation)
                    {
                        // If the location matches, add the corresponding jobs to the result
                        filteredJobsWithLocation.AddRange(filteredJobs.Where(job => job.IdUser == userId));
                    }
                }

                filteredJobs = filteredJobsWithLocation.ToList();
            }



            return filteredJobs;

        }

    }
}
