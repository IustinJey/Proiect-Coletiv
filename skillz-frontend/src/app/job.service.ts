// job.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JobService {
  private apiUrl = 'https://localhost:7062/job/Job'; // assuming the JobController route

  constructor(private http: HttpClient) {}

  getJobById(jobId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${jobId}`);
  }

  getAllJobs(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/all`);
  }

  getJobsByTitle(jobTitle: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/title/${jobTitle}`);
  }

  getJobsByUser(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/user/${userId}`);
  }

  getJobsByExperience(experiencedYears: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/experience/${experiencedYears}`);
  }

  createJob(jobDto: any, images: File[]): Observable<any> {
    const formData = new FormData();

    // Append jobDto properties to the FormData
    Object.keys(jobDto).forEach(key => {
      formData.append(key, jobDto[key]);
    });

    // Append images to the FormData
    images.forEach((image, index) => {
      formData.append(`files[${index}]`, image, image.name);
    });

    // Headers for handling FormData
    const headers = new HttpHeaders();

    return this.http.post<any>(`${this.apiUrl}`, formData, { headers });
  }

  updateJob(jobId: number, jobDto: any): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.put<any>(`${this.apiUrl}/${jobId}`, jobDto, { headers });
  }

  deleteJob(jobId: number): Observable<any> {
    const headers = new HttpHeaders({
    });

    return this.http.delete<any>(`${this.apiUrl}/${jobId}`, { headers });
  }
}
