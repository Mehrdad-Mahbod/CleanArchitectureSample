import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
/*import { catchError } from 'rxjs/internal/operators';*/
/*New Version*/
import { catchError } from 'rxjs';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PublichttpService {

  constructor(private http: HttpClient) { }

  HttpPost(Url:string, Ob: any): Observable<any> {
    return this.http.post<any>(Url, Ob)
      .pipe(catchError(this.ErrorHandler));
  }


  HttpGet(Url: string): Observable<any> {
    return this.http.get(Url);
  }

  HttpGetWithObject(Url: string, Ob: any): Observable<any> {
    return this.http.post<any>(Url, Ob)
    .pipe(catchError(this.ErrorHandler));
  }


  ErrorHandler(error: HttpErrorResponse) {
    return throwError(error);
  }
}
