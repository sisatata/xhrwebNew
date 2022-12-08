import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-active-employee-list',
  templateUrl: './active-employee-list.component.html',
  styleUrls: ['./active-employee-list.component.css']
})
export class ActiveEmployeeListComponent implements OnInit {
employees:any;
  constructor(
    @Inject(MAT_DIALOG_DATA) data,
    private dialogRef: MatDialogRef<ActiveEmployeeListComponent>,
  ) { 
    this.employees = data;
  }

  ngOnInit(): void {
  }
close():void{
  this.dialogRef.close();
}
}
