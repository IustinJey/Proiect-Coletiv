import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent {
  email: string = '';
  password: string = '';
  confirmPassword: string = '';

  constructor(private router: Router) {} // Inject the Router

  onSubmit() {
    console.log('Login clicked');
    console.log('Email:', this.email);
    console.log('Password:', this.password);
    console.log('Confirm Password', this.confirmPassword)
    this.router.navigate(['/signup-steps']);
  }

  navigateToLogIn() {
    this.router.navigate(['/login']); // Navigate to the signup route
  }
}
