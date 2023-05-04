import {
  HttpErrorResponse,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export class AuthInterceptor implements HttpInterceptor {
  constructor() {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const token = localStorage.getItem('access_token');
    let request: HttpRequest<any> = req;

    if (token) {
      request = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${token}`),
      });
    }

    return next.handle(request).pipe(
      catchError((err: HttpErrorResponse) => {
        return throwError(err.error);
      })
    );
  }
}
