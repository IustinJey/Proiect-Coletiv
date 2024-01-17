import { Component } from '@angular/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {

  certificates = [
    {
      certificateName: 'Electrician',
      experience: '+5 years experience'
    },

    {
      certificateName: 'Gradinar',
      experience: '+8 years experience'
    },

    {
      certificateName: 'Panaramist',
      experience: '+12 years experience'
    }
];
}
