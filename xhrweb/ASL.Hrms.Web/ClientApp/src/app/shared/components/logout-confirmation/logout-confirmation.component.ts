import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SharedService } from '../../services/shared.service';


@Component({
  selector: 'app-logout-confirmation',
  templateUrl: './logout-confirmation.component.html',
  styleUrls: ['./logout-confirmation.component.css']
})

export class LogoutConfirmationComponent implements OnInit {
  confirmationMessage: string;
  onLogoutOkEvent: EventEmitter<any> = new EventEmitter();
  onLogoutCancelEvent: EventEmitter<any> = new EventEmitter();
  submitted: boolean;


  constructor(private dialogRef: MatDialogRef<LogoutConfirmationComponent>,
    private sharedService: SharedService,
    @Inject(MAT_DIALOG_DATA) data) {

    if (data !== null) {
      this.confirmationMessage = data;
    }
  }

  ngOnInit() {
    this.sharedService.loggoutMessage.subscribe(message => this.confirmationMessage = message);
    this.sharedService.isLoggedout.subscribe(result => {
      if (result == true) {
        this.close();
      }
    });

  }

  close() {
    this.dialogRef.close();
    this.onLogoutCancelEvent.emit();
  }

  confirm() {
    this.submitted = true;
    this.onLogoutOkEvent.emit();
  }
}



