import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { RoleEnum } from '../models/role';

export const adminGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const isGranted =  authService.isGranted(RoleEnum.ADMIN);

  if (!isGranted) {
    router.navigate(['/']);
    return false;
  }
  return true;
};
