import { Injectable } from '@angular/core';
import { Subject, Observable, BehaviorSubject } from 'rxjs';
import { Router, NavigationStart } from '@angular/router';
import { AlertType, Alert } from '../models';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})


export class  AlertService {

  private subject = new BehaviorSubject<Alert>(null);
  private keepAfterRouteChange = false;

  constructor(private router: Router, public snackBar:MatSnackBar) {
    // clear alert messages on route change unless 'keepAfterRouteChange' flag is true
    router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        if (this.keepAfterRouteChange) {
          // only keep for a single route change
          this.keepAfterRouteChange = false;
        }
        else {
          this.clear();
        }
      }
    });
  }

  getAlert(): Observable<any> {
    return this.subject.asObservable();
  }

  success(message: string, keepAfterRouteChange = false) {
    this.alert(AlertType.Success, message, keepAfterRouteChange);
  }

  error(message: string, keepAfterRouteChange = false) {
    if (this.router.url != "/auth/login") {
      this.alert(AlertType.Error, message, keepAfterRouteChange);
    }
  }

  info(message: string, keepAfterRouteChange = false) {
    this.alert(AlertType.Info, message, keepAfterRouteChange);
  }

  warn(message: string, keepAfterRouteChange = false) {
    this.alert(AlertType.Warning, message, keepAfterRouteChange);
  }

  clear() {
    this.subject.next(null);
  }


  cssClass(alertType: AlertType) {
    switch (alertType) {
      case AlertType.Success:
        return ['alert','alert-success'];
      case AlertType.Error:
        return ['alert', 'alert-danger'];
      case AlertType.Info:
        return ['alert' ,'alert-info'];
      case AlertType.Warning:
        return ['alert', 'alert-warning'];
    }
  }

  alert(type: AlertType, message: string, keepAfterRouteChange = false) {
    this.keepAfterRouteChange = keepAfterRouteChange;
    this.subject.next(<Alert>{ type: type, message: message });
    // subject is not working here. Not calling alert.component from here.

    var snackBarClass = this.cssClass(type);

    this.snackBar.open(message, '', {
      duration: 5000,
      verticalPosition:'top',
      horizontalPosition :'right',
      panelClass: snackBarClass
    });
  }
}
