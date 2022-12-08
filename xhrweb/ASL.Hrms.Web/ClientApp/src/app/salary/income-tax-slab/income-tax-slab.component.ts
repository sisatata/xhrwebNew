import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { IncomeTaxSlab } from 'src/app/shared/models';
import { CompanyService, IncomeTaxPayerTypeService, IncomeTaxSlabService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { CreateIncomeTaxSlabComponent } from './create-income-tax-slab/create-income-tax-slab.component';
@Component({
  selector: 'app-income-tax-slab',
  templateUrl: './income-tax-slab.component.html',
  styleUrls: ['./income-tax-slab.component.css']
})
export class IncomeTaxSlabComponent extends BaseAuthorizedComponent implements OnInit {
  incomeTaxSlabFilterFormGroup: FormGroup;
  companies: any;
  companyId: any;
  submitted: boolean;
  incomeTaxSlabs: any;
  loading: boolean;
  incometTaxPayerType: any;
  incomeTaxPayerTypes: any[] = [];
  allData: any;
  constructor(
    private companyService: CompanyService,
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private injector: Injector,
    private incomeTaxPayerTypeService: IncomeTaxPayerTypeService,
    private incomeTaxSlabService: IncomeTaxSlabService
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.getAllCompanies();
    this.buildForm();
    this.getAllIncomeTaxPayerType();
    this.getAllIncomeTaxSlabs();  }
  get f() { return this.incomeTaxSlabFilterFormGroup.controls; }
  getAllCompanies(): void {
    this.loading = true;
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    }, () => {
      this.loading = false;
    })
  }
  buildForm() {
    this.incomeTaxSlabFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      incometTaxPayerType: [this.incometTaxPayerType, Validators.required],
    });
  }
  getAllIncomeTaxSlabs(): void {
    this.loading = true;
    this.incometTaxPayerType = null;
    this.incomeTaxSlabService.getAllIncomeTaxSlab(this.companyId).subscribe(result => {
      this.incomeTaxSlabs = result;
      this.totalItems = result.length;
      this.generateTotalItemsText();
      this.allData = result;
      this.loading = false;
    }, () => {
      this.loading = false;
    })
  }
  getAllIncomeTaxPayerType(): void {
    this.loading = true;
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
      this.incomeTaxSlabs = this.allData;
    }
    else {
      let incomeTaxSlabdatas = this.allData.filter(x => x.taxPayerTypeCode === data.value);
      this.incomeTaxSlabs = incomeTaxSlabdatas;
    }
    this.totalItems = this.incomeTaxSlabs.length;
    this.generateTotalItemsText();
  }
  createIncomeTaxSlab(): void {
    const createIncomeTaxSlabDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createIncomeTaxSlabDialogConfig.disableClose = true;
    createIncomeTaxSlabDialogConfig.autoFocus = true;
    createIncomeTaxSlabDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createIncomeTaxSlabDialogConfig.width =  `${dialogWidth}%`;
    var incomeTaxSlab = new IncomeTaxSlab();
    incomeTaxSlab.companyId = this.companyId;
    createIncomeTaxSlabDialogConfig.data = incomeTaxSlab;
    const createIncomeTaxSlabDialog = this.dialog.open(CreateIncomeTaxSlabComponent, createIncomeTaxSlabDialogConfig);
    const successfullCreate = createIncomeTaxSlabDialog.componentInstance.onIncomeTaxSlabCreateEvent.subscribe((data) => {
      this.getAllIncomeTaxSlabs();
    });
    createIncomeTaxSlabDialog.afterClosed().subscribe(() => {
    });
  }
  editIncomeTaxSlab(incomeTaxSlab: IncomeTaxSlab): void {
    const editIncomeTaxSlabDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editIncomeTaxSlabDialogConfig.disableClose = true;
    editIncomeTaxSlabDialogConfig.autoFocus = true;
    editIncomeTaxSlabDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editIncomeTaxSlabDialogConfig.width =  `${dialogWidth}%`;
    incomeTaxSlab.companyId = this.companyId;
    editIncomeTaxSlabDialogConfig.data = incomeTaxSlab;
    const editIncomeTaxSlabDialog = this.dialog.open(CreateIncomeTaxSlabComponent, editIncomeTaxSlabDialogConfig);
    const successfulledit = editIncomeTaxSlabDialog.componentInstance.onIncomeTaxSlabEditEvent.subscribe((data) => {
      this.getAllIncomeTaxSlabs();
    });
    editIncomeTaxSlabDialog.afterClosed().subscribe(() => {
    });
  }
  onDeleteIncomeTaxSlab(incometTaxSlab: IncomeTaxSlab): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data = "Are you sure to delete the income tax slab " + incometTaxSlab.slabName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result !== undefined) {
        this.deleteIncomeTaxSlab(incometTaxSlab);
      }
    });
  }
  deleteIncomeTaxSlab(incometTaxSlab: IncomeTaxSlab): void {
    this.incomeTaxSlabService.deleteIncomeTaxSlab(incometTaxSlab).subscribe(result => {
      this.getAllIncomeTaxSlabs();
      this.alertService.success('Income tax slab successfully deleted');
    }, () => {
    })
  }
}
