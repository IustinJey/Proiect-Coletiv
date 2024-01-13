// auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isAuthenticated() {
    return this.isAuth;
  }
  private apiUrl = 'https://localhost:7062/Authentication'; // backend url
  private tokenKey = 'auth_token'; // key to store the token in local storage
  private usernameKey = 'auth_username'; // key to store the username in local storage
  private emailKey = 'auth_email'; // key to store the email in local storage
  private userID= 'user_id';
  private isAuth = false
  constructor(private http: HttpClient) {}

  register(user: any): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.post<any>(`${this.apiUrl}/register`, user, { headers })
    .pipe(
      tap(response => this.handleAuthentication(response))
    );
  }

  login(user: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, user)
      .pipe(
        tap(response => this.handleAuthentication(response))
      );
  }

  logout(): Observable<any> {
    // Implement logout logic here, if needed
    this.isAuth = false;
    return this.http.post<any>(`${this.apiUrl}/logout`, {});
  }

  private handleAuthentication(response: any): void {
    // Handle the authentication response, e.g., store the token, username, and email in local storage
    if (response && response.token) {
      localStorage.setItem(this.tokenKey, response.token);
    }

    if (response && response.username) {
      localStorage.setItem(this.usernameKey, response.username);
    }

    if (response && response.email) {
      localStorage.setItem(this.emailKey, response.email);
    }
    if(response)
      this.isAuth = true;
  }

  getToken(): string | null {
    // Retrieve the stored token from local storage
    return localStorage.getItem(this.tokenKey);
  }

  getUserId(): string | null {
    // Retrieve the stored token from local storage
    return localStorage.getItem(this.userID);
  }

  getUsername(): string | null {
    // Retrieve the stored username from local storage
    return localStorage.getItem(this.usernameKey);
  }

  getEmail(): string | null {
    // Retrieve the stored email from local storage
    return localStorage.getItem(this.emailKey);
  }
}
