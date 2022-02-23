import { Injectable } from '@angular/core';
import { HttpClient,HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
/*import { catchError } from 'rxjs/internal/operators';*/
import { AppComponent } from '../app.component';
import { ServerurlService } from './serverurl.service';

import { LoginViewModel } from '../ViewModels/Authentication/LoginViewModel';
import { RegisterViewModel } from '../ViewModels/Authentication/RegisterViewModel';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient, private Serverurl: ServerurlService) { }

  Register(RegisterViewModel: RegisterViewModel): Observable<any> {
    console.log(this.Serverurl.Url + '/Authentication/Register');
    return this.http.post<any>(this.Serverurl.Url + '/Authentication/Register', RegisterViewModel)
      .pipe(catchError(this.ErrorHandler));
  }

  ErrorHandler(error: HttpErrorResponse) {
    /*alert(JSON.stringify( error));
    console.log(error);
    return Observable.throw(error);*/
    return throwError(error);
  }


  Login(LoginViewModel: LoginViewModel): Observable<any> {
    return this.http.post<any>(this.Serverurl.Url + "/Authentication/Login", LoginViewModel);
  }

  obtenerToken(): string  {
    return localStorage.getItem("token")!;
  }

  obtenerExpiractionToken(): string 
  {
    return localStorage.getItem("tokenExpiration")!;
  }

  Logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("tokenExpiration");
    console.log(this.Serverurl.Url + "/Authentication/Logout");
    return this.http.get(this.Serverurl.Url + "/Authentication/Logout").pipe(catchError(this.ErrorHandler));
  }

  IsLoggedIn(): boolean {
    var exp = this.obtenerExpiractionToken();
    if (!exp) {
      return false;
    }
    var now = new Date().getTime();
    var DateExp = new Date(exp);
    if (now >= DateExp.getTime()) {
      localStorage.removeItem("token");
      localStorage.removeItem("tokenExpiration");
      return false;
    }
    else {
      return true;
    }
  }
}
