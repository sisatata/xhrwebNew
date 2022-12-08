import { Component, OnInit, Input } from "@angular/core";
import { BlockUI, NgBlockUI } from "ng-block-ui";
import { EmployeeEmailService } from "../../shared/services";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { ConfirmationDialogComponent } from "../../shared/components/confirmation-dialog/confirmation-dialog.component";
import { EmployeeEmail } from "../../shared/models";
import { CreateEmployeeEmailComponent } from "./create-employee-email/create-employee-email.component";
import { AlertService } from "../../shared/services/alert.service";
import { AuthService } from "src/app/auth/services/auth.service";
@Component({
  selector: "app-employee-email",
  templateUrl: "./employee-email.component.html",
  styleUrls: ["./employee-email.component.css"],
})
export class EmployeeEmailComponent implements OnInit {
  @Input() employeeId: any;
  @Input() employeeFullName: any;
  @BlockUI("employee-email-section") blockUI: NgBlockUI;
  employeeEmails: any = [];
  isHRManager:boolean;
  isAdmin:boolean;
  constructor(
    private employeeEmailService: EmployeeEmailService,
    private alertService: AlertService,
    private authService: AuthService,
    private dialog: MatDialog
  ) {}
  ngOnInit() {
    this.isHRManager = this.authService.isHRManager;
    this.isAdmin = this.authService.isAdmin;
    this.getAllAddresses();
  }
  getAllAddresses() {
    this.blockUI.start();
    this.employeeEmailService
      .getEmployeeEmailByEmployeeId(this.employeeId)
      .subscribe(
        (result) => {
          this.employeeEmails = result;
          this.blockUI.stop();
        },
        (error) => {
          this.blockUI.stop();
        }
      );
  }
  createEmployeeEmail() {
    const createEmployeeEmailDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createEmployeeEmailDialogConfig.disableClose = true;
    createEmployeeEmailDialogConfig.autoFocus = true;
    createEmployeeEmailDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90 : 50;
    createEmployeeEmailDialogConfig.width = `${dialogWidth}%`;
    var employeeEmail = new EmployeeEmail();
    employeeEmail.employeeId = this.employeeId;
    createEmployeeEmailDialogConfig.data = employeeEmail;
    const createemployeeEmailDialog = this.dialog.open(
      CreateEmployeeEmailComponent,
      createEmployeeEmailDialogConfig
    );
    const successfullCreate =
      createemployeeEmailDialog.componentInstance.onEmployeeEmailCreateEvent.subscribe(
        (data) => {
          this.getAllAddresses();
        }
      );
    createemployeeEmailDialog.afterClosed().subscribe(() => {});
  }
  editEmployeeEmail(employeeEmail: EmployeeEmail) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = employeeEmail;
    const dialogWidth = window.screen.width <= 576 ? 90 : 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const editDialog = this.dialog.open(
      CreateEmployeeEmailComponent,
      editDialogConfig
    );
    const successFullEdit =
      editDialog.componentInstance.onEmployeeEmailEditEvent.subscribe(
        (data) => {
          this.getAllAddresses();
        }
      );
    editDialog.afterClosed().subscribe(() => {});
  }
  onDeleteEmployeeEmail(employeeEmail: EmployeeEmail): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data =
      "Are you sure to delete the employee Email ?";
    confirmationDialogConfig.panelClass = "xHR-dialog";
    const confirmationDialog = this.dialog.open(
      ConfirmationDialogComponent,
      confirmationDialogConfig
    );
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteEmployeeEmail(employeeEmail);
      }
    });
  }
  deleteEmployeeEmail(employeeEmail: EmployeeEmail) {
    this.employeeEmailService.deleteEmployeeEmail(employeeEmail).subscribe(
      (data) => {
        this.alertService.success("Employee Email deleted successfully");
        this.getAllAddresses();
      },
      (error) => {
        this.alertService.error(error);
        console.log(error);
      }
    );
  }
}
