import { Component, OnInit, Input } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { EmployeeAddressService } from '../../shared/services';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../../shared/components/confirmation-dialog/confirmation-dialog.component';
import { EmployeeAddress } from '../../shared/models';
import { CreateEmployeeAddressComponent } from './create-employee-address/create-employee-address.component';
import { AlertService } from 'src/app/shared/services/alert.service';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/auth/services/auth.service';
@Component({
  selector: 'app-employee-address',
  templateUrl: './employee-address.component.html',
  styleUrls: ['./employee-address.component.css']
})
export class EmployeeAddressComponent implements OnInit {
  @Input() employeeId: any;
  @Input() employeeFullName: any;
  @BlockUI('employee-address-section') blockUI: NgBlockUI;
  employeeAddresses: any = [];
  allDsitricts: any = [];
  loading: boolean;
  isHRManager:boolean;
  isAdmin:boolean;
  constructor(private employeeAddressService: EmployeeAddressService,
    private alertService: AlertService,
    private authService: AuthService,
    private dialog: MatDialog) { }
  ngOnInit() {
     this.isHRManager = this.authService.isHRManager;
     this.isAdmin = this.authService.isAdmin;
    this.getAllAddresses();
  }
  getAllAddresses() {
    this.blockUI.start();
    this.employeeAddressService.getEmployeeAddressByEmployeeId(this.employeeId).subscribe(result => {
      this.employeeAddresses = result;
      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
    });
  }
  // async getDistrictNameById() {
  //   this.employeeAddressService.getAllDistricts().subscribe(result => {
  //     this.allDsitricts = result;
  //     this.employeeAddresses.forEach(async (element, index) => {
  //       let districtIndex = this.allDsitricts.findIndex((data: { id: any; }) => data.id === element.districtId);
  //       if (districtIndex !== -1) {
  //         this.employeeAddresses[index].districtName = this.allDsitricts[districtIndex].name;
  //         if (element.thanaId) {
  //           await this.getThanaNameById(this.employeeAddresses[index].thanaId, element.districtId, index);
  //         }
  //       }
  //     });
  //   })
  // }
  // async getThanaNameById(thanaId: string, districtId: string, index: any) {
  //   await this.employeeAddressService.getAllThanas(districtId).toPromise().then(result => {
  //     let allThanas = result;
  //     let thanaIndex = allThanas.findIndex((data: { id: string; }) => data.id === thanaId);
  //     this.employeeAddresses[index].thanaName = allThanas[thanaIndex].upazilaName;
  //   });
  // }

  createEmployeeAddress() {
    const createEmployeeAddressDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createEmployeeAddressDialogConfig.disableClose = true;
    createEmployeeAddressDialogConfig.autoFocus = true;
    createEmployeeAddressDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createEmployeeAddressDialogConfig.width = `${dialogWidth}%`;
    var employeeAddress = new EmployeeAddress();
    employeeAddress.employeeId = this.employeeId;
    createEmployeeAddressDialogConfig.data = employeeAddress;
    const createemployeeAddressDialog = this.dialog.open(CreateEmployeeAddressComponent, createEmployeeAddressDialogConfig);
    const successfullCreate = createemployeeAddressDialog.componentInstance.onEmployeeAddressCreateEvent.subscribe((data) => {
      this.getAllAddresses();
    });
    createemployeeAddressDialog.afterClosed().subscribe(() => {
    });
  }
  editEmployeeAddress(employeeAddress: EmployeeAddress) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = employeeAddress
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const editDialog = this.dialog.open(CreateEmployeeAddressComponent, editDialogConfig);
    
    const successFullEdit = editDialog.componentInstance.onEmployeeAddressEditEvent.subscribe((data) => {
     
      this.getAllAddresses();
    });
    editDialog.afterClosed().subscribe(() => {
    });
  }
  onDeleteEmployeeAddress(employeeAddress: EmployeeAddress): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to delete the Employee Address ?";
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteEmployeeAddress(employeeAddress);
      }
    });
  }
  deleteEmployeeAddress(employeeAddress: EmployeeAddress) {
    this.employeeAddressService.deleteEmployeeAddress(employeeAddress).subscribe((data) => {
      this.alertService.success('Employee Address deleted successfully');
      this.getAllAddresses();
    },
      (error) => {
        this.alertService.error('Internal server error happen');
        console.log(error);
      });
  }
}
