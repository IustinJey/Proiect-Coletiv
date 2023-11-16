// login.component.ts
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  constructor(private router: Router) {} // Inject the Router

  onSubmit() {
    console.log('Login clicked');
    console.log('Email:', this.email);
    console.log('Password:', this.password);
  }

  navigateToSignup() {
    this.router.navigate(['/signup']); // Navigate to the signup route
  }
}
