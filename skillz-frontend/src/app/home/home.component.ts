import { Component, OnInit } from '@angular/core';
import { JobService } from '../job.service';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  userId: number = 0;
  jobTitles: string[] = [];
  selectedJobTitle: string = '';
  isDropdownVisible: boolean = false;  // Added property

  constructor(private jobService: JobService, private router: Router, private authService: AuthService) {}

  ngOnInit() {
    const id = this.authService.getUserId()
    if(id)
    {
      this.userId = id;
    }
    this.loadJobTitles();
  }

  loadJobTitles() {
    this.jobService.getAllJobs().subscribe(
      jobs => {
        if (jobs) {
          this.jobTitles = [...new Set(jobs.map(job => job.jobTitle))];
        } else {
          console.error('Received null or undefined jobs array.');
        }
      },
      error => {
        console.error('Error loading job titles:', error);
      }
    );
  }

  toggleDropdown() {
    this.isDropdownVisible = !this.isDropdownVisible;
  }
  routeToJobPost(){
    this.router.navigate(['/job-post']);
  }
  searchClicked() {
    if (this.selectedJobTitle) {
      this.router.navigate(['/jobs-listing', this.selectedJobTitle.toLowerCase()]);
    }
  }
  navigate(location: string) {
    this.router.navigate([location]);
  }
  redirectToProfile() {
    this.router.navigate(['/profile', this.userId]);
  }
}
