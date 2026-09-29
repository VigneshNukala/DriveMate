import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  imports: [],
  selector: 'app-dashboard',
  styleUrl: './dashboard.css',
  templateUrl: './dashboard.html',
})
export class Dashboard {
  private readonly router = inject(Router);
  private readonly authService = inject(AuthService);

  goToProfile(): void {
    this.router.navigate(['/profile']);
  }

  viewRequests(): void {
    this.router.navigate(['/driver/requests']);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
