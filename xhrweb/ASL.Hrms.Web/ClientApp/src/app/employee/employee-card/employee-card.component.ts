import { Component, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AuthService } from 'src/app/auth/services/auth.service';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { EmployeeCard } from 'src/app/shared/models';
import { EmployeeCardService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { CreateEmployeeCardComponent } from './create-employee-card/create-employee-card.component';
@Component({
  selector: 'app-employee-card',
  templateUrl: './employee-card.component.html',
  styleUrls: ['./employee-card.component.css']
})
export class EmployeeCardComponent implements OnInit {
  @Input() employeeId: any;
  @Input() employeeFullName: any;
  loading: boolean;
  employeeCards: any = [];
  isAdmin: boolean=false;
  isHRManager:boolean;
  constructor(
    private alertService: AlertService,
    private dialog: MatDialog,
    private employeeCardService: EmployeeCardService,
    private authService: AuthService
  ) {
    this.isAdmin = this.authService.isAdmin;
    this.isHRManager = this.authService.isHRManager;

   }
  ngOnInit(): void {
    this.getAllCards();
  }
  getAllCards(): void {
    this.loading = true;
    this.employeeCardService.getEmployeeCardByEmployee(this.employeeId).subscribe(result => {
    this.employeeCards = result;
      this.loading = false;
    }, () => {
      this.loading = false;
    })
  }
  onDeleteEmployeeCard(employeeCard: EmployeeCard): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to delete the employee id Card ?";
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteEmployeeCard(employeeCard);
      }
    });
  }
  deleteEmployeeCard(employeeCard: EmployeeCard) {
    this.employeeCardService.deleteEmployeeCard(employeeCard).subscribe((data) => {
      this.alertService.success('Employee id card deleted successfully');
      this.getAllCards();
    },
      (error) => {
        this.alertService.error(error);
        console.log(error);
      });
  }
  createEmployeeCard(): void {
    const createEmployeeCardDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createEmployeeCardDialogConfig.disableClose = true;
    createEmployeeCardDialogConfig.autoFocus = true;
    createEmployeeCardDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createEmployeeCardDialogConfig.width = `${dialogWidth}%`;
    var employeeCard = new EmployeeCard();
    employeeCard.employeeId = this.employeeId;
    createEmployeeCardDialogConfig.data = employeeCard;
     const createemployeeCardDialog = this.dialog.open(CreateEmployeeCardComponent, createEmployeeCardDialogConfig);
    const successfullCreate = createemployeeCardDialog.componentInstance.onEmployeeCardCreateEvent.subscribe((data) => {
    this.getAllCards();
    });
    createemployeeCardDialog.afterClosed().subscribe(() => {
    });
  }
  editEmployeeCard(employeeCard: EmployeeCard): void {
    const createEmployeeCardDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createEmployeeCardDialogConfig.disableClose = true;
    createEmployeeCardDialogConfig.autoFocus = true;
    createEmployeeCardDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createEmployeeCardDialogConfig.width = `${dialogWidth}%`;
    var employeeCard = employeeCard;
    createEmployeeCardDialogConfig.data = employeeCard;
     const createemployeeCardDialog = this.dialog.open(CreateEmployeeCardComponent, createEmployeeCardDialogConfig);
    const successfullCreate = createemployeeCardDialog.componentInstance.onEmployeeCardEditEvent.subscribe((data) => {
    this.getAllCards();
    });
    createemployeeCardDialog.afterClosed().subscribe(() => {
    });
  }
}
