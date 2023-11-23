import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent {
  nickname: string = "Justin";
  email: string = '';
  password: string = '';
  confirmPassword: string = '';

  constructor(private router: Router, private authService: AuthService) {}
  

  onSubmit() {
    const user = {
      nickname: this.nickname,
      email: this.email,
      password: this.password
    };

    this.authService.register(user).subscribe(
      response => {
        console.log('Registration successful:', response);
        // Optionally, you can redirect to the login page or handle success
        this.router.navigate(['/login']);
      },
      error => {
        console.error('Registration failed:', error);
        // Handle registration error
      }
    );
  }

  navigateToLogIn() {
    this.router.navigate(['/login']); // Navigate to the signup route
  }
}
