using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using skillz_backend.DTOs;
using skillz_backend.models;
using skillz_backend.Services;

namespace skillz_backend.controllers
{
    [ApiController]
    [Route("job/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IWebHostEnvironment _environment;

        // Constructor to inject IJobService dependency
        public JobController(IJobService jobService, IWebHostEnvironment environment)
        {
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
            _environment = environment;
        }

        // Retrieves a job by ID
        [HttpGet("{jobId}")]
        public async Task<IActionResult> GetJobById(int jobId)
        {
            // Validation for a positive JobId
            if (jobId <= 0)
            {
                return BadRequest("Invalid JobId. It should be a positive integer.");
            }

            var job = await _jobService.GetJobByIdAsync(jobId);

            if (job == null)
            {
                return NotFound("Job not found.");
            }

            return Ok(job);
        }

        // Retrieves all jobs
        [HttpGet("all")]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _jobService.GetAllJobsAsync();

            return Ok(jobs);
        }

        // Retrieves jobs by title
        [HttpGet("title/{jobTitle}")]
        public async Task<IActionResult> GetJobsByTitle(string jobTitle)
        {
            // Validation for a non-null and non-empty jobTitle
            if (string.IsNullOrEmpty(jobTitle))
            {
                return BadRequest("JobTitle cannot be null or empty.");
            }

            var jobs = await _jobService.GetJobsByTitleAsync(jobTitle);

            if (jobs.Count == 0)
            {
                return NotFound($"No jobs found with the title '{jobTitle}'.");
            }

            return Ok(jobs);
        }

        // Retrieves jobs by user ID
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetJobsByUser(int userId)
        {
            // Validation for a positive UserId
            if (userId <= 0)
            {
                return BadRequest("Invalid UserId. It should be a positive integer.");
            }

            var jobs = await _jobService.GetJobsByUserAsync(userId);

            if (jobs.Count == 0)
            {
                return NotFound($"No jobs found for user with UserId '{userId}'.");
            }

            return Ok(jobs);
        }

        // Retrieves jobs by experience
        [HttpGet("experience/{experiencedYears}")]
        public async Task<IActionResult> GetJobsByExperience(int experiencedYears)
        {
            // Validation for a non-negative experiencedYears
            if (experiencedYears < 0)
            {
                return BadRequest("ExperiencedYears should be a non-negative integer.");
            }

            var jobs = await _jobService.GetJobsByExperienceAsync(experiencedYears);

            return Ok(jobs);
        }

        [HttpGet("jobImages")]
        public async Task<ActionResult<List<JobImage>>> GetAllImagesAsync()
        {
            var images = await _jobService.GetAllImagesAsync();
            return Ok(images);
        }

        [HttpGet("{jobId}/images")]
        public async Task<ActionResult<List<JobImage>>> GetImagesByJobIdAsync(int jobId)
        {
            var images = await _jobService.GetImagesByJobIdAsync(jobId);
            return Ok(images);
        }

        // Creates a new job
        [HttpPost]
        public async Task<ActionResult<JobDto>> CreateJobWithImages([FromForm] JobDto jobDto)
        {
            // Validate the ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Manual mapping from JobDto to Job
                var job = new Job
                {
                    JobTitle = jobDto.JobTitle,
                    Description = jobDto.Description,
                    ExperiencedYears = jobDto.ExperiencedYears,
                    IdUser = jobDto.IdUser
                };

                // Create the job with images
                await _jobService.CreateJobAsync(job, jobDto.Images);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(jobDto);
        }

        // Updates an existing job
        [HttpPut("{jobId}")]
        public async Task<IActionResult> UpdateJob(int jobId, [FromBody] JobDto jobDto)
        {
            // Validate the ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingJob = await _jobService.GetJobByIdAsync(jobId);

            if (existingJob == null)
            {
                return NotFound($"Job with JobId '{jobId}' not found.");
            }

            try
            {
                // Manual mapping from JobDto to Job
                existingJob.JobTitle = jobDto.JobTitle;
                existingJob.Description = jobDto.Description;
                existingJob.ExperiencedYears = jobDto.ExperiencedYears;
                existingJob.IdUser = jobDto.IdUser;

                await _jobService.UpdateJobAsync(existingJob);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(jobDto);
        }

        // Deletes a job by ID
        [HttpDelete("{jobId}")]
        public async Task<IActionResult> DeleteJob(int jobId)
        {
            // Validation for a positive JobId
            if (jobId <= 0)
            {
                return BadRequest("Invalid JobId. It should be a positive integer.");
            }

            var job = await _jobService.GetJobByIdAsync(jobId);

            if (job == null)
            {
                return NotFound($"Job with JobId '{jobId}' not found.");
            }

            await _jobService.DeleteJobAsync(jobId);

            return NoContent();
        }

        // Filters jobs based on jobTitle, date, and location
        [HttpGet("filter")]
        public async Task<ActionResult<List<Job>>> FilterJobsAsync([FromQuery] string jobTitle, [FromQuery] DateTime date, [FromQuery] string location)
        {
            try
            {
                var filteredJobs = await _jobService.FilterJobsAsync(jobTitle, date, location);
                return Ok(filteredJobs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
