import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpRequest, HttpHandler, HttpEventType, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';

@Injectable()
export class AuthinterceptorInterceptor implements HttpInterceptor {

  constructor(public route:Router) {}

  intercept(httpRequest: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // return next.handle(request);
    var token = localStorage.getItem('token');
    var c_code = localStorage.getItem('c_code');
    var url = environment.API_URL;
    if (token == '' || token == null) {
      const req = httpRequest.clone({
        url: url + httpRequest.url,
        headers: httpRequest.headers.set('Access-Control-Allow-Origin', '*')
      });
      return next.handle(req).pipe(
        tap({
          next:(event)=>{
            if(event instanceof HttpResponse)
            {
              if(event.status == 500)
              {
                this.route.navigate(['auth/500']);
              } 
              else if(event.status == 401)
              {
                this.route.navigate(['auth/401'])
              }
              else if(event.status == 404)
              {
                this.route.navigate(['auth/404'])
              }
              return;
            }
          },
          error: (error) => {
            if(error.status === 401) {
              this.route.navigate(['auth/401'])
            }
            else if(error.status === 404) {
              this.route.navigate(['auth/404']);
            }
            else if(error.status === 500) {
              this.route.navigate(['auth/500']);
            }
          }
        })
      )
     
    }
    else {
      c_code = (c_code == "" || c_code == null)? "":c_code;
      const req = httpRequest.clone({
        url: url + httpRequest.url,
        headers: httpRequest.headers.set('Access-Control-Allow-Origin', '*')
          .set('Authorization', token)
          .set('C_Code', c_code)
          // .set('Content-Type', 'application/json')
      });
      return next.handle(req).pipe(
        tap({
          next:(event)=>{
            if(event instanceof HttpResponse)
            {
              if(event.status == 500)
              {
                this.route.navigate(['auth/500']);
              } 
              else if(event.status == 401)
              {
                this.route.navigate(['auth/401'])
              }
              else if(event.status == 404)
              {
                this.route.navigate(['auth/404'])
              }
            }
          },
          error: (error) => {
            if(error.status === 401) {
              this.route.navigate(['auth/401'])
            }
            else if(error.status === 404) {
              this.route.navigate(['auth/404']);
            }
            else if(error.status === 500) {
              this.route.navigate(['auth/500']);
            }
          }
        })
      )
    }
  }
}
