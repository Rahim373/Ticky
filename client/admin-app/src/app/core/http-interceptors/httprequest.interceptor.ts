import { HttpErrorResponse, HttpHandler, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { AuthService } from '../../services/auth.service';
import { inject } from '@angular/core';

export const HttpRequestInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn) => {
  const authService: AuthService = inject(AuthService);
  // const router: Router = inject(Router);

  const token = authService.getAccessToken();

  if (token) {
    const request = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    })
    return next(request);
  }

  return next(req);
};
