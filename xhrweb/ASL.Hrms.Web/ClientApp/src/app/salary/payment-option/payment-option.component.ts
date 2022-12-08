import { Component, OnInit, Injector } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { PaymentOption } from 'src/app/shared/models/payroll/payment-option';
import { CreatePaymentOptionComponent } from './create-paymentoption/create-payment-option.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CompanyService, PaymentOptionService } from 'src/app/shared/services';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';


@Component({
  selector: 'app-payment-option',
  templateUrl: './payment-option.component.html',
  styleUrls: ['./payment-option.component.css']
})
export class PaymentOptionComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('paymentoption-list-section') blockUI: NgBlockUI
  paymentoptions: PaymentOption[];
  paymentoptionId: any;
  paymentoptionFilterFormGroup: FormGroup
  companies: any;
  submitted: boolean;

  constructor(private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private paymentoptionService: PaymentOptionService,
    private companyService: CompanyService,
    private injector: Injector) {
    super(injector);
  }

  ngOnInit() {
    this.buildForm();
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
  }

  buildForm() {
    this.paymentoptionFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required]

    });
  }

  get f() { return this.paymentoptionFilterFormGroup.controls; }

  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllPaymentOptionByCompanyId();
    }
  }
  getPaymentOption() {
    this.submitted = true;
    if (this.paymentoptionFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.getAllPaymentOptionByCompanyId();
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }

  createPaymentOption() {
    const createPaymentOptionDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createPaymentOptionDialogConfig.disableClose = true;
    createPaymentOptionDialogConfig.autoFocus = true;
    createPaymentOptionDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createPaymentOptionDialogConfig.width =  `${dialogWidth}%`;
    var paymentoption = new PaymentOption();
    //paymentoption. = this.companyId;
    createPaymentOptionDialogConfig.data = paymentoption;
    const createPaymentOptionDialog = this.dialog.open(CreatePaymentOptionComponent, createPaymentOptionDialogConfig);
    const successfullCreate = createPaymentOptionDialog.componentInstance.onPaymentOptionCreateEvent.subscribe((data) => {
      this.getAllPaymentOptionByCompanyId();
    });
    createPaymentOptionDialog.afterClosed().subscribe(() => {
    });
  }
  editPaymentOption(paymentoption: PaymentOption) {

    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = paymentoption
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width =  `${dialogWidth}%`;

    const outletEditDialog = this.dialog.open(CreatePaymentOptionComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onPaymentOptionEditEvent.subscribe((data) => {
      this.getAllPaymentOptionByCompanyId();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }

  onDeletePaymentOption(paymentoption: PaymentOption): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = 'Are you sure to delete the PaymentOption ' + paymentoption.optionName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);

    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deletePaymentOption(paymentoption);
      }
    });
  }
  deletePaymentOption(paymentoption: PaymentOption) {
    this.paymentoptionService.deletePaymentOption(paymentoption).subscribe((data) => {
      this.getAllPaymentOptionByCompanyId();
    },
      (error) => {
        console.log(error);
      });
  }

  getAllPaymentOptionById(brnchId) {
    this.paymentoptionService.getPaymentOptionById(brnchId).subscribe(result => {
      this.paymentoptions = result;
    })
  }

  getAllPaymentOptionByCompanyId() {
    this.blockUI.start();
    this.paymentoptionService.getAllPaymentOptionByCompanyId(this.companyId).subscribe(result => {
      this.paymentoptions = result;

      this.totalItems = this.paymentoptions.length;
      this.generateTotalItemsText();

      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
    })

  }
}

