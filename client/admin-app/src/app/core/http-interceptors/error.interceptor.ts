import { HttpErrorResponse, HttpEvent, HttpEventType, HttpHandler, HttpHandlerFn, HttpRequest, HttpStatusCode } from "@angular/common/http";
import { inject } from "@angular/core";
import { NzMessageService } from "ng-zorro-antd/message";
import { catchError, Observable, of, tap, throwError } from "rxjs";
import { ErrorResponse } from "../models/errorResponse";

export function ErrorResponseInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn)
    : Observable<HttpEvent<unknown>> {
    let messageService = inject(NzMessageService);

    return next(req).pipe(
        catchError(err => {
            if (err instanceof HttpErrorResponse && err.status != HttpStatusCode.Ok) {
                try {
                    messageService.error((<(ErrorResponse)>err.error).detail);
                } catch {
                    messageService.error("Something went wrong.");
                }
            }
            return throwError(() => err);
        })
    );
}