import { Component } from "@angular/core";
import { SimpleGetComponent } from "../../test.component";
import { FormComponent } from "../form/form.component";

@Component({
  selector: 'app-home',
  template: `<h1>Hello, you've reached Home</h1>
  <!-- <app-simple-get></app-simple-get> -->
  <!-- <app-form></app-form> -->
  `,
  styleUrls: ['./home.component.css'],
  imports: [SimpleGetComponent, FormComponent]
})
export class HomeComponent {
  title = 'Home Page';
  description = 'Welcome to the home page of our application.';

  constructor() {
    // Initialization logic can go here
  }
}