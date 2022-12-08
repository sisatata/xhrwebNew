import { Component, OnInit, Injector } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { FinancialYear } from 'src/app/shared/models/company/financial-year';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { FinancialYearService, CompanyService } from 'src/app/shared/services';
import { CreateFinancialyearComponent } from './create-financialyear/create-financialyear.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
@Component({
  selector: 'app-financial-year',
  templateUrl: './financial-year.component.html',
  styleUrls: ['./financial-year.component.css']
})

export class FinancialYearComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('financialYear-list-section') blockUI: NgBlockUI
  financialYears: FinancialYear[]=[];
  financialYearId: any;
  financialYearFilterFormGroup: FormGroup
  companies: any;
  submitted: boolean;
  isFormValid: boolean;
  errorMessages: any ;
  isSearched: boolean = false;
loading: boolean = false;
  constructor(
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private financialYearService: FinancialYearService,
    private companyService: CompanyService,
    private injector: Injector
  ) {
    super(injector);
  }

  ngOnInit() {
    this.buildForm();
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
  }

  buildForm() {
    this.financialYearFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required]

    });
  }
  get f() { return this.financialYearFilterFormGroup.controls; }

  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    this.emptyTable();
    if (companyId) {
      this.getAllFinancialYearByCompanyId();
     
    }
  }

  getFinancialYears() {
    this.submitted = true;
    if (this.financialYearFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.getAllFinancialYearByCompanyId();
  }

  getAllCompanies() {

    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
     
    })
  }


  createFinancialYear() {
    const createFinancialYearDialogConfig = new MatDialogConfig();
    createFinancialYearDialogConfig.disableClose = true;
    createFinancialYearDialogConfig.autoFocus = true;
    createFinancialYearDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createFinancialYearDialogConfig.width = `${dialogWidth}%`;
    // createFinancialYearDialogConfig.minWidth = "300px";
    var financialyear = new FinancialYear();
    financialyear.companyId = this.companyId;
    createFinancialYearDialogConfig.data = financialyear;
    const createfinancialyearDialog = this.dialog.open(CreateFinancialyearComponent, createFinancialYearDialogConfig);
    const successfullCreate = createfinancialyearDialog.componentInstance.onFinancialYearCreateEvent.subscribe((data) => {
      this.getAllFinancialYearByCompanyId();
    });
    createfinancialyearDialog.afterClosed().subscribe(() => {
    });
  }

   editFinancialYear(financialYear: FinancialYear) {
    const editDialogConfig = new MatDialogConfig();
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = financialYear
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width =  `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(CreateFinancialyearComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onFinancialYearEditEvent.subscribe((data) => {
      this.getAllFinancialYearByCompanyId();
     
     });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }

  onDeleteFinancialYear(financialYear: FinancialYear): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data = "Are you sure to delete the Financial Year " + financialYear.financialYearName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteFinancialYear(financialYear);
      }
    });
  }

  deleteFinancialYear(financialYear: FinancialYear) {
    this.financialYearService.deleteFinancialYear(financialYear).subscribe((data) => {
      this.getAllFinancialYearByCompanyId();
    }, (error) => {
      console.log(error);
    });

  }

  getAllFinancialYearByCompanyId() {
    this.blockUI.start();
    this.loading = true;
    this.financialYearService.getAllFinancialYearByCompanyId(this.companyId).subscribe(result => {
      this.financialYears = result;  // no status
      this.totalItems = this.financialYears.length;
      this.generateTotalItemsText();
      
      this.blockUI.stop();
      this.loading = false;
    }, error => {
      this.blockUI.stop();
      this.loading = false;

    })

  }
  emptyTable():void{
    this.financialYears = [];
    this.loading = false;
    this.isSearched = false;
    this.totalItems =0;
  }
}
