import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
@Component({
  selector: 'app-employee-confirmation-list',
  templateUrl: './employee-confirmation-list.component.html',
  styleUrls: ['./employee-confirmation-list.component.css']
})
export class EmployeeConfirmationListComponent implements OnInit {
  confirmedEmployees: any;
  constructor(@Inject(MAT_DIALOG_DATA) data, private dialogRef: MatDialogRef<EmployeeConfirmationListComponent>) {
    this.confirmedEmployees = data;
  }
  ngOnInit(): void {
    
  }
  close(): void {
    this.dialogRef.close();
  }
}
