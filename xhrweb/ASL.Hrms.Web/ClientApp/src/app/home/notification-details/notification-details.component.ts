import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-notification-details',
  templateUrl: './notification-details.component.html',
  styleUrls: ['./notification-details.component.css']
})
export class NotificationDetailsComponent implements OnInit {
notifications:any;
  constructor(@Inject(MAT_DIALOG_DATA) data, private dialogRef: MatDialogRef<NotificationDetailsComponent>) { 
    this.notifications = data;
    
  }

  ngOnInit(): void {
  }
close():void{
  this.dialogRef.close();
}
}
