import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { isPlatformBrowser } from '@angular/common';
import { PLATFORM_ID } from '@angular/core';

import { UserProfile, UserService } from '../../core/services/user.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [FormsModule, DatePipe],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile implements OnInit {
  private readonly userService = inject(UserService);
  private readonly platformId = inject(PLATFORM_ID);
  private readonly cdr = inject(ChangeDetectorRef);

  profile: UserProfile | null = null;

  isEditing = false;
  isLoading = true;
  isSaving = false;

  errorMessage = '';
  successMessage = '';

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      console.log('Profile component running in browser');
      this.loadProfile();
    } else {
      this.isLoading = false;
    }
  }

  loadProfile(): void {
    console.log('loadProfile() called');

    this.isLoading = true;
    this.errorMessage = '';

    this.userService.getProfile().subscribe({
      next: (profile) => {
        console.log('PROFILE RESPONSE:', profile);

        this.profile = profile;
        this.isLoading = false;

        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error('PROFILE ERROR:', error);

        this.errorMessage = 'Unable to load your profile.';
        this.isLoading = false;

        this.cdr.detectChanges();
      },
    });
  }

  startEditing(): void {
    this.successMessage = '';
    this.errorMessage = '';
    this.isEditing = true;
  }

  cancelEditing(): void {
    this.isEditing = false;
    this.successMessage = '';
    this.errorMessage = '';

    this.loadProfile();
  }

  saveProfile(): void {
    if (!this.profile) {
      return;
    }

    if (!this.profile.firstName.trim() || !this.profile.lastName.trim()) {
      this.errorMessage = 'First name and last name are required.';
      return;
    }

    this.isSaving = true;
    this.errorMessage = '';
    this.successMessage = '';

    const request = {
      firstName: this.profile.firstName.trim(),
      lastName: this.profile.lastName.trim(),
      phoneNumber: this.profile.phoneNumber?.trim() || null,
    };

    this.userService.updateProfile(request).subscribe({
      next: (updatedProfile) => {
        console.log('UPDATE RESPONSE:', updatedProfile);

        this.profile = updatedProfile;
        this.isEditing = false;
        this.isSaving = false;
        this.successMessage = 'Profile updated successfully.';

        this.cdr.detectChanges();
      },

      error: (error) => {
        console.error('UPDATE ERROR:', error);

        this.isSaving = false;
        this.errorMessage = 'Unable to update your profile.';

        this.cdr.detectChanges();
      },
    });
  }
}
