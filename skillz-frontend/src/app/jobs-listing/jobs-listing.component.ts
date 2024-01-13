import { Component } from '@angular/core';

@Component({
  selector: 'app-jobs-listing',
  templateUrl: './jobs-listing.component.html',
  styleUrl: './jobs-listing.component.css'
})
export class JobsListingComponent {
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

    }
];

 
}
