import { Component, Injector, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { BaseAuthorizedComponent } from "src/app/shared/components/base-authorized/base-authorized.component";
import { ConfirmationDialogComponent } from "src/app/shared/components/confirmation-dialog/confirmation-dialog.component";
import { LeaveGroup } from "src/app/shared/models";
import { CompanyService, LeaveGroupService } from "src/app/shared/services";
import { AlertService } from "src/app/shared/services/alert.service";
import { CreateLeaveGroupComponent } from "./create-leave-group/create-leave-group.component";
@Component({
  selector: "app-leave-group",
  templateUrl: "./leave-group.component.html",
  styleUrls: ["./leave-group.component.css"],
})
export class LeaveGroupComponent
  extends BaseAuthorizedComponent
  implements OnInit
{
  companies: any = [];
  leaveGroupFilterFormGroup: FormGroup;
  submitted = false;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  leaveGroups: LeaveGroup[] = [];
  constructor(
    private companyService: CompanyService,
    private leaveGroupService: LeaveGroupService,
    private alertService: AlertService,
    private formBuilder: FormBuilder,
    private dialog: MatDialog,
    private injector: Injector
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.getAllCompanies();
    this.onChangeCompany(this.companyId);
    this.buildForm();
  }
  buildForm(): void {
    this.leaveGroupFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
    });
  }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe(
      (result: any) => {
        this.companies = result;
      },
      () => {}
    );
  }
  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllLeaveGroups();
    }
  }
  getLeaves() {
    this.getAllLeaveGroups();
  }
  getAllLeaveGroups(): void {
    this.loading = true;
    this.leaveGroupService.getAllLeaveGroups(this.companyId).subscribe(
      (res) => {
        this.loading = false;
        this.leaveGroups = res;
        this.totalItems = res.length;
        this.generateTotalItemsText();
      },
      () => {
        this.loading = false;
        this.leaveGroups = [];
      }
    );
  }
  createLeaveGroup() {
    const createLeaveGroupDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createLeaveGroupDialogConfig.disableClose = true;
    createLeaveGroupDialogConfig.autoFocus = true;
    createLeaveGroupDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90 : 50;
    createLeaveGroupDialogConfig.width = `${dialogWidth}%`;
    var leaveGroup = new LeaveGroup();
    leaveGroup.companyId = this.companyId;
    createLeaveGroupDialogConfig.data = leaveGroup;
    const createleaveGroupDialog = this.dialog.open(
      CreateLeaveGroupComponent,
      createLeaveGroupDialogConfig
    );
    const successfullCreate =
      createleaveGroupDialog.componentInstance.onLeaveGroupCreateEvent.subscribe(
        (data) => {
          this.getAllLeaveGroups();
        }
      );
    createleaveGroupDialog.afterClosed().subscribe(() => {});
  }
  editLeaveGroup(leaveGroup: LeaveGroup) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = leaveGroup;
    editDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90 : 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const editDialog = this.dialog.open(
      CreateLeaveGroupComponent,
      editDialogConfig
    );
    const successFullEdit =
      editDialog.componentInstance.onLeaveGroupEditEvent.subscribe((data) => {
        this.getAllLeaveGroups();
      });
    editDialog.afterClosed().subscribe(() => {});
  }
  onDeleteLeaveGroup(leaveGroup: LeaveGroup): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to delete the leave Group ?";
    confirmationDialogConfig.panelClass = "xHR-dialog";
    const confirmationDialog = this.dialog.open(
      ConfirmationDialogComponent,
      confirmationDialogConfig
    );
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteLeaveGroup(leaveGroup);
      }
    });
  }
  deleteLeaveGroup(leaveGroup: LeaveGroup) {
    this.leaveGroupService.deleteLeaveGroup(leaveGroup).subscribe(
      (data) => {
        this.alertService.success("Leave Group deleted successfully");
        this.getAllLeaveGroups();
      },
      (error) => {
        this.alertService.error("Internal server error happen");
        console.log(error);
      }
    );
  }
  get f() {
    return this.leaveGroupFilterFormGroup.controls;
  }
}
