import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from '../../services/shared.service';
import { AuthService } from 'src/app/auth/services/auth.service';
import { EventEmitterService } from 'src/app/auth/services/event-emitter.service';
import { TranslateService } from '@ngx-translate/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ChangePasswordComponent } from 'src/app/auth/change-password/change-password.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isCollapsed = false;
  isAuthenticated = false;
  userId: any;
  userName: any;
  firstName: any;
  lastName: any;
  employeeId:any;
  paddingLeft:any;
  profilePictureSrc: string = 'assets/images/avatar.png';
  defaultProfilePictureSrc: string = 'assets/images/avatar.png';
  sidebarWidth:number = -1;
  constructor(private sharedService: SharedService,
    private authService: AuthService,
    private eventEmitterService: EventEmitterService,
    private dialog: MatDialog,
    private router: Router, public translate: TranslateService) {

    translate.addLangs(['en-us', 'bn-bd']);
    translate.setDefaultLang('en-us');

    const browserLang = translate.getBrowserLang();
    translate.use(browserLang.match(/en-us|bn-bd/) ? browserLang : 'en-us');




    this.eventEmitterService.logoutEvent.subscribe(data => {
      if (data) {
        this.isAuthenticated = this.isCollapsed = false;
        this.eventEmitterService.emitLogoutEvent(false);
      }
    });
  }

  async ngOnInit() {
    this.isAuthenticated = await this.authService.checkAuthenticated();
    this.paddingLeft =  (<HTMLInputElement>document.getElementById("mainBody")).style.paddingLeft;
   // console.log(this.paddingLeft)
    var loggedInUserInfo = await this.authService.getLoggedInUserInfo();
    if (loggedInUserInfo) {
      this.employeeId = loggedInUserInfo.employeeId;
      this.userName = loggedInUserInfo.displayName;


      this.profilePictureSrc =
        loggedInUserInfo.profilePictureUri && loggedInUserInfo.profilePictureUri != 'null'
          && loggedInUserInfo.profilePictureUri != 'undefined'
          ? loggedInUserInfo.profilePictureUri : this.defaultProfilePictureSrc;
    }
    else {
      this.userId = '';
      this.userName = '';
    }
  }
  ngAfterViewInit(){
    this.paddingLeft =  (<HTMLInputElement>document.getElementById("mainBody")).style;
    
  }

  collapseSidbar() {
    this.isCollapsed = !this.isCollapsed;
    this.sharedService.showSidebarCollapsed.next(this.isCollapsed);
  }

  logout() {
    this.isAuthenticated = false;
    this.authService.logout();
    this.eventEmitterService.emitLogoutEvent(true);
    this.router.navigate(['/auth/login']);


    // if (this.router.url.match('all-exams')) {
    //   this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
    //     this.router.navigate(['/all-exams']);
    //   });
    // }
    // else {
    //   this.router.navigate(['/all-exams']);
    // }
  }

  showBackButton(): boolean {
    if (this.router.url.match('employee-detail')) {
      return true;
    }
    return false;
  }
changePassword():void{
 
    const changePasswordDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    changePasswordDialogConfig.disableClose = true;
    changePasswordDialogConfig.autoFocus = true;
    changePasswordDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    changePasswordDialogConfig.width = `${dialogWidth}%`;
    const changePasswordDialog = this.dialog.open(ChangePasswordComponent,changePasswordDialogConfig);
    // const successfullCreate = createemployeePhoneDialog.componentInstance.onEmployeePhoneCreateEvent.subscribe((data) => {
     
    // });
    
  
}
  goBack() {
    window.history.back();
  }
  toggleSidebar():void{
  var currentWidth =   (<HTMLInputElement>document.getElementById("sidebarMenu")).offsetWidth;
  this.sidebarWidth = this.sidebarWidth === -1 ? currentWidth: this.sidebarWidth;
  if(currentWidth>0){
  
   
    (<HTMLInputElement>document.getElementById("sidebarMenu")).style.width="0";
    //(<HTMLInputElement>document.getElementById("mainBody")).style.marginLeft="30px";
    (<HTMLInputElement>document.getElementById("float-icon")).style.display="block";
    (<HTMLInputElement>document.getElementById("mainBody")).classList.replace('col-lg-10','col-lg-12');
   // this.paddingLeft =  (<HTMLInputElement>document.getElementById("mainBody")).style.paddingLeft;
    (<HTMLInputElement>document.getElementById("mainBody")).style.paddingLeft=`30px`;
    const cardChartWidth = window.screen.width ;
    if(cardChartWidth<=990 && cardChartWidth>768){
      (<HTMLInputElement>document.getElementById("mainBody")).style.marginLeft="unset";
      (<HTMLInputElement>document.getElementById("mainBody")).classList.replace('col-md-9','col-md-12');
    }
    

  }
  else{
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
   // (<HTMLInputElement>document.getElementById("mainBody")).style.marginLeft="auto";
    
  }
  }
}
