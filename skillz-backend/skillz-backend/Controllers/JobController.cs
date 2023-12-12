// JobController.cs
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

        public JobController(IJobService jobService)
        {
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
        }

        [HttpGet("{jobId}")]
        public async Task<IActionResult> GetJobById(int jobId)
        {
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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _jobService.GetAllJobsAsync();

            return Ok(jobs);
        }

        [HttpGet("title/{jobTitle}")]
        public async Task<IActionResult> GetJobsByTitle(string jobTitle)
        {
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

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetJobsByUser(int userId)
        {
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

        [HttpGet("experience/{experiencedYears}")]
        public async Task<IActionResult> GetJobsByExperience(int experiencedYears)
        {
            if (experiencedYears < 0)
            {
                return BadRequest("ExperiencedYears should be a non-negative integer.");
            }

            var jobs = await _jobService.GetJobsByExperienceAsync(experiencedYears);

            return Ok(jobs);
        }
        [HttpPost]
        public async Task<ActionResult<JobDto>> CreateJob([FromBody] JobDto jobDto)
        {
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

                await _jobService.CreateJobAsync(job);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return new JobDto
            {
                JobTitle = jobDto.JobTitle,
                Description = jobDto.Description,
                ExperiencedYears = jobDto.ExperiencedYears,
                IdUser = jobDto.IdUser
            };
        }

        [HttpPut("{jobId}")]
        public async Task<IActionResult> UpdateJob(int jobId, [FromBody] JobDto jobDto)
        {
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


        [HttpDelete("{jobId}")]
        public async Task<IActionResult> DeleteJob(int jobId)
        {
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
    }
}
