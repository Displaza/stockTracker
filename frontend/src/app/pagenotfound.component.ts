import { Component } from "@angular/core";

@Component({
  selector: 'app-home',
  template: `<h1>Page not found whoops</h1>`,
  styleUrls: []
})
export class PageNoFoundComponent {
  title = 'Home Page';
  description = 'Welcome to the home page of our application.';

  constructor() {
    // Initialization logic can go here
  }
}