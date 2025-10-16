import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ApiService } from '../ui/service/api.service';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  
  isLoading = signal(false);
  errorMessage = signal('');
  
  loginData = {
    username: '',
    password: ''
  };

  constructor(
    private apiService: ApiService,
    private router: Router,
    private authService: AuthService
  ) {}

  onSubmit(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    this.apiService.post<{data: string}>('Auth/login', {
      username: this.loginData.username,
      password: this.loginData.password
    }).subscribe({
      next: (response) => {
        this.isLoading.set(false);
        
        if (response.data) {
          this.authService.setAuthenticated(response.data);
          this.router.navigate(['/providers']);
        }
      },
      error: (error) => {
        this.isLoading.set(false);
        this.errorMessage.set(
          error.error?.message || 
          'Error starting session. Verify your credentials.'
        );
      }
    });
  }
}