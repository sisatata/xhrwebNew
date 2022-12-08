import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CountChartService } from 'src/app/shared/services';
@Component({
  selector: 'app-notice-details',
  templateUrl: './notice-details.component.html',
  styleUrls: ['./notice-details.component.css']
})
export class NoticeDetailsComponent implements OnInit {
  companyId: any = localStorage.getItem('companyId');
  noticeFilterFormGroup: FormGroup;
  loading: boolean = false;
  notices: any;
  constructor(
    private dialogRef: MatDialogRef<NoticeDetailsComponent>,
    private formBuilder: FormBuilder,
    private countChartService: CountChartService,
    @Inject(MAT_DIALOG_DATA) data,
  ) {
    this.notices = data;
   }
  ngOnInit(): void {
    
  }
  close():void{
    this.dialogRef.close();
  }
  
}
