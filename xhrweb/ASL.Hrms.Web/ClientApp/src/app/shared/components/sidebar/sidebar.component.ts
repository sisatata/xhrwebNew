import { Component, OnInit, OnDestroy, ChangeDetectorRef, Input, Renderer2 } from '@angular/core';
import { Router} from '@angular/router';
import { MediaMatcher } from '@angular/cdk/layout';
import { SharedService } from '../../services/shared.service';
import { AuthService } from 'src/app/auth/services/auth.service';
import { EventEmitterService } from 'src/app/auth/services/event-emitter.service';
import { ElementRef } from '@angular/core';
import { ViewChild } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  showCollapsed: boolean = false;
  isSystemAdmin: boolean = false;
  isAdmin: boolean = false;
  isEmployee: boolean = false;
  employeeId: any;
  loggedInUserInfo: any;
  paddingLeft:any;
  userName: any;
  isPayrollManager:any;
  isSuperAdmin:any;
  sidebarWidth:number = -1;
  isReportingManager: any;
  

  constructor(private _sharedService: SharedService,
    private eventEmitterService: EventEmitterService,
    private router: Router,
    private rendere : Renderer2,
    private authService: AuthService) {
    this._sharedService.showSidebarCollapsed.subscribe((showSidebarCollapsed: boolean) => {
      this.showCollapsed = showSidebarCollapsed;
    });

    this.loggedInUserInfo = this.authService.getLoggedInUserInfo();
    this.isPayrollManager = this.authService.isPayrollManger;
    this.isSuperAdmin = this.authService.isSuperAdmin;
    this.isSystemAdmin = this.authService.isSystemAdmin;
    this.isReportingManager = this.authService.isReportingManager;
    this.isAdmin = this.authService.isAdmin;
    this.isEmployee = this.authService.isEmployee;
    this.employeeId = this.loggedInUserInfo.employeeId;
    this.userName = this.loggedInUserInfo.displayName;
    this.isPayrollManager = this.authService.isPayrollManger;
    this.eventEmitterService.logoutEvent.subscribe(data => {
      if (data) {
        this.isAdmin = this.isEmployee = false;
        this.eventEmitterService.emitLogoutEvent(false);
        this.router.navigateByUrl('/auth/login',);
      }
    });
  
  }
  ngOnInit():void{
    this.sidebarWidth =   (<HTMLInputElement>document.getElementById("sidebarMenu")).offsetWidth;
  }
  expandSideMenu():void{
    (<HTMLInputElement>document.getElementById("sidebarMenu")).style.width=`${this.sidebarWidth.toString()}px`;
    (<HTMLInputElement>document.getElementById("float-icon")).style.display="none";
    (<HTMLInputElement>document.getElementById("mainBody")).style.marginLeft="auto";
    (<HTMLInputElement>document.getElementById("mainBody")).classList.replace('col-lg-12','col-lg-10');
    this.paddingLeft =  (<HTMLInputElement>document.getElementById("mainBody")).style.paddingLeft;
    (<HTMLInputElement>document.getElementById("mainBody")).style.paddingLeft=`${this.paddingLeft}px`;

    (<HTMLInputElement>document.getElementById("float-icon")).style.display="none";
    (<HTMLInputElement>document.getElementById("sidebarMenu")).style.width=`${this.sidebarWidth.toString()}px`;
    (<HTMLInputElement>document.getElementById("mainBody")).classList.replace('col-lg-12','col-lg-10');
    const cardChartWidth = window.screen.width ;
    if(cardChartWidth>=992 && cardChartWidth<=1199){
    (<HTMLInputElement>document.getElementById("mainBody")).style.paddingLeft="100px";
    }
    else{
      (<HTMLInputElement>document.getElementById("mainBody")).style.paddingLeft="unset";
    }
    if(cardChartWidth<=990 && cardChartWidth>768){
      (<HTMLInputElement>document.getElementById("mainBody")).style.marginLeft="auto";
      (<HTMLInputElement>document.getElementById("mainBody")).classList.replace('col-md-12','col-md-9');
    }
  }
}
