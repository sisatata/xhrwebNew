import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { IncomeTaxParameter } from 'src/app/shared/models';
import { CompanyService, IncomeTaxParameterService, IncomeTaxPayerTypeService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { CreateIncomeTaxParameterComponent } from './create-income-tax-parameter/create-income-tax-parameter.component';
@Component({
  selector: 'app-income-tax-parameter',
  templateUrl: './income-tax-parameter.component.html',
  styleUrls: ['./income-tax-parameter.component.css']
})
export class IncomeTaxParameterComponent extends BaseAuthorizedComponent implements OnInit {
  incomeTaxParameterFilterFormGroup: FormGroup;
  companies: any;
  companyId: any;
  submitted: boolean;
  incomeTaxSlabs: any;
  loading: boolean;
  incometTaxPayerType: any;
  incomeTaxPayerTypes: any[] = [];
  allData: any;
  payerTypeCode: any;
  incomeTaxParameters: IncomeTaxParameter[] = [];
  constructor(
    private companyService: CompanyService,
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private injector: Injector,
    private incomeTaxPayerTypeService: IncomeTaxPayerTypeService,
    private incomeTaxParameterService: IncomeTaxParameterService,
  ) {
    super(injector)
  }
  ngOnInit(): void {
    this.getAllCompanies();
    this.buildForm();
    this.getAllIncomeTaxPayerType();
    this.getAllIncomeTaxParameters();
  }
  get f() { return this.incomeTaxParameterFilterFormGroup.controls; }
  buildForm() {
    this.incomeTaxParameterFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      payerTypeCode: [this.payerTypeCode, Validators.required],
    });
  }
  getAllCompanies(): void {
    this.loading = true;
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    }, () => {
      this.loading = false;
    })
  }
  getAllIncomeTaxPayerType(): void {
    this.loading = true;
    this.incometTaxPayerType = null;
    this.incomeTaxPayerTypeService.getAllIncomeTaxPayerType(this.companyId).subscribe(result => {
      this.incomeTaxPayerTypes = result;
      // for getting all data
      this.incomeTaxPayerTypes.unshift({ id: 1, payerTypeName: 'All', payerTypeCode: 'All' });
    }, () => {
      this.loading = false;
    })
  }
  onChangeIncomeTaxPayerType(data: any): void {
    if (data.value === 'All') {
      this.incomeTaxParameters = this.allData;
    }
    else {
      let incomeTaxParameterdatas = this.allData.filter(x => x.payerTypeCode === data.value);
      this.incomeTaxParameters = incomeTaxParameterdatas;
    }
    this.totalItems = this.incomeTaxParameters.length;
    this.generateTotalItemsText();
  }
  getAllIncomeTaxParameters(): void {
    this.loading = true;
    this.payerTypeCode = null;
    this.incomeTaxParameterService.getAllIncomeTaxParameter(this.companyId).subscribe(result => {
      this.incomeTaxParameters = result;
      this.allData = result;
      this.loading = false;
      this.totalItems = result.length;
      this.generateTotalItemsText();
      //this.incomeTaxPayerTypes.unshift({ id: 1, payerTypeName: 'All', payerTypeCode: 'All' });
    }, () => {
      this.loading = false;
    })
  }
  createIncomeTaxParameter(): void {
    const createIncomeTaxParameterDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createIncomeTaxParameterDialogConfig.disableClose = true;
    createIncomeTaxParameterDialogConfig.autoFocus = true;
    createIncomeTaxParameterDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createIncomeTaxParameterDialogConfig.width =  `${dialogWidth}%`;
    var incomeTaxParameter = new IncomeTaxParameter();
    incomeTaxParameter.companyId = this.companyId;
    createIncomeTaxParameterDialogConfig.data = incomeTaxParameter;
    const createIncomeTaxParameterDialog = this.dialog.open(CreateIncomeTaxParameterComponent, createIncomeTaxParameterDialogConfig);
    const successfullCreate = createIncomeTaxParameterDialog.componentInstance.onIncomeTaxParameterCreateEvent.subscribe((data) => {
      this.getAllIncomeTaxParameters();
    });
    createIncomeTaxParameterDialog.afterClosed().subscribe(() => {
    });
  }
  editIncomeTaxParameter(incomeTaxParameter: IncomeTaxParameter): void {
    const editIncomeTaxParameterDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editIncomeTaxParameterDialogConfig.disableClose = true;
    editIncomeTaxParameterDialogConfig.autoFocus = true;
    editIncomeTaxParameterDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editIncomeTaxParameterDialogConfig.width =  `${dialogWidth}%`;
    //var incomeTaxParameter = new IncomeTaxParameter();
    incomeTaxParameter.companyId = this.companyId;
    editIncomeTaxParameterDialogConfig.data = incomeTaxParameter;
    const editIncomeTaxParameterDialog = this.dialog.open(CreateIncomeTaxParameterComponent, editIncomeTaxParameterDialogConfig);
    const successfullEdit = editIncomeTaxParameterDialog.componentInstance.onIncomeTaxParameterEditEvent.subscribe((data) => {
      this.getAllIncomeTaxParameters();
    });
    editIncomeTaxParameterDialog.afterClosed().subscribe(() => {
    });
  }
  onDeleteIncomeTaxParameter(incomeTaxParameter: IncomeTaxParameter): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data = "Are you sure to delete the income tax parameter " + incomeTaxParameter.parameterName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result !== undefined) {
        this.deleteIncomeTaxParameter(incomeTaxParameter);
      }
    });
  }
  deleteIncomeTaxParameter(incomeTaxParameter: IncomeTaxParameter): void {
    this.incomeTaxParameterService.deleteIncomeTaxParameter(incomeTaxParameter).subscribe(result => {
      this.getAllIncomeTaxParameters();
      this.alertService.success('Income tax parameter successfully deleted');
    }, () => {
    })
  }
}
