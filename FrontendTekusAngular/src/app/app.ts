import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from './auth/auth.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'Tekus Angular';

  constructor(
    private router: Router,
    public authService: AuthService
  ) {}

  isLoginPage(): boolean {
    return this.router.url === '/login';
  }

  logout(): void {
    this.authService.logout();
  }
}
