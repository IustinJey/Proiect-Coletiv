import { Component } from '@angular/core';

@Component({
  selector: 'app-job-page',
  templateUrl: './job-page.component.html',
  styleUrl: './job-page.component.css'
})
export class JobPageComponent {
  images:string[] = [
    'https://www.homelux.ro/blog/wp-content/uploads/2019/03/gradini-de-dimensiuni-mari8.jpg',
    'https://hips.hearstapps.com/hmg-prod/images/claude-monets-house-and-gardens-in-giverny-france-news-photo-1042013294-1562610151.jpg?crop=1.00xw:0.753xh;0,0.0671xh&resize=1200:*',
  ];

  currentIndex: number = 0;

  ngOnInit() {
    // You can add any initialization logic here if needed
  }

  nextImage() {
    this.currentIndex = (this.currentIndex + 1) % this.images.length;
    this.updateSlider();
  }
  
  prevImage() {
    this.currentIndex = (this.currentIndex - 1 + this.images.length) % this.images.length;
    this.updateSlider();
  }
  
  private updateSlider() {
    const sliderMask = document.querySelector('.w-slider-mask') as HTMLElement | null;
  
    if (sliderMask) {
      const translateValue = -100 * this.currentIndex + '%';
      sliderMask.style.transform = 'translateX(' + translateValue + ')';
    }
  }
  
}

