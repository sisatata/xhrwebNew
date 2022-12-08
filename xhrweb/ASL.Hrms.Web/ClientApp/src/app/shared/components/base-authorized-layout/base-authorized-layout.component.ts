import { Component, OnInit, Injector } from '@angular/core';
import { AuthService } from 'src/app/auth/services/auth.service';
import { AutoLogoutService } from '../../services/auto-logout.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-base-authorized-layout',
  templateUrl: './base-authorized-layout.component.html',
  styleUrls: ['./base-authorized-layout.component.css']
})

export class BaseAuthorizedLayoutComponent {

  //@Inject(AuthService)
  public authService: AuthService;
  autoLogoutService: AutoLogoutService;
  companyId: any;
  //@Inject(Router)
  router: Router
  constructor(injector: Injector) {
    this.authService = injector.get(AuthService);
    this.router = injector.get(Router);
    this.autoLogoutService = injector.get(AutoLogoutService);

    this.checkAuthentication();

    this.setUserDatas();

  }

  checkAuthentication() {
    if (!this.authService.checkAuthenticated()) {
      this.router.navigate(['/auth/login']);
    }
   // this.autoLogoutService.setInitialDatas();
  }

  setUserDatas() {
    this.companyId = this.authService.getLoggedInUserInfo().companyId;
  }

}

