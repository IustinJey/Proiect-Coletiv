import { Component } from '@angular/core';
import { JobService } from '../job.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-jobs-listing',
  templateUrl: './jobs-listing.component.html',
  styleUrl: './jobs-listing.component.css'
})
export class JobsListingComponent {
//   jobs = [
//     {
//         title: 'Gardener',  
//         rating: '4.3/5',
//         username: 'Gombos Andrei',
//         experience: '6+ years',
//         certificationStatus: 'Not Certified',
//         backgroundImage: "https://img.freepik.com/premium-photo/wooden-garden-table-with-trees-flowers-blurred-background-generative-ai_74760-571.jpg",
//         profileImage: 'https://assets-global.website-files.com/65635da09dc83a90af8638e0/65635da09dc83a90af863905_gardener-job-description-1659711272.webp',
//         jobTypeLogo:'https://assets-global.website-files.com/65635da09dc83a90af8638e0/6563938b39802fe6b6fd8969_gardening.png',
//         isVerified: false // Set the verified status based on your logic

//     }
// ];
jobs: any[] = [];

  constructor(private jobService: JobService, private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.params.subscribe(params => {
      const jobTitle = params['jobTitle'];

      if (jobTitle) {
        this.loadJobsByTitle(jobTitle);
      } else {
        // Handle the case when no job title is specified
        this.loadAllJobs();
      }
    });
  }

  loadJobsByTitle(jobTitle: string) {
    this.jobService.getJobsByTitle(jobTitle).subscribe(
      jobs => {
        this.jobs = jobs;
      },
      error => {
        console.error('Error loading jobs by title:', error);
      }
    );
  }

  loadAllJobs() {
    this.jobService.getAllJobs().subscribe(
      jobs => {
        this.jobs = jobs;
      },
      error => {
        console.error('Error loading all jobs:', error);
      }
    );
  }
 
}
