import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router , private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        console.log(error);
        if(error){
          switch(error.status) {
            case 400: {
              console.log(Array.isArray(error.error))
              if(Array.isArray(error.error)){
                for(let er of error.error){
                  this.toastr.error(er.description);
                }
              }
              else if(error.error.errors){
                const modalStateErrors = [];
                for(const key in error.error.errors){
                  if(error.error.errors[key]){
                    modalStateErrors.push(error.error.errors[key])
                  }
                }
                throw modalStateErrors.reduce((acc, val) => acc.concat(val), []);
              } else if(typeof(error.error) === 'object') {
                  this.toastr.error(error.statusText, error.status);
              }
              else {
                this.toastr.error(error.error, error.status);
                
              }
              console.log(error);

              break;
            }
            
            case 401: {
              if(error.error === null){
                this.toastr.error("Unauthorized")
              }
              else if(typeof(error.error) === 'object'){
                this.toastr.error(error.error.status, error.error.title);
              }
              else{
                this.toastr.error(error.error, error.status);
              }
              console.log(error);

              break;
            }

            case 404: {
              this.router.navigateByUrl('/not-found');
              console.log(error);

              // this.toastr.error(error.statusText, error.status);
              break;
            }

            case 500: {
              const navigationExtras: NavigationExtras = {state: {error: error.error}}
              console.log(error);
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;
            }

            default: {
              this.toastr.error('Something unexpected went wrong');
              console.log(error);
              break;
            }
              
          }
        }
        return throwError(error);
      })
    );
  }
}
