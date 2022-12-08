import { DatePipe } from "@angular/common";
import { TextAttribute } from "@angular/compiler/src/render3/r3_ast";
import { Component, EventEmitter, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from "@angular/material/dialog";
import { EmployeeCard, Guid } from "src/app/shared/models";
import { EmployeeCardService } from "src/app/shared/services";
import { AlertService } from "src/app/shared/services/alert.service";
@Component({
  selector: "app-create-employee-card",
  templateUrl: "./create-employee-card.component.html",
  styleUrls: ["./create-employee-card.component.css"],
})
export class CreateEmployeeCardComponent implements OnInit {
  onEmployeeCardCreateEvent: EventEmitter<number> = new EventEmitter();
  onEmployeeCardEditEvent: EventEmitter<number> = new EventEmitter();
  submitted: boolean = false;
  employeeCardCreateForm: FormGroup;
  employeeCard: EmployeeCard;
  isFormValid: boolean;
  loading: boolean = false;
  errorMessage: any;
  employeeId: any;
  isEditMode: boolean = false;
  companyId: any = localStorage.getItem("companyId");
  isCardRevoked: boolean = false;
  isPaidChecked: boolean = false;
  employeeCardId: any;
  constructor(
    private alertService: AlertService,
    private dialog: MatDialog,
    private employeeCardService: EmployeeCardService,
    private formBuilder: FormBuilder,
    private datePipe: DatePipe,
    private dialogRef: MatDialogRef<CreateEmployeeCardComponent>,
    @Inject(MAT_DIALOG_DATA) data
  ) {
    this.employeeCard = new EmployeeCard();
    if (isNaN(data)) {
      this.employeeCard = new EmployeeCard(data);
      this.employeeId = this.employeeCard.employeeId;
      this.isPaidChecked = this.employeeCard.isPaid === true ? true : false;
      this.isCardRevoked =
        this.employeeCard.cardRevoked === true ? true : false;
    }
    if (this.employeeCard.id) {
      this.isEditMode = true;

      this.employeeCardId = this.employeeCard.id;
    }

    this.employeeCard.companyId = this.companyId;
  }
  ngOnInit(): void {
    this.buildForm();
  }
  onSubmit(): void {
    this.submitted = true;

    if (this.employeeCardCreateForm.invalid) return;
    if (
      this.employeeCard.id === null ||
      this.employeeCard.id === undefined ||
      this.employeeCard.id === Guid.empty
    ) {
      this.createEmployeeeCard();
    } else this.editEmployeeCard();
  }
  buildForm() {
    this.employeeCardCreateForm = this.formBuilder.group({
      companyId: [this.employeeCard.companyId, [Validators.required]],
      employeeId: [this.employeeCard.employeeId, [Validators.required]],
      cardMaskingValue: [
        this.employeeCard.cardMaskingValue,
        [Validators.required, Validators.maxLength(150)],
      ],
      issueDate: [this.employeeCard.issueDate],
      chargeAmount: [this.employeeCard.chargeAmount],
      isPaid: [this.employeeCard.isPaid],
      paymentDate: [this.employeeCard.paymentDate],
      cardRevoked: [this.employeeCard.cardRevoked],
      cardRevokedDate: [this.employeeCard.cardRevokedDate],
    });
  }
  createEmployeeeCard(): void {
    this.loading = true;
    this.employeeCard = new EmployeeCard(this.employeeCardCreateForm.value);

    this.employeeCard.issueDate =
      this.employeeCard.issueDate !== null
        ? this.datePipe.transform(
            new Date(this.employeeCard.issueDate),
            "yyyy-MM-dd"
          )
        : null;
    this.employeeCard.paymentDate = this.datePipe.transform(
      new Date(this.employeeCard.paymentDate),
      "yyyy-MM-dd"
    );
    this.employeeCard.cardRevokedDate = this.datePipe.transform(
      new Date(this.employeeCard.cardRevokedDate),
      "yyyy-MM-dd"
    );
    this.employeeCard.cardRevokedDate =
      this.isCardRevoked === false ? null : this.employeeCard.cardRevokedDate;
    this.employeeCard.paymentDate =
      this.isPaidChecked === false ? null : this.employeeCard.paymentDate;
    this.employeeCardService.createEmployeeCard(this.employeeCard).subscribe(
      (result) => {
        this.onEmployeeCardCreateEvent.emit((result as any).id);
        if ((result as any).status === true) {
          this.loading = false;
          this.isFormValid = true;
          this.alertService.success("Employee card  id created successfully");
          this.close();
        } else {
          this.isFormValid = false;
          this.errorMessage = (result as any).message;
        }
        this.loading = false;
      },
      () => {
        this.loading = false;
      }
    );
  }
  editEmployeeCard(): void {
    let newData = new EmployeeCard(this.employeeCardCreateForm.value);
    this.employeeCard = Object.assign({}, newData);
    this.employeeCard.id = this.employeeCardId;

    this.employeeCard.paymentDate = this.datePipe.transform(
      new Date(newData.paymentDate),
      "yyyy-MM-dd"
    );
    this.employeeCard.issueDate =
      this.employeeCard.issueDate !== null
        ? this.datePipe.transform(
            new Date(this.employeeCard.issueDate),
            "yyyy-MM-dd"
          )
        : null;
    this.employeeCard.cardRevokedDate = this.datePipe.transform(
      new Date(newData.cardRevokedDate),
      "yyyy-MM-dd"
    );
    this.employeeCard.cardRevokedDate =
      this.isCardRevoked === false ? null : this.employeeCard.cardRevokedDate;
    this.employeeCard.paymentDate =
      this.isPaidChecked === false ? null : this.employeeCard.paymentDate;
    if (this.employeeCard !== null) {
      this.loading = true;
      this.employeeCardService.editEmployeeCard(this.employeeCard).subscribe(
        (result) => {
          this.onEmployeeCardEditEvent.emit((result as any).id);
          if ((result as any).status === true) {
            this.isFormValid = true;
            this.alertService.success("Employee card  id edited successfully");
            this.loading = false;
            this.close();
          } else {
            this.isFormValid = false;
            this.errorMessage = (result as any).message;
          }
          this.loading = false;
        },
        () => {
          this.loading = false;
        }
      );
    }
  }
  close(): void {
    this.dialogRef.close();
  }
  paidChange(data: string): void {
    if (data === "true") {
      this.isPaidChecked = true;
      this.employeeCardCreateForm.controls["paymentDate"].setValidators([
        Validators.required,
      ]);
      this.employeeCardCreateForm.controls[
        "paymentDate"
      ].updateValueAndValidity();
    } else {
      this.isPaidChecked = false;

      this.employeeCardCreateForm.controls["paymentDate"].setErrors(null);
      this.employeeCardCreateForm.controls["paymentDate"].clearValidators();
      this.employeeCardCreateForm.controls[
        "paymentDate"
      ].updateValueAndValidity();
    }
  }
  cardRevokedChange(data: string): void {
    if (data === "true") {
      this.isCardRevoked = true;
      this.employeeCardCreateForm.controls["cardRevokedDate"].setValidators([
        Validators.required,
      ]);
      this.employeeCardCreateForm.controls[
        "cardRevokedDate"
      ].updateValueAndValidity();
    } else {
      this.isCardRevoked = false;

      this.employeeCardCreateForm.controls["cardRevokedDate"].setErrors(null);
      this.employeeCardCreateForm.controls["cardRevokedDate"].clearValidators();
      this.employeeCardCreateForm.controls[
        "cardRevokedDate"
      ].updateValueAndValidity();
    }
  }
  get f() {
    return this.employeeCardCreateForm.controls;
  }
}
