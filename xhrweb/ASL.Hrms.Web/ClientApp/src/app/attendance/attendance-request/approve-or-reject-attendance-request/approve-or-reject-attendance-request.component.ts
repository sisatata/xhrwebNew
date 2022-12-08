import { Component, EventEmitter, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ApproveOrDeclineEnums } from 'src/app/enums/transferPromotion';
import { ApproveOrRejectAttendanceRequest } from 'src/app/shared/models';
import { AttendanceRequestService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
  selector: 'app-approve-or-reject-attendance-request',
  templateUrl: './approve-or-reject-attendance-request.component.html',
  styleUrls: ['./approve-or-reject-attendance-request.component.css']
})
export class ApproveOrRejectAttendanceRequestComponent implements OnInit {
  onAttendanceRequestApproveOrRejectCreateEvent: EventEmitter<boolean> =
  new EventEmitter();
  approveOrRejectAttendanceRequestForm: FormGroup;
  approveOrRejectAttendanceRequest: ApproveOrRejectAttendanceRequest;
  submitted = false;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  id: any;
  type: number;
  constructor(
    private dialogRef: MatDialogRef<ApproveOrRejectAttendanceRequestComponent>,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private attendanceRequestService: AttendanceRequestService,
    @Inject(MAT_DIALOG_DATA) data
  ) { 
    this.type = data.type;
    this.id = data.id;
  }

  ngOnInit(): void {
    this.buildForm();
  }
  buildForm(): void {
    this.approveOrRejectAttendanceRequestForm =
      this.formBuilder.group({
        id: [this.id, [Validators.required]],
        note: ['', [Validators.maxLength(250)]],
      });
  }
  onSubmit(): void {
    this.submitted = true;
    if (this.approveOrRejectAttendanceRequestForm.invalid) {
      return;
    } else if (this.type === ApproveOrDeclineEnums.Approve) {
      this.approveAttendanceRequest();
    } else {
      this.declineAttendanceRequest();
    }
  }
  approveAttendanceRequest():void{
    this.loading = true;
    this.approveOrRejectAttendanceRequest = new ApproveOrRejectAttendanceRequest(this.approveOrRejectAttendanceRequestForm.value);
    this.attendanceRequestService.approveAttendanceRequest(this.approveOrRejectAttendanceRequest).subscribe(res=>{
      this.loading = false;
      if ((res as any).status === false) {
        this.isFormValid = false;
        this.errorMessages = (res as any).message;
      } else {
        this.isFormValid = true;
        this.onAttendanceRequestApproveOrRejectCreateEvent.emit(true);
        this.alertService.success("Approve successful");
        this.close();
      }
    },()=>{

    })
  }
  declineAttendanceRequest():void{
    this.loading = true;
    this.approveOrRejectAttendanceRequest = new ApproveOrRejectAttendanceRequest(this.approveOrRejectAttendanceRequestForm.value);
    this.attendanceRequestService.rejectAttendanceRequest(this.approveOrRejectAttendanceRequest).subscribe(res=>{
      this.loading = false;
      if ((res as any).status === false) {
        this.isFormValid = false;
        this.errorMessages = (res as any).message;
      } else {
        this.isFormValid = true;
        this.onAttendanceRequestApproveOrRejectCreateEvent.emit(true);
        this.alertService.success("Decline successful");
        this.close();
      }
    },()=>{

    })
  }
  close():void{
    this.dialogRef.close();
  }
  get f() {
    return this.approveOrRejectAttendanceRequestForm.controls;
  }
}
