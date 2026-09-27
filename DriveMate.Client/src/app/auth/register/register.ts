import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  isLoading = false;
  errorMessage = '';
  successMessage = '';

  registerForm;

  constructor(
    private readonly fb: FormBuilder,
    private readonly authService: AuthService,
    private readonly router: Router,
  ) {
    this.registerForm = this.fb.nonNullable.group({
      firstName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],

      lastName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],

      email: ['', [Validators.required, Validators.email]],

      phoneNumber: ['', [Validators.maxLength(20)]],

      password: [
        '',
        [
          Validators.required,
          Validators.minLength(8),
          Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$/),
        ],
      ],

      confirmPassword: ['', Validators.required],
    });
  }

  private submit(): void {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    const formValue = this.registerForm.getRawValue();

    if (formValue.password !== formValue.confirmPassword) {
      this.errorMessage = 'Passwords do not match.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.authService
      .register({
        firstName: formValue.firstName,
        lastName: formValue.lastName,
        email: formValue.email,
        phoneNumber: formValue.phoneNumber || undefined,
        password: formValue.password,
      })
      .subscribe({
        next: () => {
          this.isLoading = false;
          this.successMessage = 'Registration successful. Redirecting to login...';

          setTimeout(() => {
            this.router.navigate(['/login']);
          }, 1000);
        },

        error: (error) => {
          this.isLoading = false;
          this.errorMessage = this.getErrorMessage(error);

          if (error.status === 409) {
            this.registerForm.controls.email.setErrors({
              emailExists: true,
            });
          }
        },
      });
  }

  private getErrorMessage(error: any): string {
    if (error?.error?.message) {
      return error.error.message;
    }

    if (error?.error?.errors) {
      const errors = error.error.errors;

      return Object.values(errors).flat().join(' ');
    }

    return 'Registration failed. Please check your details and try again.';
  }
}
