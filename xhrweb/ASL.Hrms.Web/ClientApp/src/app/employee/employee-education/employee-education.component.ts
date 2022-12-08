import { Component, OnInit, Input } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { EmployeeEducationService } from '../../shared/services';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../../shared/components/confirmation-dialog/confirmation-dialog.component';
import { EmployeeEducation } from '../../shared/models';
import { CreateEmployeeEducationComponent } from './create-employee-education/create-employee-education.component';
import { AlertService } from '../../shared/services/alert.service';
import { AuthService } from 'src/app/auth/services/auth.service';
@Component({
  selector: 'app-employee-education',
  templateUrl: './employee-education.component.html',
  styleUrls: ['./employee-education.component.css']
})
export class EmployeeEducationComponent implements OnInit {
  @Input() employeeId: any;
  @Input() employeeFullName: any;
  @BlockUI('employee-education-section') blockUI: NgBlockUI;
  employeeEducations: any = [];
  isHRManager:boolean;
  isAdmin:boolean;
  constructor(private employeeEducationService: EmployeeEducationService,
    private alertService: AlertService,
    private authService: AuthService,
    private dialog: MatDialog) {
      this.isHRManager = this.authService.isHRManager;
      this.isAdmin = this.authService.isAdmin;   
     }
  ngOnInit() {
    this.getAllEducations();
  }
  getAllEducations() {
    this.blockUI.start();
    this.employeeEducationService.getEmployeeEducationByEmployeeId(this.employeeId).subscribe(result => {
      this.employeeEducations = result;
      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
    });
  }

  createEmployeeEducation() {
    const createEmployeeEducationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createEmployeeEducationDialogConfig.disableClose = true;
    createEmployeeEducationDialogConfig.autoFocus = true;
    createEmployeeEducationDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createEmployeeEducationDialogConfig.width = `${dialogWidth}%`;
    var employeeEducation = new EmployeeEducation();
    employeeEducation.employeeId = this.employeeId;
    createEmployeeEducationDialogConfig.data = employeeEducation;
    const createemployeeEducationDialog = this.dialog.open(CreateEmployeeEducationComponent, createEmployeeEducationDialogConfig);
    const successfullCreate = createemployeeEducationDialog.componentInstance.onEmployeeEducationCreateEvent.subscribe((data) => {
      this.getAllEducations();
    });
    createemployeeEducationDialog.afterClosed().subscribe(() => {
    });
  }
  editEmployeeEducation(employeeEducation: EmployeeEducation) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = employeeEducation
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width =  `${dialogWidth}%`;
    const editDialog = this.dialog.open(CreateEmployeeEducationComponent, editDialogConfig);
    const successFullEdit = editDialog.componentInstance.onEmployeeEducationEditEvent.subscribe((data) => {
      this.getAllEducations();
    });
    editDialog.afterClosed().subscribe(() => {
    });
  }
  onDeleteEmployeeEducation(employeeEducation: EmployeeEducation): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to delete the employee Education ?";
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteEmployeeEducation(employeeEducation);
      }
    });
  }
  deleteEmployeeEducation(employeeEducation: EmployeeEducation) {
    this.employeeEducationService.deleteEmployeeEducation(employeeEducation).subscribe((data) => {
      this.alertService.success('Employee Education deleted successfully');
      this.getAllEducations();
    },
      (error) => {
        this.alertService.error('Internal server error happen');
      });
  }
}
