import { Component } from '@angular/core';
import { AuthService } from "../auth/auth.service";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',})
export class DashboardComponent {
    public isAuthenticated = false;;

    constructor(private authService: AuthService) {
        console.log('DashboardComponent initialized');
        console.log('Current user:', this.authService.currentUser$);
        this.isAuthenticated = this.authService.isAuthenticated();
        console.log('Is authenticated:', this.isAuthenticated);        
    }

}