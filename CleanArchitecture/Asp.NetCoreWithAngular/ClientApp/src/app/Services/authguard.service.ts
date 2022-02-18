import { Injectable } from '@angular/core';
import { AuthenticationService } from './authentication.service';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthguardService {

  constructor(private authenticationService: AuthenticationService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.authenticationService.IsLoggedIn()) {
      return true;
    }
    else {
      //this.router.navigate(['/register-login']);
      this.router.navigate(['/Login']);
      return false;
    }
  }
}