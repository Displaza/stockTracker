import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SimpleGetComponent } from './test.component';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, SimpleGetComponent, RouterLink],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'fin-frontend';
}
