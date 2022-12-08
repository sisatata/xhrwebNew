import { Component, OnInit, Injector } from '@angular/core';
import { AuthService } from '../../../auth/services/auth.service';
import { Router } from '@angular/router';
import { BaseAuthorizedLayoutComponent } from "../../../shared/components/base-authorized-layout/base-authorized-layout.component";

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css']
})
export class MainLayoutComponent extends BaseAuthorizedLayoutComponent implements OnInit {

  constructor(
    injector: Injector) {
      super(injector);
  }

  ngOnInit(): void {
    //if (!this.authService.checkAuthenticated()) {
    //  this.router.navigateByUrl('/auth/login');
    //}
  }
}
