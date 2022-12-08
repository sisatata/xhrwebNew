import { Component, OnInit, Input } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { EmployeePhoneService } from '../../shared/services';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../../shared/components/confirmation-dialog/confirmation-dialog.component';
import { EmployeePhone } from '../../shared/models';
import { CreateEmployeePhoneComponent } from './create-employee-phone/create-employee-phone.component';
import { AlertService } from 'src/app/shared/services/alert.service';
import { AuthService } from 'src/app/auth/services/auth.service';
@Component({
  selector: 'app-employee-phone',
  templateUrl: './employee-phone.component.html',
  styleUrls: ['./employee-phone.component.css']
})
export class EmployeePhoneComponent implements OnInit {
  @Input() employeeId: any;
  @Input() employeeFullName: any;
  @BlockUI('employee-phone-section') blockUI: NgBlockUI;
  employeePhones: any = [];
  isHRManager:boolean;
  isAdmin:boolean; 
  constructor( private alertService: AlertService, private employeePhoneService: EmployeePhoneService,
    private authService: AuthService,
    private dialog: MatDialog) { 

      this.isHRManager = this.authService.isHRManager;
     this.isAdmin = this.authService.isAdmin;
    }
  ngOnInit() {
    this.getAllAddresses();
  }
  getAllAddresses() {
    this.blockUI.start();
    this.employeePhoneService.getEmployeePhoneByEmployeeId(this.employeeId).subscribe(result => {
      this.employeePhones = result;
      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
    });
  }
  createEmployeePhone() {
    const createEmployeePhoneDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createEmployeePhoneDialogConfig.disableClose = true;
    createEmployeePhoneDialogConfig.autoFocus = true;
    createEmployeePhoneDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createEmployeePhoneDialogConfig.width = `${dialogWidth}%`;
    var employeePhone = new EmployeePhone();
    employeePhone.employeeId = this.employeeId;
    createEmployeePhoneDialogConfig.data = employeePhone;
    const createemployeePhoneDialog = this.dialog.open(CreateEmployeePhoneComponent, createEmployeePhoneDialogConfig);
    const successfullCreate = createemployeePhoneDialog.componentInstance.onEmployeePhoneCreateEvent.subscribe((data) => {
      this.getAllAddresses();
    });
    createemployeePhoneDialog.afterClosed().subscribe(() => {
    });
  }
  editEmployeePhone(employeePhone: EmployeePhone) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = employeePhone
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width=`${dialogWidth}%`;
    const editDialog = this.dialog.open(CreateEmployeePhoneComponent, editDialogConfig);
    const successFullEdit = editDialog.componentInstance.onEmployeePhoneEditEvent.subscribe((data) => {
      this.getAllAddresses();
    });
    editDialog.afterClosed().subscribe(() => {
    });
  }
  onDeleteEmployeePhone(employeePhone: EmployeePhone): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to delete the employee Phone ?";
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteEmployeePhone(employeePhone);
      }
    });
  }
  deleteEmployeePhone(employeePhone: EmployeePhone) {
    this.employeePhoneService.deleteEmployeePhone(employeePhone).subscribe((data) => {
      this.alertService.success('Employee Phone deleted successfully');
      this.getAllAddresses();
    },
      (error) => {
        //this.alertService.error('Internal server error happen');
        console.log(error);
      });
  }
}
