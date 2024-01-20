import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { JobService } from '../job.service';
import { UserService } from '../user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
username: string = '';
location: string = '';
verified: boolean = false;
profileImage: string = '';
certificates: string[] = [];

  constructor(private jobService: JobService,
              private userService: UserService, 
              private router: Router, 
              private authService: AuthService,
              private route: ActivatedRoute){}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const userId = params['userId'];
      this.loadData(userId);
    });
  }

  loadData(idUser: number){
    this.userService.getUserById(idUser).subscribe(async (user: any) => {
      this.username = user.username;
      this.location = user.location;
      this.certificates = await this.loadCertificates(idUser);
    });
  }


  loadCertificates(IdUser: number){
    return []
  }
}
