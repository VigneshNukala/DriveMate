import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { DriverRequest, DriverRequestService } from '../../../core/services/driver-request.service';

@Component({
  selector: 'app-driver-requests',
  standalone: true,
  templateUrl: './requests.html',
  styleUrl: './requests.css',
})
export class DriverRequests implements OnInit {
  private readonly router = inject(Router);

  private readonly driverRequestService = inject(DriverRequestService);

  private readonly changeDetectorRef = inject(ChangeDetectorRef);

  requests: DriverRequest[] = [];

  isLoading = false;
  processingRequestId: string | null = null;

  errorMessage = '';
  successMessage = '';

  ngOnInit(): void {
    this.loadRequests();
  }

  loadRequests(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.driverRequestService.getPendingRequests().subscribe({
      next: (requests) => {
        console.log('Driver pending requests:', requests);

        this.requests = requests;
        this.isLoading = false;

        this.changeDetectorRef.detectChanges();
      },

      error: (error) => {
        console.error('Failed to load driver requests:', error);

        this.isLoading = false;

        this.errorMessage = error?.error?.message ?? 'Unable to load driver requests.';

        this.changeDetectorRef.detectChanges();
      },
    });
  }

  acceptRequest(id: string): void {
    this.processingRequestId = id;
    this.errorMessage = '';
    this.successMessage = '';

    this.driverRequestService.acceptRequest(id).subscribe({
      next: () => {
        this.processingRequestId = null;

        this.successMessage = 'Driver request accepted successfully.';

        this.loadRequests();
      },

      error: (error) => {
        this.processingRequestId = null;

        this.errorMessage = error?.error?.message ?? 'Unable to accept the request.';

        this.changeDetectorRef.detectChanges();
      },
    });
  }

  rejectRequest(id: string): void {
    this.processingRequestId = id;
    this.errorMessage = '';
    this.successMessage = '';

    this.driverRequestService.rejectRequest(id).subscribe({
      next: () => {
        this.processingRequestId = null;

        this.successMessage = 'Driver request rejected successfully.';

        this.loadRequests();
      },

      error: (error) => {
        this.processingRequestId = null;

        this.errorMessage = error?.error?.message ?? 'Unable to reject the request.';

        this.changeDetectorRef.detectChanges();
      },
    });
  }

  goBack(): void {
    this.router.navigate(['/driver']);
  }
}
