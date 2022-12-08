import { Component, OnInit, Input } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { EmployeeExperienceService } from '../../shared/services';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../../shared/components/confirmation-dialog/confirmation-dialog.component';
import { EmployeeExperience } from '../../shared/models';
import { CreateEmployeeExperienceComponent } from './create-employee-experience/create-employee-experience.component';
import { AlertService } from '../../shared/services/alert.service';
import { AuthService } from 'src/app/auth/services/auth.service';
@Component({
  selector: 'app-employee-experience',
  templateUrl: './employee-experience.component.html',
  styleUrls: ['./employee-experience.component.css']
})
export class EmployeeExperienceComponent implements OnInit {
  @Input() employeeId: any;
  @Input() employeeFullName: any;
  @BlockUI('employee-experience-section') blockUI: NgBlockUI;
  employeeExperiences: any = [];
  isFormValid: boolean;
  errorMessages: any ;
  isHRManager:boolean;
  isAdmin:boolean;
  constructor(private employeeExperienceService: EmployeeExperienceService,
    private alertService: AlertService,
    private authService: AuthService,
    private dialog: MatDialog) { 
      this.isHRManager = this.authService.isHRManager;
      this.isAdmin = this.authService.isAdmin;

    }
  ngOnInit() {
    this.getAllExperiencees();
  }
  getAllExperiencees() {
    this.blockUI.start();
    this.employeeExperienceService.getEmployeeExperienceByEmployeeId(this.employeeId).subscribe(result => {
      this.employeeExperiences = result;
     
      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
    });
  }
  createEmployeeExperience() {
    const createEmployeeExperienceDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createEmployeeExperienceDialogConfig.disableClose = true;
    createEmployeeExperienceDialogConfig.autoFocus = true;
    createEmployeeExperienceDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createEmployeeExperienceDialogConfig.width = `${dialogWidth}%`;
    var employeeExperience = new EmployeeExperience();
    employeeExperience.employeeId = this.employeeId;
    createEmployeeExperienceDialogConfig.data = employeeExperience;
    const createemployeeExperienceDialog = this.dialog.open(CreateEmployeeExperienceComponent, createEmployeeExperienceDialogConfig);
    const successfullCreate = createemployeeExperienceDialog.componentInstance.onEmployeeExperienceCreateEvent.subscribe((data) => {
      this.getAllExperiencees();
    });
    createemployeeExperienceDialog.afterClosed().subscribe(() => {
    });
  }
  editEmployeeExperience(employeeExperience: EmployeeExperience) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = employeeExperience;
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width =`${dialogWidth}%`;
    const editDialog = this.dialog.open(CreateEmployeeExperienceComponent, editDialogConfig);
    const successFullEdit = editDialog.componentInstance.onEmployeeExperienceEditEvent.subscribe((data) => {
      this.getAllExperiencees();
    });
    editDialog.afterClosed().subscribe(() => {
    });
  }
  onDeleteEmployeeExperience(employeeExperience: EmployeeExperience): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to delete the Employee Experience?";
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteEmployeeExperience(employeeExperience);
      }
    });
  }
  deleteEmployeeExperience(employeeExperience: EmployeeExperience) {
    this.employeeExperienceService.deleteEmployeeExperience(employeeExperience).subscribe((data) => {
      this.alertService.success('Employee Experience deleted successfully');
      this.getAllExperiencees();
    },
      (error) => {
        this.alertService.error('Internal server error happen');
        console.log(error);
      });
  }
}
