import { Component, OnInit } from '@angular/core';
import { JobService } from '../job.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../user.service';
import JSZip from 'jszip';

@Component({
  selector: 'app-jobs-listing',
  templateUrl: './jobs-listing.component.html',
  styleUrls: ['./jobs-listing.component.css']
})
export class JobsListingComponent implements OnInit {
  isDropdownVisible: boolean = false;  // Added property
  imageFiles: File[] = [];
  jobs: {
    jobId: number;
    title: string;
    rating: string;
    username: string;
    workerId: number;
    experience: number;
    certificationStatus: boolean;
    isVerified: boolean;
    profileImage: string;
    jobTypeLogo: string;
    backgroundImage: string;
  }[] = [];

  constructor(
    private jobService: JobService,
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const jobTitle = params['jobTitle'];
      this.loadJobsList(jobTitle);
    });
  }

  toggleDropdown() {
    this.isDropdownVisible = !this.isDropdownVisible;
  }

  loadJobsList(jobTitle: string): void {
    this.jobService.getJobsByTitle(jobTitle).subscribe(
      async jobs => {
        const jobPromises = jobs.map(async job => {
          const worker = await this.userService.getUserById(job.idUser).toPromise();

          return {
            jobId: job.idJob,
            title: job.jobTitle,
            rating: '4.3/5', // You can set the rating based on your logic
            username: worker.username,
            workerId: job.idUser,
            experience: job.experiencedYears,
            certificationStatus: false, // You need to fetch this information
            isVerified: false, // You need to fetch this information
            profileImage: worker.profileImage, // You need to fetch this information
            jobTypeLogo: '',
            backgroundImage: await this.getJobBackgroundImage(job.idJob)
          };
        });

        // Wait for all promises to resolve and then assign the values to this.jobs
        this.jobs = await Promise.all(jobPromises);
      },
      error => {
        console.error('Error loading jobs list:', error);
      }
    );
  }

  async getJobBackgroundImage(jobId: number) {
    console.log('Start getJobBackgroundImage');
    console.log('Job ID:', jobId);
  
    try {
      const response = await this.jobService.getJobImagesById(jobId).toPromise();
      console.log('API Response:', response);
  
      if (response instanceof Blob) {
        // Extract images from the ZIP file
        const zip = new JSZip();
        const zipData = await zip.loadAsync(response);
  
        // Assuming images are stored in the root of the ZIP file
        const imageFiles = Object.values(zipData.files);
  
        if (imageFiles.length > 0) {
          const imageUrl = URL.createObjectURL(await imageFiles[0].async('blob'));
          console.log('Image URL:', imageUrl);
          return imageUrl;
        } else {
          console.error('No image found in the ZIP file.');
        }
      } else {
        console.error('Unexpected response format:', response);
      }
    } catch (error) {
      console.error('Error fetching images:', error);
    }
  
    // Return default path if an error occurred
    console.log('Returning default path.');
    return 'path/to/default/image.jpg';
  }
  
  redirectToJobPage(jobId: number) {
    if(jobId) {
      this.router.navigate(['/job-page', jobId]);
    }
  }

}
