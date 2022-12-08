import { Component, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CompanyPhone } from '../../shared/models';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { CreateCompanyPhoneComponent } from './create-company-phone/create-company-phone.component';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { CompanyPhoneService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-company-phone',
  templateUrl: './company-phone.component.html',
  styleUrls: ['./company-phone.component.css']
})
export class CompanyPhoneComponent implements OnInit {
  @Input() companyId: any;
  @Input() companyName: any;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean;
  @BlockUI('company-phone-section') blockUI: NgBlockUI;

  companyPhones: any = [];
  constructor(private dialog: MatDialog,
    private companyPhoneService: CompanyPhoneService,
    private alertService: AlertService,
  ) { }

  ngOnInit(): void {
    this.getCompanyPhones();
  }
  getCompanyPhones(): void {
    this.blockUI.start();
    this.loading = true;
    this.companyPhoneService.getAllCompanyPhoneByCompanyId(this.companyId).subscribe(result => {
      this.companyPhones = result; // not status provides
      this.blockUI.stop();
      this.loading = false;
    }, error => {
      this.blockUI.stop();
      this.loading = false;

    });
  }
  createCompanyPhone() {
    const createCompanyPhoneDialogConfig = new MatDialogConfig();

    // Setting different dialog configurations
    createCompanyPhoneDialogConfig.disableClose = true;
    createCompanyPhoneDialogConfig.autoFocus = true;
    createCompanyPhoneDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createCompanyPhoneDialogConfig.width = `${dialogWidth}%`;
    var companyPhone = new CompanyPhone();
    companyPhone.companyId = this.companyId;
    createCompanyPhoneDialogConfig.data = companyPhone;
    const createcompanyPhoneDialog = this.dialog.open(CreateCompanyPhoneComponent, createCompanyPhoneDialogConfig);
    const successfullCreate = createcompanyPhoneDialog.componentInstance.onCompanyPhoneCreateEvent.subscribe((data) => {
      this.getCompanyPhones();
    });
    createcompanyPhoneDialog.afterClosed().subscribe(() => {
    });
  }
  editCompanyPhone(companyPhone: CompanyPhone) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = companyPhone
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const editDialog = this.dialog.open(CreateCompanyPhoneComponent, editDialogConfig);
    const successFullEdit = editDialog.componentInstance.onCompanyPhoneEditEvent.subscribe((data) => {
      this.getCompanyPhones();
    });
    editDialog.afterClosed().subscribe(() => {
    });
  }

  onDeleteCompanyPhone(companyPhone: CompanyPhone): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to delete the Company Phone ?";
    confirmationDialogConfig.panelClass = 'xHR-dialog';

    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);

    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteCompanyPhone(companyPhone);
      }
    });
  }
  deleteCompanyPhone(companyPhone: CompanyPhone) {
    this.companyPhoneService.deleteCompanyPhone(companyPhone).subscribe((data) => {
      if ((data as any).status === true) {
        this.alertService.success('Company Phone deleted successfully');
        this.isFormValid = true;
        this.getCompanyPhones();

      }
      else {
        this.isFormValid = false;
        this.errorMessages = (data as any).message;
      }


    },
      (error) => {
        //this.alertService.error('Internal server error happen');
        console.log(error);
      });
  }


}
