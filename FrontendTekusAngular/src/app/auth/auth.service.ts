import { Injectable, signal } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  isAuthenticated = signal(false);
  
  constructor(private router: Router) {
    this.checkAuthStatus();
  }

  checkAuthStatus(): boolean {
    const token = localStorage.getItem('token');
    const isAuth = !!token;
    
    this.isAuthenticated.set(isAuth);
    
    if (!isAuth && this.router.url !== '/login') {
      this.router.navigate(['/login']);
    }
    
    return isAuth;
  }

  setAuthenticated(token: string): void {
    localStorage.setItem('token', token);
    this.isAuthenticated.set(true);
  }

  logout(): void {
    localStorage.removeItem('token');
    this.isAuthenticated.set(false);
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  canActivate(): boolean {
    return this.checkAuthStatus();
  }
}