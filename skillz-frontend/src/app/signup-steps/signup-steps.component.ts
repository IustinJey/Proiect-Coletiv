import { Component } from '@angular/core';
import { Route } from '@angular/router';

@Component({
  selector: 'app-signup-steps',
  templateUrl: './signup-steps.component.html',
  styleUrl: './signup-steps.component.css'
})
export class SignupStepsComponent {
  nickname: string = ""; 
  country: string = "";
  city: string = "";
  phone_number: string = "";
  showStep1: boolean = true;
  showStep2: boolean = false;
  showStep3: boolean = false;

  step1_action(){
    this.showStep1 = false;
    this.showStep2 = true;
  }
  step2_action(){
    this.showStep2 = false;
    this.showStep3 = true;
  }
  step3_action(){
    this.showStep3 = false;
    this.showStep1 = true;
  }
}
