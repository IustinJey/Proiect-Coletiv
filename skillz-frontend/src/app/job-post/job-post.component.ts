import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { SafeUrl } from '@angular/platform-browser';
import { JobService } from '../job.service';

@Component({
  selector: 'app-job-post',
  templateUrl: './job-post.component.html',
  styleUrl: './job-post.component.css'
})
export class JobPostComponent {
  imageFiles: File[] = [];
  description: string = "";
  experiencedYears: string = "";
  showStepInitial: boolean = true;
  showStep1: boolean = false;
  showStep2: boolean = false;
  showStep3: boolean = false;
  errorMessage: string = "";
  selectedJobTitle: string = '';
  jobTitles: string[] = [
    "Accountant",
    "Babysitter",
    "Carpenter",
    "Dentist",
    "Electrician",
    "Florist",
    "Graphic Designer",
    "Hairdresser",
    "Interior Designer",
    "Janitor",
    "Landscaper",
    "Massage Therapist",
    "Nutritionist",
    "Optician",
    "Painter",
    "Quilter",
    "Realtor",
    "Swimming Instructor",
    "Tailor",
    "Upholsterer",
    "Veterinarian",
    "Wedding Planner",
    "X-ray Technician",
    "Yoga Instructor",
    "Zoologist"
  ];
  sanitizer: any;

  constructor(private router: Router, private authService: AuthService, private jobService: JobService) { }

  step_initial_action() {
    this.showStepInitial = false;
    this.showStep1 = true;
  }
  step1_action() {
    this.showStep1 = false;
    this.showStep2 = true;
  }
  step2_action() {
    this.showStep2 = false;
    this.showStep3 = true;
  }
  step3_action() {
    // Check if the user is authenticated
    if (this.authService.isAuthenticated()) {
      // Create a job object with the required properties
      const jobDto = {
        Description: this.description,
        ExperiencedYears: parseInt(this.experiencedYears),
        JobTitle: this.selectedJobTitle,
        IdUser: this.authService.getUserId(), // Assuming you have a getUserId() method in your AuthService
      };

      // Call the job service to create the job
      this.jobService.createJob(jobDto, this.imageFiles)
        .subscribe(
          (response) => {
            console.log('Job created successfully!', response);
            // Redirect to the job details page or any other page
            this.router.navigate(['/home', response.id]); // Assuming the response contains the created job's ID
          },
          (error) => {
            console.error('Error creating job:', error);
            // Handle the error
          }
        );
    } else {
      // User is not authenticated, redirect to login
      this.navigateToLogIn();
    }
  }

  navigateToLogIn() {
    this.router.navigate(['/login']); // Navigate to the login route
  }

  handleImageUpload(event: any): void {
    const fileList: FileList | null = event.target.files;

    if (fileList) {
      // Convert FileList to array and update imageFiles
      this.imageFiles = Array.from(fileList);

      // You may want to perform additional actions with the uploaded images
    }
  }
  removeImage(index: number): void {
    // Remove the image from the array based on its index
    this.imageFiles.splice(index, 1);
  }
  getPreviewImage(imageFile: File): string {
    return URL.createObjectURL(imageFile);
  }
}