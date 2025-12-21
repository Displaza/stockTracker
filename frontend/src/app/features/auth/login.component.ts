import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AuthService } from './auth.service';
import { LoginRequest, LoginResponse } from '../../core/models/auth';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html'
})
export class LoginComponent {
  message = '';
  form: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService) {
      this.form = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  });
  }

  onLogin() {
    if (this.form.invalid) {
      this.message = 'All fields are required';
      return;
    }

    console.log("clicked Login");

    const user = {
      username: this.form.value.username!,
      password: this.form.value.password!
    };

    this.authService.login(user).subscribe({
      next: res => {
        this.message = 'Login successful';
        // Store token if provided: localStorage.setItem('token', res.token);
      },
      error: err => this.message = `Login failed: ${err.error.toString() || err.message.toString()}`
    });
  }
}
