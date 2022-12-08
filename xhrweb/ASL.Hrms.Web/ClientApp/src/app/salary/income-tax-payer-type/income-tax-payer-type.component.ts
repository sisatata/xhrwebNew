import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { IncomeTaxPayerType } from 'src/app/shared/models';
import { CompanyService, IncomeTaxPayerTypeService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { CreateIncomeTaxPayerTypeComponent } from './create-income-tax-payer-type/create-income-tax-payer-type.component';
@Component({
  selector: 'app-income-tax-payer-type',
  templateUrl: './income-tax-payer-type.component.html',
  styleUrls: ['./income-tax-payer-type.component.css']
})
export class IncomeTaxPayerTypeComponent extends BaseAuthorizedComponent implements OnInit {
  incomeTaxPayerTypeFilterFormGroup: FormGroup;
  companies: any;
  companyId: any;
  submitted: boolean;
  loading: boolean;
  incomeTaxPayerTypes: IncomeTaxPayerType[];
  constructor(
    private companyService: CompanyService,
    private injector: Injector,
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private incomeTaxPayerTypeService: IncomeTaxPayerTypeService
  ) {
    super(injector)
  }
  ngOnInit(): void {
    this.getAllCompanies();
    this.buildForm();
    this.getAllIncomeTaxPayerTypes();
  }
  getAllCompanies():void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    },()=>{
      this.loading = false;
    })
  }
  get f() { return this.incomeTaxPayerTypeFilterFormGroup.controls; }
  buildForm() {
    this.incomeTaxPayerTypeFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
    });
  }
  createIncomeTaxPayerType():void {
    const createIncomeTaxPayerTypeDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createIncomeTaxPayerTypeDialogConfig.disableClose = true;
    createIncomeTaxPayerTypeDialogConfig.autoFocus = true;
    createIncomeTaxPayerTypeDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createIncomeTaxPayerTypeDialogConfig.width = `${dialogWidth}%`;
    var incomeTaxPayerType = new IncomeTaxPayerType();
    incomeTaxPayerType.companyId = this.companyId;
    createIncomeTaxPayerTypeDialogConfig.data = incomeTaxPayerType;
    const createIncomeTaxPayerTypeDialog = this.dialog.open(CreateIncomeTaxPayerTypeComponent, createIncomeTaxPayerTypeDialogConfig);
    const successfullCreate = createIncomeTaxPayerTypeDialog.componentInstance.onIncomeTaxPayerTypeCreateEvent.subscribe((data) => {
      this.getAllIncomeTaxPayerTypes();
    });
    createIncomeTaxPayerTypeDialog.afterClosed().subscribe(() => {
    });
  }
  editIncomeTaxPayerType(incomeTaxPayerType: IncomeTaxPayerType): void {
    const editIncomeTaxPayerTypeDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editIncomeTaxPayerTypeDialogConfig.disableClose = true;
    editIncomeTaxPayerTypeDialogConfig.autoFocus = true;
    editIncomeTaxPayerTypeDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editIncomeTaxPayerTypeDialogConfig.width = `${dialogWidth}%`;
    incomeTaxPayerType.companyId = this.companyId;
    editIncomeTaxPayerTypeDialogConfig.data = incomeTaxPayerType;
    const createIncomeTaxPayerTypeDialog = this.dialog.open(CreateIncomeTaxPayerTypeComponent, editIncomeTaxPayerTypeDialogConfig);
    const successfullCreate = createIncomeTaxPayerTypeDialog.componentInstance.onIncomeTaxPayerTypeEditEvent.subscribe((data) => {
      this.getAllIncomeTaxPayerTypes();
    });
    createIncomeTaxPayerTypeDialog.afterClosed().subscribe(() => {
    });
  }
  getAllIncomeTaxPayerTypes(): void {
    this.loading = true;
    this.incomeTaxPayerTypeService.getAllIncomeTaxPayerType(this.companyId).subscribe(result => {
      this.incomeTaxPayerTypes = result;
      this.loading = false;
      this.totalItems = result.length;
      this.generateTotalItemsText();
    },()=>{
      this.loading = false
    })
  }
  onDeleteIncomeTaxPayerType(incomeTaxPayerType: IncomeTaxPayerType): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data = "Are you sure to delete the income tax payer type " + incomeTaxPayerType.payerTypeName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result !== undefined) {
        this.deleteIncomeTaxPayerType(incomeTaxPayerType);
      }
    });
  }
  deleteIncomeTaxPayerType(incomeTaxPayerType: IncomeTaxPayerType): void {
    this.incomeTaxPayerTypeService.deleteIncomeTaxPayerType(incomeTaxPayerType).subscribe(result => {
      this.getAllIncomeTaxPayerTypes();
      this.alertService.success('Income tax payer type sucessfully deleted');
    }, () => { })
  }
}
