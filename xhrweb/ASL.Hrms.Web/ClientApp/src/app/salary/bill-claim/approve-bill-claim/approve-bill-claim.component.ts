import { Component, EventEmitter, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ApproveBillClaim, BillClaim } from 'src/app/shared/models';
import { BenefitBillClaimService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-approve-bill-claim',
  templateUrl: './approve-bill-claim.component.html',
  styleUrls: ['./approve-bill-claim.component.css']
})
export class ApproveBillClaimComponent implements OnInit {
  onBillApproveOrDeclinedEvent: EventEmitter<any> = new EventEmitter();
  billVerdictForm: FormGroup;
  errorMessages: any[] = [];
  errorMessage: string;
  submitted = false;
  isEditMode = false;
  approveBenefitBillClaim: ApproveBillClaim = new ApproveBillClaim();
  branchId: any;
  departmentId: any;
  positionId: any;
  companies: any;
  departments: any;
  branches: any;
  loading: boolean = false;
  companyId: any;
  billClaim: ApproveBillClaim;
  employeeId: string;
  positions: any;
  genders: any;
  nationalities: any;
  hidePassword: boolean = true;
  hideConfirmPassword: boolean = true;
  isFormValid: boolean;
  constructor(
    private dialogRef: MatDialogRef<ApproveBillClaimComponent>,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private benefitBillClaimService: BenefitBillClaimService,
    @Inject(MAT_DIALOG_DATA) data
  ) {
    this.billClaim = new ApproveBillClaim(data);
  }
  ngOnInit(): void {
    this.buildForm();
  }
  buildForm() {
    this.billVerdictForm = this.formBuilder.group({
      Id: [this.billClaim.benefitBillClaimId, [Validators.required]],
      claimAmount: [{ value: this.billClaim.claimAmount, disabled: true }, [Validators.required]],
      approvedAmount: [this.billClaim.claimAmount, [Validators.required]],
      approvedNotes: [this.billClaim.approvedNotes, [Validators.required]]
    });
  }
  reject(): void {
    if (this.billVerdictForm.invalid) {
      return;
    }
    this.loading = true;
    this.billClaim = new ApproveBillClaim(this.billVerdictForm.value);
    this.benefitBillClaimService.rejectBillClaim(this.billClaim).subscribe(res => {
      if ((res as any).status === true) {
        this.onBillApproveOrDeclinedEvent.emit(true);
        this.alertService.success("Benefit bill claim successfully rejected");
        this.isFormValid = true;
        this.close();
      }
      else {
        this.errorMessages = (res as any).message;
        this.isFormValid = false;
      }
      this.loading = false;
    }, () => {
      this.loading = false;
    }, () => {
    })
  }
  approve(): void {
    if (this.billVerdictForm.invalid) {
      return;
    }
    this.loading = true;
    this.billClaim = new ApproveBillClaim(this.billVerdictForm.value);
    this.benefitBillClaimService.approveBillClaim(this.billClaim).subscribe(res => {
      if ((res as any).status === true) {
        this.onBillApproveOrDeclinedEvent.emit(true);
        this.close();
        this.alertService.success("Benefit bill claim successfully approved");
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (res as any).message;
      }
    }, () => {
      this.loading = false;
    }, () => {
    })
  }
  close(): void {
    this.dialogRef.close();
  }
  get f() { return this.billVerdictForm.controls; }
}
