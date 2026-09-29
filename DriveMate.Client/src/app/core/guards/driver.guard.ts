import { inject } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../services/auth.service';

export const driverGuard = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (!authService.isAuthenticated()) {
    return router.createUrlTree(['/login']);
  }

  if (authService.isDriver()) {
    return true;
  }

  if (authService.isCustomer()) {
    return router.createUrlTree(['/customer']);
  }

  return router.createUrlTree(['/login']);
};