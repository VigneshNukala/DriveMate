import { Routes } from '@angular/router';

import { Login } from './auth/login/login';
import { Register } from './auth/register/register';
import { authGuard } from './core/guards/auth.guard';
import { Profile } from './pages/profile/profile';
import { Dashboard as DriverDashboard } from './pages/driver/dashboard/dashboard';
import { Dashboard as CustomerDashboard } from './pages/customer/dashboard/dashboard';
import { customerGuard } from './core/guards/customer.guard';
import { driverGuard } from './core/guards/driver.guard';
import { RequestDriver } from './pages/customer/request-driver/request-driver';
import { MyRequests } from './pages/customer/my-requests/my-requests';
import { DriverRequests } from './pages/driver/requests/requests';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'register',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: Login,
  },
  {
    path: 'register',
    component: Register,
  },
  {
    path: 'customer',
    component: CustomerDashboard,
    canActivate: [customerGuard],
  },
  {
    path: 'customer/request-driver',
    component: RequestDriver,
    canActivate: [customerGuard],
  },
  {
    path: 'customer/requests',
    component: MyRequests,
    canActivate: [customerGuard],
  },
  {
    path: 'driver',
    component: DriverDashboard,
    canActivate: [driverGuard],
  },
  {
    path: 'driver/requests',
    component: DriverRequests,
    canActivate: [driverGuard],
  },
  {
    path: 'profile',
    component: Profile,
    canActivate: [authGuard],
  },
];
