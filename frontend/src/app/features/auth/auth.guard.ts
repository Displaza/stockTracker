// auth.guard.ts
import { CanActivateFn, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from './auth.service';

export const authGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  
  //first check if user is authenticated
  if (authService.isAuthenticated()) {
    // Check for role-based access if specified in route data
    const requiredRole = route.data['role'];
    //if the route role doesn't match the user role, redirect to unauthorized.
    if (requiredRole && !authService.hasRole(requiredRole)) {
      router.navigate(['/unauthorized']);
      return false;
    }
    return true;
  }
  
  // Redirect to login page with return url
  router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
  return false;
};

// Usage in routing (app.routes.ts):
// {
//   path: 'dashboard',
//   component: DashboardComponent,
//   canActivate: [authGuard]
// },
// {
//   path: 'admin',
//   component: AdminComponent,
//   canActivate: [authGuard],
//   data: { role: 'Admin' }
// }