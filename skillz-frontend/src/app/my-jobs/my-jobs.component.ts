import { Component } from '@angular/core';

@Component({
  selector: 'app-my-jobs',
  templateUrl: './my-jobs.component.html',
  styleUrl: './my-jobs.component.css'
})
export class MyJobsComponent {
    jobs = [
      {
          title: 'Gardener',  
          rating: '4.3/5',
          username: 'Gombos Andrei',
          experience: '6+ years',
          certificationStatus: 'Not Certified',
          backgroundImage: "https://img.freepik.com/premium-photo/wooden-garden-table-with-trees-flowers-blurred-background-generative-ai_74760-571.jpg",
          profileImage: 'https://assets-global.website-files.com/65635da09dc83a90af8638e0/65635da09dc83a90af863905_gardener-job-description-1659711272.webp',
          jobTypeLogo:'https://assets-global.website-files.com/65635da09dc83a90af8638e0/6563938b39802fe6b6fd8969_gardening.png',
          isVerified: false // Set the verified status based on your logic
  
      },
      {
        title: 'Gardener',  
        rating: '4.3/5',
        username: 'Ilie Cristian',
        experience: '10+ years',
        certificationStatus: 'Certified',
        backgroundImage: "https://img.freepik.com/premium-photo/wooden-garden-table-with-trees-flowers-blurred-background-generative-ai_74760-571.jpg",
        profileImage: 'https://assets-global.website-files.com/65635da09dc83a90af8638e0/65635da09dc83a90af863905_gardener-job-description-1659711272.webp',
        jobTypeLogo:'https://assets-global.website-files.com/65635da09dc83a90af8638e0/6563938b39802fe6b6fd8969_gardening.png',
        isVerified: true // Set the verified status based on your logic
  
    }
  ];
  
   
  }