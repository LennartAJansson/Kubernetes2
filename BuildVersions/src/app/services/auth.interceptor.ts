//import { Injectable } from '@angular/core';
//import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
//import { Router } from '@angular/router';
//import { catchError, Observable, throwError } from 'rxjs';
//import { AuthService } from './auth.service';

//@Injectable({
//  providedIn: 'root'
//})

//export class AuthInterceptor implements HttpInterceptor {

//  constructor(private authService: AuthService,
//    private router: Router) {}

//  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
//    var token = this.authService.getToken();

//    if (token) {
//      request = request.clone({
//        setHeaders: {
//          Authorization: `Bearer ${token}`
//        }
//      });
//    }

//    return next.handle(request).pipe(
//      catchError((error) => {
//        // Perform logout on 401 - Unauthorized HTTP response errors
//        if (error instanceof HttpErrorResponse && error.status === 401) {
//          this.authService.logout();
//          this.router.navigate(['login']);
//        }
//        return throwError(error);
//      })
//    );
//  }
//}
