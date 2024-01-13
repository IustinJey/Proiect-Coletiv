// app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { SignupStepsComponent } from './signup-steps/signup-steps.component';
import { AuthService } from './auth.service';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { JobsListingComponent } from './jobs-listing/jobs-listing.component';
import { JobPageComponent } from './job-page/job-page.component';
import { MatIconModule } from '@angular/material/icon';
import { JobPostComponent } from './job-post/job-post.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignupComponent,
    SignupStepsComponent,
    HomeComponent,
    ProfileComponent,
    JobsListingComponent,
    JobPageComponent,
    JobPostComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    FormsModule,
    AppRoutingModule,  // Add this line to include the routing module
    MatIconModule
  ],
  providers: [
    AuthService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
