import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { DriverRequestService } from '../../../core/services/driver-request.service';

@Component({
  selector: 'app-request-driver',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './request-driver.html',
  styleUrl: './request-driver.css',
})
export class RequestDriver {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly driverRequestService = inject(DriverRequestService);

  isLoading = false;
  errorMessage = '';
  successMessage = '';

  requestForm = this.fb.nonNullable.group({
    pickupLocation: ['', [Validators.required, Validators.maxLength(500)]],

    dropLocation: ['', Validators.maxLength(500)],

    serviceDate: ['', Validators.required],

    startTime: ['', Validators.required],

    durationHours: [1, [Validators.required, Validators.min(0.5), Validators.max(24)]],

    notes: ['', Validators.maxLength(1000)],
  });

  submitRequest(): void {
    if (this.requestForm.invalid) {
      this.requestForm.markAllAsTouched();
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    const formValue = this.requestForm.getRawValue();

    this.driverRequestService
      .createRequest({
        pickupLocation: formValue.pickupLocation.trim(),

        dropLocation: formValue.dropLocation.trim() || null,

        serviceDate: formValue.serviceDate,

        startTime: formValue.startTime,

        durationHours: Number(formValue.durationHours),

        notes: formValue.notes.trim() || null,
      })
      .subscribe({
        next: () => {
          this.isLoading = false;

          this.successMessage = 'Driver request created successfully.';

          this.requestForm.reset({
            pickupLocation: '',
            dropLocation: '',
            serviceDate: '',
            startTime: '',
            durationHours: 1,
            notes: '',
          });
        },

        error: (error) => {
          this.isLoading = false;

          this.errorMessage = error?.error?.message ?? 'Unable to create the driver request.';
        },
      });
  }

  goBack(): void {
    this.router.navigate(['/customer']);
  }
}
