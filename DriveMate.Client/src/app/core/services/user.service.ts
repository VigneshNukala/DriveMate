import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface UserProfile {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string | null;
  role: string;
  isActive: boolean;
  createdAt: string;
}

export interface UpdateUserProfileRequest {
  firstName: string;
  lastName: string;
  phoneNumber: string | null;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    'http://localhost:5036/api/users';

  getProfile(): Observable<UserProfile> {
    console.log('Calling profile API...');

    return this.http.get<UserProfile>(
      `${this.apiUrl}/profile`
    );
  }

  updateProfile(
    request: UpdateUserProfileRequest
  ): Observable<UserProfile> {
    return this.http.put<UserProfile>(
      `${this.apiUrl}/profile`,
      request
    );
  }
}