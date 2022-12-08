import { Component, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { CompanyAddressService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { CompanyAddress } from '../../shared/models';
import { CreateCompanyAddressComponent } from './create-company-address/create-company-address.component';
@Component({
  selector: 'app-company-address',
  templateUrl: './company-address.component.html',
  styleUrls: ['./company-address.component.css']
})
export class CompanyAddressComponent implements OnInit {
  @Input() companyId: any;
  @Input() companyName: any;
  companyAddresses: any = [];
  allDsitricts: any = [];
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean;
  constructor(private alertService: AlertService,
    private companyAddressService: CompanyAddressService,
    private dialog: MatDialog) { }
  ngOnInit(): void {
    this.getAllAddresses();
  }
  getAllAddresses() {
    this.loading = true;
    this.companyAddressService.getAllCompanyAddressByCompanyId(this.companyId).subscribe(result => {
      this.companyAddresses = result;
      this.loading = false;
    }, error => {
      this.loading = false;

    });
  }
  createCompanyAddress() {
    const createcompanyAddressDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createcompanyAddressDialogConfig.disableClose = true;
    createcompanyAddressDialogConfig.autoFocus = true;
    createcompanyAddressDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createcompanyAddressDialogConfig.width = `${dialogWidth}%`;
    var companyAddress = new CompanyAddress();
    companyAddress.companyId = this.companyId;
    createcompanyAddressDialogConfig.data = companyAddress;
    const createcompanyAddressDialog = this.dialog.open(CreateCompanyAddressComponent, createcompanyAddressDialogConfig);
    const successfullCreate = createcompanyAddressDialog.componentInstance.onCompanyAddressCreateEvent.subscribe((data) => {
      this.getAllAddresses();
    });
    createcompanyAddressDialog.afterClosed().subscribe(() => {
    });
  }
  editCompanyAddress(companyAddress: CompanyAddress) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = companyAddress
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width =  `${dialogWidth}%`;
    const editDialog = this.dialog.open(CreateCompanyAddressComponent, editDialogConfig);
    const successFullEdit = editDialog.componentInstance.onCompanyAddressEditEvent.subscribe((data) => {
      this.getAllAddresses();
    });
    editDialog.afterClosed().subscribe(() => {
    });
  }
  onDeleteCompanyAddress(companyAddress: CompanyAddress): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to delete the company Address ?";
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteCompanyAddress(companyAddress);
      }
    });
  }
  deleteCompanyAddress(companyAddress: CompanyAddress) {

    this.companyAddressService.deleteCompanyAddress(companyAddress).subscribe((data) => {

      if ((data as any).status === true) {
        this.isFormValid = true;
        this.alertService.success("Company Address deleted successfully");
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (data as any).message;
      }
      this.getAllAddresses();
    },
      (error) => {
        this.alertService.error('Internal server error happen');
        console.log(error);
      });
  }
}
