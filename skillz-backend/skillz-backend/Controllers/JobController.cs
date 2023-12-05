// JobController.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> CreateJob([FromBody] Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _jobService.CreateJobAsync(job);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetJobById), new { jobId = job.IdJob }, job);
        }

        [HttpPut("{jobId}")]
        public async Task<IActionResult> UpdateJob(int jobId, [FromBody] Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (jobId != job.IdJob)
            {
                return BadRequest("JobId in the request body does not match the JobId in the route.");
            }

            var existingJob = await _jobService.GetJobByIdAsync(jobId);

            if (existingJob == null)
            {
                return NotFound($"Job with JobId '{jobId}' not found.");
            }

            try
            {
                await _jobService.UpdateJobAsync(job);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(job);
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
