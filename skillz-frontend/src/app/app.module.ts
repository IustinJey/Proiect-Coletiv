// app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { SignupStepsComponent } from './signup-steps/signup-steps.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignupComponent,
    SignupStepsComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule  // Add this line to include the routing module
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
