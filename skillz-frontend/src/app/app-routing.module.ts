import { Component } from '@angular/core';
// app-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component'; // Import the new SignupComponent
import { SignupStepsComponent } from './signup-steps/signup-steps.component';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { JobsListingComponent } from './jobs-listing/jobs-listing.component';
import { JobPageComponent } from './job-page/job-page.component';
import { JobPostComponent } from './job-post/job-post.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent }, // Add the route for the signup component
  { path: 'signup-steps', component: SignupStepsComponent},
  { path: 'home', component:HomeComponent},
  { path: 'profile', component:ProfileComponent},
  { path: 'jobs-listing', component:JobsListingComponent},
  { path: 'job-page', component:JobPageComponent},
  { path: 'job-post', component:JobPostComponent}
  // Add other routes as needed
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
