import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanActivateChild } from '@angular/router';
import { Injectable } from "@angular/core";
import { AuthService } from './auth.service';

import { User } from '../models/User';
import { LoginModel } from '../models/Login';
import { environment } from '../../../environments/environment';
import { CursorError } from '@angular/compiler/src/ml_parser/lexer';

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild {
  constructor(private router: Router, private authService: AuthService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    // if(this.authService.checkAuthenticated()==false){
    //   return false;
    // }

    const currentUser = this.authService.currentUserValue;
    if (currentUser) {
      // check if route is restricted by role
      //if (route.data.roles && route.data.roles.indexOf(currentUser.userRoles[0]) === -1) {
      //  // role not authorised so redirect to home page
      //  this.router.navigate(['/']);
      //  return false;
      //}


//       if (route.data.roles && (route.data.roles.indexOf('Administrators') === -1
//         && route.data.roles.indexOf('SystemAdmin') === -1
//         && route.data.roles.indexOf('EmployeeRole') === -1)) {
//         // role not authorised so redirect to home page
//         this.router.navigate(['/']);
//         return false;
//       }
// console.log(route.data.roles);
// console.log(currentUser.userRoles);
      if(route.data.roles){
        const roles = route.data.roles.filter(item=> currentUser.userRoles.includes(item));
        if(roles.length == 0){
          this.router.navigate(['/']);
            return false;
        }
      }
      // authorised so return true
      return true;
    }

    // not logged in so redirect to login page with the return url
   // this.router.navigateByUrl('/auth/login', { queryParams: { returnUrl: state.url } });
   this.router.navigate(['/auth/login'], { queryParams: { returnUrl: state.url }});
    return false;
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    return this.canActivate(route, state);
  }
}
