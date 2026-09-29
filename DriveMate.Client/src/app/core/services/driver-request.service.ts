import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface CreateDriverRequest {
  pickupLocation: string;
  dropLocation: string | null;
  serviceDate: string;
  startTime: string;
  durationHours: number;
  notes: string | null;
}

export interface DriverRequest {
  id: string;
  customerId: string;
  driverId: string | null;
  pickupLocation: string;
  dropLocation: string | null;
  serviceDate: string;
  startTime: string;
  durationHours: number;
  notes: string | null;
  status: string;
  createdAt: string;
  modifiedAt: string | null;
}

@Injectable({
  providedIn: 'root',
})
export class DriverRequestService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl = 'http://localhost:5036/api/driver-requests';

  private readonly driverApiUrl = 'http://localhost:5036/api/driver/requests';

  createRequest(request: CreateDriverRequest): Observable<DriverRequest> {
    return this.http.post<DriverRequest>(this.apiUrl, request);
  }

  getMyRequests(): Observable<DriverRequest[]> {
    return this.http.get<DriverRequest[]>(this.apiUrl);
  }

  getRequestById(id: string): Observable<DriverRequest> {
    return this.http.get<DriverRequest>(`${this.apiUrl}/${id}`);
  }

  getPendingRequests(): Observable<DriverRequest[]> {
    return this.http.get<DriverRequest[]>(this.driverApiUrl);
  }

  acceptRequest(id: string): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${this.driverApiUrl}/${id}/accept`, {});
  }

  rejectRequest(id: string): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${this.driverApiUrl}/${id}/reject`, {});
  }
}
