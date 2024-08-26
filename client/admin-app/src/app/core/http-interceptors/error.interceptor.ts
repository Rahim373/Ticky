import { HttpEvent, HttpEventType, HttpHandler, HttpHandlerFn, HttpRequest, HttpStatusCode } from "@angular/common/http";
import { catchError, Observable, of, tap } from "rxjs";

export function ErrorResponseInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn)
: Observable<HttpEvent<unknown>> {
    return next(req).pipe(
        catchError(err => {
            console.error('Error while calling API', err);
            return of(err);
        })
    );
}