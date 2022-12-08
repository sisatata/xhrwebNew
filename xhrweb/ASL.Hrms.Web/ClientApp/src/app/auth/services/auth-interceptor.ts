import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from "@angular/common/http";
import { Router } from "@angular/router";
import { Observable, throwError } from "rxjs";
import { tap, retry, catchError } from "rxjs/operators";
import { Injectable } from "@angular/core";
import { AuthService } from "./auth.service";
import { EventEmitterService } from "./event-emitter.service";


@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private router: Router, private authService: AuthService,
    private eventEmitterService: EventEmitterService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // add authorization header with jwt token if available
    let currentUser = this.authService.currentUserValue;
    if (currentUser && currentUser.bearerToken) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.bearerToken}`
        }
      });
    }

    return next.handle(request).pipe(
      catchError((error: any) => {
        if (error.status === 401) {
          this.eventEmitterService.emitLogoutEvent(true);
          this.router.navigateByUrl('/auth/login');
        }
        return throwError(error);
      })
    );
  }
}
