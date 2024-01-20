import { Component } from '@angular/core';

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrl: './profile-edit.component.css'
})
export class ProfileEditComponent {

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
