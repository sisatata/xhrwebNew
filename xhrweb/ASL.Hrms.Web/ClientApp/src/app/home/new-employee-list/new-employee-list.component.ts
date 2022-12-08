import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NoticeDetailsComponent } from '../notice-details/notice-details.component';

@Component({
  selector: 'app-new-employee-list',
  templateUrl: './new-employee-list.component.html',
  styleUrls: ['./new-employee-list.component.css']
})
export class NewEmployeeListComponent implements OnInit {
 employees:any;
 loading: boolean;
  constructor(
    @Inject(MAT_DIALOG_DATA) data,
    private dialogRef: MatDialogRef<NewEmployeeListComponent>,
  ) {
      this.employees = data;
     
   }

  ngOnInit(): void {
  }
  close():void{
    this.dialogRef.close();
  }

}
