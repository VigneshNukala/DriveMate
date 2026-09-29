import { inject } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../services/auth.service';

export const customerGuard = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (!authService.isAuthenticated()) {
    return router.createUrlTree(['/login']);
  }

  if (authService.isCustomer()) {
    return true;
  }

  if (authService.isDriver()) {
    return router.createUrlTree(['/driver']);
  }

  return router.createUrlTree(['/login']);
};