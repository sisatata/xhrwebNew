import { Component, OnInit,Input } from '@angular/core';
import { CompanyService, BranchService } from 'src/app/shared/services';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { EmployeeManager } from '../../shared/models';
import { } from './create-employee-manager/create-employee-manager.component';
import { EmployeeManagerService } from '../../shared/services/employee/employee-manager.service';
import { CreateEmployeeManagerComponent } from './create-employee-manager/create-employee-manager.component'
import { AuthService } from '../../auth/services/auth.service';
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
  selector: 'app-employee-manager',
  templateUrl: './employee-manager.component.html',
  styleUrls: ['./employee-manager.component.css']
})
export class EmployeeManagerComponent implements OnInit {

  @Input() employeeId: any;
  @Input() employeeFullName: any;
  @BlockUI('employee-manager-section') blockUI: NgBlockUI;

  employeeManagers: any = [];
  canCreateManager: boolean = false;
  canEditManager: boolean = false;
  canDeleteManager: boolean = false;
    isAuthenticated: boolean;
    isHRManager:boolean;
    isAdmin:boolean;
  constructor( private alertService: AlertService, private employeeManagerService: EmployeeManagerService,
    private dialog: MatDialog, private authService: AuthService) { 

      this.isHRManager = this.authService.isHRManager;
      this.isAdmin = this.authService.isAdmin;
    }

  ngOnInit() {
    this.checkAuthentication();
    this.getAllManagers();
  }

  checkAuthentication() {
    this.isAuthenticated = this.authService.checkAuthenticated();
    var loggedInUserInfo = this.authService.getLoggedInUserInfo();

    var roles = loggedInUserInfo.userRoles;

    if (roles.includes("Administrators")) {
      
      this.canCreateManager = true;
      this.canEditManager = true;
      this.canDeleteManager = true;
    }
  }

  getAllManagers() {
    this.blockUI.start();
    this.employeeManagerService.getEmployeeManagerListByEmployeeId(this.employeeId).subscribe(result => {
      this.employeeManagers = result;
      this.blockUI.stop();
    });
  }

  createEmployeeManager() {
    const createEmployeeManagerDialogConfig = new MatDialogConfig();

    // Setting different dialog configurations
    createEmployeeManagerDialogConfig.disableClose = true;
    createEmployeeManagerDialogConfig.autoFocus = true;
    createEmployeeManagerDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createEmployeeManagerDialogConfig.width =  `${dialogWidth}%`;
    var employeeManager = new EmployeeManager();
    employeeManager.employeeId = this.employeeId;
    createEmployeeManagerDialogConfig.data = employeeManager;
    const createemployeeManagerDialog = this.dialog.open(CreateEmployeeManagerComponent, createEmployeeManagerDialogConfig);
    const successfullCreate = createemployeeManagerDialog.componentInstance.onEmployeeManagerCreateEvent.subscribe((data) => {
      this.getAllManagers();
    });
    createemployeeManagerDialog.afterClosed().subscribe(() => {
    });
  }

  editEmployeeManager(employeeManager: EmployeeManager) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = employeeManager
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const editDialog = this.dialog.open(CreateEmployeeManagerComponent, editDialogConfig);
    const successFullEdit = editDialog.componentInstance.onEmployeeManagerEditEvent.subscribe((data) => {
      this.getAllManagers();
    });
    editDialog.afterClosed().subscribe(() => {
    });
  }

  onDeleteEmployeeManager(employeeManager: EmployeeManager): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to delete the employee manager ?";
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);

    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteEmployeeManager(employeeManager);
      }
    });
  }
  deleteEmployeeManager(employeeManager: EmployeeManager) {
    this.employeeManagerService.deleteEmployeeManager(employeeManager).subscribe((data) => {
      this.alertService.success('Employee manager deleted successfully');
      this.getAllManagers();
    },
      (error) => {
        //this.alertService.error('Internal server error happen');
        console.log(error);
      });
  }
}
