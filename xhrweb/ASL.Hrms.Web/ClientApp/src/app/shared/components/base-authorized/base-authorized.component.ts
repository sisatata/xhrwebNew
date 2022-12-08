import { Component, OnInit, Inject, Injector } from '@angular/core';
import { AuthService } from 'src/app/auth/services/auth.service';
import { Router } from '@angular/router';
import { AutoLogoutService } from '../../services/auto-logout.service';
import { PaginatorBase } from '../../models';

@Component({
  selector: 'app-base-authorized',
  templateUrl: './base-authorized.component.html',
  styleUrls: ['./base-authorized.component.css']
})
export class BaseAuthorizedComponent extends PaginatorBase {
 
  //@Inject(AuthService)
  public authService: AuthService;
  //autoLogoutService: AutoLogoutService;
  companyId:any;
  //@Inject(Router)
  router: Router
  constructor(injector: Injector) {    
    super(injector);
    
    this.authService = injector.get(AuthService);
    this.router = injector.get(Router);
    this.setUserDatas();
    //this.autoLogoutService = injector.get(AutoLogoutService);
    
    //this.checkAuthentication(); 
  }
  
  // checkAuthentication(){
    
  //   if(!this.authService.checkAuthenticated()){
  //     this.router.navigate(['/auth/login']);
  //   }

  //   this.autoLogoutService.setInitialDatas();
  // }

  setUserDatas() {
   this.companyId= this.authService.getLoggedInUserInfo().companyId;
  }

}
