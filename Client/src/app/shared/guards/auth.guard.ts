import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { map } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = () => {
  const router = inject(Router);
  const authService = inject(AuthService);

  return authService.isAuthorized.pipe(
    map(isAuthorized => isAuthorized || router.parseUrl('/auth')),
  );
};

export const authComponentGuard: CanActivateFn = () => {
  const router = inject(Router);
  const authService = inject(AuthService);

  return authService.isAuthorized.pipe(
    map(isAuthorized => isAuthorized ? router.parseUrl('/') : true),
  );
};
