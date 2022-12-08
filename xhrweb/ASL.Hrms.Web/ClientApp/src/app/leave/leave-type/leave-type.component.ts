import { Component, OnInit, Injector } from "@angular/core";
import { BlockUI, NgBlockUI } from "ng-block-ui";
import { LeaveType } from "src/app/shared/models";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { LeaveTypeService, CompanyService } from "src/app/shared/services";
import { CreateLeaveTypeComponent } from "./create-leave-type/create-leave-type.component";
import { ConfirmationDialogComponent } from "src/app/shared/components/confirmation-dialog/confirmation-dialog.component";
import { BaseAuthorizedComponent } from "src/app/shared/components/base-authorized/base-authorized.component";
@Component({
  selector: "app-leave-type",
  templateUrl: "./leave-type.component.html",
  styleUrls: ["./leave-type.component.css"],
})
export class LeaveTypeComponent
  extends BaseAuthorizedComponent
  implements OnInit
{
  @BlockUI("leaveType-list-section") blockUI: NgBlockUI;
  leaveTypes: LeaveType[] = [];
  leaveTypeId: any;
  leaveTypeFilterFormGroup: FormGroup;
  companies: any;
  submitted: boolean;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  constructor(
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private leaveTypeService: LeaveTypeService,
    private companyService: CompanyService,
    private injector: Injector
  ) {
    super(injector);
  }
  ngOnInit() {
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
    this.buildForm();
  }
  buildForm() {
    this.leaveTypeFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
    });
  }
  get f() {
    return this.leaveTypeFilterFormGroup.controls;
  }
  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllLeaveTypeByCompanyId();
    }
  }
  getLeaveType() {
    this.submitted = true;
    if (this.leaveTypeFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.getAllLeaveTypeByCompanyId();
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  createLeaveType() {
    const createLeaveTypeDialogConfig = new MatDialogConfig();
    createLeaveTypeDialogConfig.disableClose = true;
    createLeaveTypeDialogConfig.autoFocus = true;
    createLeaveTypeDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90 : 50;
    createLeaveTypeDialogConfig.width = `${dialogWidth}%`;
    var leaveType = new LeaveType();
    leaveType.companyId = this.companyId;
    createLeaveTypeDialogConfig.data = leaveType;
    const createLeaveTypeDialog = this.dialog.open(
      CreateLeaveTypeComponent,
      createLeaveTypeDialogConfig
    );
    const successfullCreate =
      createLeaveTypeDialog.componentInstance.onLeaveTypeCreateEvent.subscribe(
        (data) => {
          this.getAllLeaveTypeByCompanyId();
        }
      );
    createLeaveTypeDialog.afterClosed().subscribe(() => {});
  }
  editLeaveType(leaveType: LeaveType) {
    const editDialogConfig = new MatDialogConfig();
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = leaveType;
    editDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90 : 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(
      CreateLeaveTypeComponent,
      editDialogConfig
    );
    const successFullEdit =
      outletEditDialog.componentInstance.onLeaveTypeEditEvent.subscribe(
        (data) => {
          this.getAllLeaveTypeByCompanyId();
        }
      );
    outletEditDialog.afterClosed().subscribe(() => {});
  }
  onDeleteLeaveType(leaveType: LeaveType): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data =
      "Are you sure to delete the leave Type " + leaveType.leaveTypeName;
    confirmationDialogConfig.panelClass = "xHR-dialog";
    const confirmationDialog = this.dialog.open(
      ConfirmationDialogComponent,
      confirmationDialogConfig
    );
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteLeaveType(leaveType);
      }
    });
  }
  deleteLeaveType(leaveType: LeaveType) {
    this.leaveTypeService.deleteLeaveType(leaveType).subscribe(
      (data) => {
        this.getAllLeaveTypeByCompanyId();
      },
      (error) => {
        console.log(error);
      }
    );
  }
  getAllLeaveTypeById(leaveTypeId) {
    this.leaveTypeService.getLeaveTypeById(leaveTypeId).subscribe((result) => {
      this.leaveTypes = result;
    });
  }
  getAllLeaveTypeByCompanyId() {
    this.blockUI.start();
    this.loading = true;
    this.leaveTypeService.getAllLeaveTypeByCompanyId(this.companyId).subscribe(
      (result) => {
        this.leaveTypes = result;
        this.loading = false;
        this.totalItems = this.leaveTypes.length;
        this.generateTotalItemsText();
        this.blockUI.stop();
      },
      (error) => {
        this.blockUI.stop();
        this.loading = false;
      }
    );
  }
}
