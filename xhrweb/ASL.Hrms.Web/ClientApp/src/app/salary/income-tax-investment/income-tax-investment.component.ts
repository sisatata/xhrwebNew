import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { IncomeTaxInvestment } from 'src/app/shared/models';
import { CompanyService, IncomeTaxInvestmentService, IncomeTaxParameterService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { CreateIncomeTaxInvestmentComponent } from './create-income-tax-investment/create-income-tax-investment.component';
@Component({
  selector: 'app-income-tax-investment',
  templateUrl: './income-tax-investment.component.html',
  styleUrls: ['./income-tax-investment.component.css']
})
export class IncomeTaxInvestmentComponent extends BaseAuthorizedComponent implements OnInit {
  incomeTaxInvestmentFilterFormGroup: FormGroup;
  incomeTaxInvestments: IncomeTaxInvestment[] = [];
  companies: any;
  loading: boolean;
  submitted: boolean;
  constructor(
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private injector: Injector,
    private incomeTaxInvestmentService: IncomeTaxInvestmentService,
    private companyService: CompanyService,
  ) {
    super(injector)
  }
  ngOnInit(): void {
    this.buildForm();
    this.getAllIncomeTaxInvestments();
    this.getAllCompanies();
  }
  get f() { return this.incomeTaxInvestmentFilterFormGroup.controls; }
  buildForm() {
    this.incomeTaxInvestmentFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
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
  getAllIncomeTaxInvestments(): void {
    this.loading = true;
    this.incomeTaxInvestmentService.getAllIncomeTaxInvestment(this.companyId).subscribe(result => {
      this.incomeTaxInvestments = result;
      this.loading = false;
    }, () => {
      this.loading = false;
    })
  }
  createIncomeTaxInvestment(): void {
    const createIncomeTaxInvestmentDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createIncomeTaxInvestmentDialogConfig.disableClose = true;
    createIncomeTaxInvestmentDialogConfig.autoFocus = true;
    createIncomeTaxInvestmentDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createIncomeTaxInvestmentDialogConfig.width =  `${dialogWidth}%`;
    var incomeTaxInvestment = new IncomeTaxInvestment();
    incomeTaxInvestment.companyId = this.companyId;
    createIncomeTaxInvestmentDialogConfig.data = incomeTaxInvestment;
    const createIncomeTaxParameterDialog = this.dialog.open(CreateIncomeTaxInvestmentComponent, createIncomeTaxInvestmentDialogConfig);
    const successfullCreate = createIncomeTaxParameterDialog.componentInstance.onIncomeTaxInvestmentCreateEvent.subscribe((data) => {
      this.getAllIncomeTaxInvestments();
    });
    createIncomeTaxParameterDialog.afterClosed().subscribe(() => {
    });
  }
  editIncomeTaxInvestment(incomeTaxInvestment: IncomeTaxInvestment): void {
    const editIncomeTaxInvestmentDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editIncomeTaxInvestmentDialogConfig.disableClose = true;
    editIncomeTaxInvestmentDialogConfig.autoFocus = true;
    editIncomeTaxInvestmentDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editIncomeTaxInvestmentDialogConfig.width = `${dialogWidth}%`;
    incomeTaxInvestment.companyId = this.companyId;
    editIncomeTaxInvestmentDialogConfig.data = incomeTaxInvestment;
    const createIncomeTaxParameterDialog = this.dialog.open(CreateIncomeTaxInvestmentComponent, editIncomeTaxInvestmentDialogConfig);
    const successfullCreate = createIncomeTaxParameterDialog.componentInstance.onIncomeTaxInvestmentEditEvent.subscribe((data) => {
      this.getAllIncomeTaxInvestments();
    });
    createIncomeTaxParameterDialog.afterClosed().subscribe(() => {
    });
  }
  onDeleteIncomeTaxInvestment(incometTaxInvestment: IncomeTaxInvestment): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data = "Are you sure to delete the income tax investment ";
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result !== undefined) {
        this.deleteIncomeTaxInvestment(incometTaxInvestment);
      }
    });
  }
  deleteIncomeTaxInvestment(incometTaxInvestment: IncomeTaxInvestment): void {
    this.incomeTaxInvestmentService.deleteIncomeTaxInvestment(incometTaxInvestment).subscribe(result => {
      this.alertService.success('Income tax investment successfully deleted');
      this.getAllIncomeTaxInvestments();
    }, () => { })
  }
}
