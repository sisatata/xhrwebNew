import { Injectable } from '@angular/core';
import { Idle, DEFAULT_INTERRUPTSOURCES } from '@ng-idle/core';
import { Keepalive } from '@ng-idle/keepalive';
import { Router } from '@angular/router';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { LogoutConfirmationComponent } from '../components/logout-confirmation/logout-confirmation.component';
import { AuthService } from '../../auth/services/auth.service';
import { BehaviorSubject } from 'rxjs';
import { SharedService } from './shared.service';

@Injectable({
  providedIn: 'root'
})
export class AutoLogoutService {

  // idleState = 'Not started.';
  // timedOut = false;
  // lastPing?: Date = null;
  // title = 'angular-idle-timeout';

  // constructor(private idle: Idle, private keepalive: Keepalive, private router: Router,
  //   private dialog: MatDialog, private authService: AuthService, private sharedSevice: SharedService) {
  //   //this.setInitialDatas();
  // }

  // setInitialDatas() {

  //   var tokenExpirationTime = this.authService.getTokenExpirationDate();

  //   var diff = Math.abs(tokenExpirationTime.valueOf() - new Date().valueOf());


  //   var minutes = Math.floor((diff / 1000) / 60);

  //   var waitingSeconds = minutes * 60;


  //   if (!waitingSeconds || waitingSeconds < 0) {
  //     waitingSeconds = 0;
  //   }
  //   // sets an idle timeout of 5 seconds, for testing purposes.
  //   if (waitingSeconds > 0)
  //     this.idle.setIdle(waitingSeconds);
  //   // sets a timeout period of 5 seconds. after 10 seconds of inactivity, the user will be considered timed out.
  //   //this.idle.setTimeout(10);
  //   // sets the default interrupts, in this case, things like clicks, scrolls, touches to the document
  //   this.idle.setInterrupts(DEFAULT_INTERRUPTSOURCES);

  //   this.idle.onIdleEnd.subscribe(() => {
  //     this.idleState = 'No longer idle.'
  //     this.sharedSevice.messageSource.next(this.idleState);
  //     this.reset();
  //   });

  //   this.idle.onTimeout.subscribe(() => {
  //     this.idleState = 'Timed out!';
  //     this.sharedSevice.messageSource.next(this.idleState);
  //     this.timedOut = true;
  //     this.sharedSevice.loggedOutMessageSource.next(true);

  //     this.router.navigate(['/auth/login']);
  //   });

  //   this.idle.onIdleStart.subscribe(() => {
  //     this.idleState = 'You\'ve gone idle!'
  //     this.sharedSevice.messageSource.next(this.idleState);
  //     this.showLogoutReminderModal();

  //   });

  //   this.idle.onTimeoutWarning.subscribe((countdown) => {
  //     this.idleState = 'You will log out in ' + countdown + ' seconds!'
  //     this.sharedSevice.messageSource.next(this.idleState);

  //   });

  //   // sets the ping interval to 15 seconds
  //   this.keepalive.interval(15);

  //   this.keepalive.onPing.subscribe(() => this.lastPing = new Date());

  //   this.reset();
  // }
  // reset() {
  //   this.idle.watch();
  //   this.idleState = 'Started.';
  //   this.timedOut = false;
  // }

  // showLogoutReminderModal() {
  //   const logoutConfirmationDialogConfig = new MatDialogConfig;
  //   // Setting different dialog configurations
  //   logoutConfirmationDialogConfig.disableClose = true;
  //   logoutConfirmationDialogConfig.autoFocus = true;
  //   logoutConfirmationDialogConfig.panelClass = "xHR-dialog";
  //   logoutConfirmationDialogConfig.width = "50%";
  //   logoutConfirmationDialogConfig.data = this.idleState;

  //   const createEmployeeDialog = this.dialog.open(LogoutConfirmationComponent, logoutConfirmationDialogConfig);
  //   const successfullCreate = createEmployeeDialog.componentInstance.onLogoutOkEvent.subscribe((data) => {
  //     this.authService.logout();
  //   });
  //   createEmployeeDialog.afterClosed().subscribe(() => {
  //   });

  // }

}
