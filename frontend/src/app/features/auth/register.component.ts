import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  message = '';
  form: FormGroup;



  constructor(private fb: FormBuilder, private authService: AuthService) {
      this.form = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  });
  }

  onRegister() {
    if (this.form.invalid) {
      this.message = 'All fields are required';
      return;
    }

    const newUser = {
      username: this.form.value.username!,
      password: this.form.value.password!,
      // role: 'User',
      // email: ''
    };

    this.authService.register(newUser).subscribe({
      next: () => this.message = 'Registration successful',
      error: (err: { error: any; message: any; }) => this.message = `Registration failed: ${err.error || err.message}`
    });
  }
}
