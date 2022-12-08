import { Component, EventEmitter, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ApproveOrDeclineEnums } from 'src/app/enums/transferPromotion';
import { ApproveOrRejectLeaveRequest } from 'src/app/shared/models';
import { LeaveApplicationService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
  selector: 'app-approve-or-reject-leave-request',
  templateUrl: './approve-or-reject-leave-request.component.html',
  styleUrls: ['./approve-or-reject-leave-request.component.css']
})
export class ApproveOrRejectLeaveRequestComponent implements OnInit {
  onleaveRequestApproveOrRejectCreateEvent: EventEmitter<boolean> =
  new EventEmitter();
  approveOrRejectLeaveRequestForm: FormGroup;
  employeeAddressCreateForm: FormGroup;
  approveOrRejectLeaveRequest: ApproveOrRejectLeaveRequest;
  submitted = false;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  id: any;
  type: number;
  constructor(
    private dialogRef: MatDialogRef<ApproveOrRejectLeaveRequestComponent>,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private leaveApplicationService: LeaveApplicationService,
    @Inject(MAT_DIALOG_DATA) data
  ) { 
    this.type = data.type;
    this.id = data.id;
   
  }

  ngOnInit(): void {
    this.buildForm();
  }
  buildForm(): void {
    this.approveOrRejectLeaveRequestForm =
      this.formBuilder.group({
        applicationId: [this.id, [Validators.required]],
        notes: ['', [Validators.maxLength(250)]],
      });
  }
  onSubmit(): void {
    this.submitted = true;
    if (this.approveOrRejectLeaveRequestForm.invalid) {
      return;
    } else if (this.type === ApproveOrDeclineEnums.Approve) {
      this.approveLeaveRequest();
    } else {
      this.rejectLeaveRequest();
    }
  }
  approveLeaveRequest():void{
    this.loading = true;
    this.approveOrRejectLeaveRequest = new ApproveOrRejectLeaveRequest(this.approveOrRejectLeaveRequestForm.value);
    this.leaveApplicationService.approveLeaveRequest(this.approveOrRejectLeaveRequest).subscribe(res=>{
      this.loading = false;
      if ((res as any).status === false) {
        this.isFormValid = false;
        this.errorMessages = (res as any).message;
      } else {
        this.isFormValid = true;
        this.onleaveRequestApproveOrRejectCreateEvent.emit(true);
        this.alertService.success("Approve successful");
        this.close();
      }
    },()=>{

    })
  }
  rejectLeaveRequest():void{
    this.loading = true;
    this.approveOrRejectLeaveRequest = new ApproveOrRejectLeaveRequest(this.approveOrRejectLeaveRequestForm.value);
    this.leaveApplicationService.rejectLeaveRequest(this.approveOrRejectLeaveRequest).subscribe(res=>{
      this.loading = false;
      if ((res as any).status === false) {
        this.isFormValid = false;
        this.errorMessages = (res as any).message;
      } else {
        this.isFormValid = true;
        this.onleaveRequestApproveOrRejectCreateEvent.emit(true);
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
    return this.approveOrRejectLeaveRequestForm.controls;
  }
}
