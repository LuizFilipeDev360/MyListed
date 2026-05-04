import { CanActivateFn } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

export const roleGuard: CanActivateFn = (route, state) => {
  const token = localStorage.getItem('token');
  if (!token) return false;

  const decoded: any = jwtDecode(token);

  const userRole =
    decoded["role"] ||
    decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

  const allowedRoles = route.data?.['roles'] as string[];

  if (!allowedRoles) return false;

  if (Array.isArray(userRole)) {
    return userRole.some(r => allowedRoles.includes(r));
  }

  return allowedRoles.includes(userRole);
};
