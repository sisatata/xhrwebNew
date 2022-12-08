import { Component, OnInit, Input, Injector } from '@angular/core';
import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { OfficeNoticeService, CompanyService } from '../../shared/services';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CreateOfficeNoticeComponent } from './create-office-notice/create-office-notice.component';
import { OfficeNotice } from '../../shared/models';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ConfirmationDialogComponent } from '../../shared/components/confirmation-dialog/confirmation-dialog.component';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';

@Component({
  selector: 'app-office-notice',
  templateUrl: './office-notice.component.html',
  styleUrls: ['./office-notice.component.css']
})
export class OfficeNoticeComponent extends BaseAuthorizedComponent implements OnInit {
  @Input() officeNoticeId: any;
  @BlockUI('office-notice-section') blockUI: NgBlockUI;
  OfficeNoticeFilterFormGroup: FormGroup;
  companies: any;
  officeNotices: any = [];
  submitted: boolean;

  constructor(private officeNoticeService: OfficeNoticeService, private dialog: MatDialog,
    private companyService: CompanyService,
    private formBuilder: FormBuilder,
    private injector: Injector) {super(injector); }

  ngOnInit() {
    this.buildForm();
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
    this.getAllOfficeNotices();
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }

  buildForm() {
    this.OfficeNoticeFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required]
    });
  }
  get f() { return this.OfficeNoticeFilterFormGroup.controls; }

  getAllOfficeNotices() {
    if (this.companyId !== '') {
      this.blockUI.start();
      this.officeNoticeService.getOfficeNoticeListByCompany(this.companyId).subscribe(
        result => {
          this.officeNotices = result;
          this.blockUI.stop();
        });
    }
  }
  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllOfficeNotices();
    }
  }

  getOfficeNotices() {
    this.submitted = true;
    if (this.OfficeNoticeFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.getAllOfficeNotices();

  }
  createOfficeNotice() {
    const createOfficeNoticeDialogConfig = new MatDialogConfig();

    // Setting different dialog configurations
    createOfficeNoticeDialogConfig.disableClose = true;
    createOfficeNoticeDialogConfig.autoFocus = true;
    createOfficeNoticeDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createOfficeNoticeDialogConfig.width = `${dialogWidth}%`;

    var officeNotice = new OfficeNotice();
    //officeNotice.employeeId = this.officeNoticeId;
    createOfficeNoticeDialogConfig.data = officeNotice;
    const createOfficeNoticeDialog = this.dialog.open(CreateOfficeNoticeComponent, createOfficeNoticeDialogConfig);
    const successfullCreate = createOfficeNoticeDialog.componentInstance.onOfficeNoticeCreateEvent.subscribe((data) => {
      this.getAllOfficeNotices();
    });
    createOfficeNoticeDialog.afterClosed().subscribe(() => {
    });
  }

  editOfficeNotice(officeNotice: OfficeNotice) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = officeNotice
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width = `${dialogWidth}%`;

    const editDialog = this.dialog.open(CreateOfficeNoticeComponent, editDialogConfig);
    const successFullEdit = editDialog.componentInstance.onOfficeNoticeEditEvent.subscribe((data) => {
      this.getAllOfficeNotices();
    });
    editDialog.afterClosed().subscribe(() => {
    });
  }

  onDeleteOfficeNotice(officeNotice: OfficeNotice): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to delete the OfficeNotice ?";
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);

    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteOfficeNotice(officeNotice);
      }
    });
  }
  deleteOfficeNotice(officeNotice: OfficeNotice) {
    this.officeNoticeService.deleteOfficeNotice(officeNotice).subscribe((data) => {
      //this.alertService.success('EmployeePhone deleted successfully');
      this.getAllOfficeNotices();
    },
      (error) => {
        //this.alertService.error('Internal server error happen');
        console.log(error);
      });
  }
}
