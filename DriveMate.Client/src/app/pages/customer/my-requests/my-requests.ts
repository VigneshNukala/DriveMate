import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { DriverRequest, DriverRequestService } from '../../../core/services/driver-request.service';

@Component({
  selector: 'app-my-requests',
  standalone: true,
  templateUrl: './my-requests.html',
  styleUrl: './my-requests.css',
})
export class MyRequests implements OnInit {
  private readonly router = inject(Router);
  private readonly driverRequestService = inject(DriverRequestService);
  private readonly changeDetectorRef = inject(ChangeDetectorRef);

  requests: DriverRequest[] = [];

  isLoading = false;
  errorMessage = '';

  ngOnInit(): void {
    this.loadRequests();
  }

  loadRequests(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.driverRequestService.getMyRequests().subscribe({
      next: (requests) => {
        console.log('My requests:', requests);

        this.requests = requests;
        this.isLoading = false;

        this.changeDetectorRef.detectChanges();
      },

      error: (error) => {
        console.error('Failed to load requests:', error);

        this.isLoading = false;

        this.errorMessage = error?.error?.message ?? 'Unable to load your requests.';

        this.changeDetectorRef.detectChanges();
      },
    });
  }

  requestDriver(): void {
    this.router.navigate(['/customer/request-driver']);
  }

  goBack(): void {
    this.router.navigate(['/customer']);
  }
}
