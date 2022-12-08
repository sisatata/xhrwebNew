import { Component, EventEmitter, Inject, OnInit, Type } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { ApproveOrRejectEmployeePromotionTransfer } from "src/app/shared/models";
import { EmployeePromotionTransferService } from "src/app/shared/services";
import { AlertService } from "src/app/shared/services/alert.service";
import { ApproveOrDeclineEnums } from "../../../enums/transferPromotion";
@Component({
  selector: "app-approve-or-reject-employee-promotion-transfer",
  templateUrl: "./approve-or-reject-employee-promotion-transfer.component.html",
  styleUrls: ["./approve-or-reject-employee-promotion-transfer.component.css"],
})
export class ApproveOrRejectEmployeePromotionTransferComponent
  implements OnInit
{
  onEmployeeApproveOrRejectCreateEvent: EventEmitter<boolean> =
    new EventEmitter();
  approveOrRejectEmployeePromotionTransferCreateForm: FormGroup;
  employeeAddressCreateForm: FormGroup;
  approveOrRejectEmployeePromtionTransfer: ApproveOrRejectEmployeePromotionTransfer;
  submitted = false;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  id: any;
  type: number;
  constructor(
    private dialogRef: MatDialogRef<ApproveOrRejectEmployeePromotionTransferComponent>,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private employeePromotionTransferService: EmployeePromotionTransferService,
    @Inject(MAT_DIALOG_DATA) data
  ) {
    this.type = data.type;
    this.id = data.id;
  }

  ngOnInit(): void {
    this.buildForm();
  }
  buildForm(): void {
    this.approveOrRejectEmployeePromotionTransferCreateForm =
      this.formBuilder.group({
        employeePromotionTransferId: [this.id, [Validators.required]],
        notes: ['', [Validators.maxLength(250)]],
      });
  }
  onSubmit(): void {
    this.submitted = true;
    if (this.approveOrRejectEmployeePromotionTransferCreateForm.invalid) {
      return;
    } else if (this.type === ApproveOrDeclineEnums.Approve) {
      this.approveEmployeePromotionTransfer();
    } else {
      this.rejectEmployeePromotionTransfer();
    }
  }
  approveEmployeePromotionTransfer(): void {
    this.approveOrRejectEmployeePromtionTransfer =
      new ApproveOrRejectEmployeePromotionTransfer(
        this.approveOrRejectEmployeePromotionTransferCreateForm.value
      );
    this.loading = true;
    this.employeePromotionTransferService
      .approveEmployeePromtionTransfer(
        this.approveOrRejectEmployeePromtionTransfer
      )
      .subscribe(
        (res) => {
          this.loading = false;
          if ((res as any).status === false) {
            this.isFormValid = false;
            this.errorMessages = (res as any).message;
          } else {
            this.isFormValid = true;
            this.onEmployeeApproveOrRejectCreateEvent.emit(true);
            this.alertService.success("Approve successful");
            this.close();
          }
        },
        () => {
          this.loading = false;
        }
      );
  }
  rejectEmployeePromotionTransfer(): void {
    this.approveOrRejectEmployeePromtionTransfer =
      new ApproveOrRejectEmployeePromotionTransfer(
        this.approveOrRejectEmployeePromotionTransferCreateForm.value
      );
    this.loading = true;
    this.employeePromotionTransferService
      .rejectEmployeePromtionTransfer(
        this.approveOrRejectEmployeePromtionTransfer
      )
      .subscribe(
        (res) => {
          this.loading = false;
          if ((res as any).status === false) {
            this.isFormValid = false;
            this.errorMessages = (res as any).message;
          } else {
            this.isFormValid = true;
            this.onEmployeeApproveOrRejectCreateEvent.emit(true);
            this.alertService.success("Reject successful");
            this.close();
          }
        },
        () => {
          this.loading = false;
        }
      );
  }
  close(): void {
    this.dialogRef.close();
  }
  get f() {
    return this.approveOrRejectEmployeePromotionTransferCreateForm.controls;
  }
}
