import { HttpEvent, HttpEventType, HttpHandler, HttpHandlerFn, HttpRequest, HttpStatusCode } from "@angular/common/http";
import { Observable, tap } from "rxjs";

export function ErrorResponseInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn)
: Observable<HttpEvent<unknown>> {
    return next(req).pipe(tap(event => {
        if (event.type === HttpEventType.Response) {
            if (event.status != HttpStatusCode.Ok) {
                //console.error(event.body);
            }
        }
    }));
}