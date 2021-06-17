import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { HttpStatusCode } from '../constants/http-status-code';
import Swal from 'sweetalert2';

export const successStatusCodes = [
    HttpStatusCode.OK, 
    HttpStatusCode.Created, 
    HttpStatusCode.Accepted, 
    HttpStatusCode.NoContent
];

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private router: Router
  ) {
  }

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler,
  ) {
    return next.handle(request).pipe(
      catchError((response: HttpErrorResponse) => {

        if (!successStatusCodes.includes(response.status)) {
            Swal.fire({
                icon: 'error',
                title: 'Erro',
                text: response.error[0].description
              })
          return;
        }
        return throwError(response);
      })
    );
  }
}
