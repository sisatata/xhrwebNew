import { Component, OnInit, Injector } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { MonthCycle } from 'src/app/shared/models';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CompanyService, FinancialYearService, MonthCycleService } from 'src/app/shared/services';
import { CreateMonthCycleComponent } from './create-month-cycle/create-month-cycle.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';

@Component({
  selector: 'app-month-cycle',
  templateUrl: './month-cycle.component.html',
  styleUrls: ['./month-cycle.component.css']
})
export class MonthCycleComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('monthCycle-list-section') blockUI: NgBlockUI
  monthCycles: MonthCycle[]=[];
  monthCycleId: any;
  monthCycleFilterFormGroup: FormGroup
  companies: any;
  financialYears: any;
  financialYearId: any;
  submitted: boolean;
loading: boolean = false;
isFormValid:boolean;
  constructor(
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private financialYearService: FinancialYearService,
    private monthCycleService: MonthCycleService,
    private injector: Injector
  ) { super(injector) }

  ngOnInit() {
    this.buildForm();
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
    this.getAllFinancialYearByCompanyId();
  }
  

  buildForm() {
    this.monthCycleFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      financialYearId: [this.financialYearId, Validators.required],
    });
  }
  get f() { return this.monthCycleFilterFormGroup.controls; }

  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllFinancialYearByCompanyId();
    }
    else if(this.financialYearId && companyId){
      this.getAllMonthCycleByCompanyIdAndFinancialYear();

    }
  }
  onChangeFinancialYear(financialYearId: any) {
    this.financialYearId = financialYearId;
    if (financialYearId) {
      this.getAllMonthCycleByCompanyIdAndFinancialYear();
    }
  }

  getAllFinancialYearByCompanyId() {
    this.financialYearService.getAllFinancialYearByCompanyId(this.companyId).subscribe(result => {
      this.financialYears = result;
      const runnigyear = result.find(x => x.isRunningYear === true);
      this.buildFormAndGetData();
      this.financialYearId = runnigyear ? runnigyear.id : null;
     

    })
  }
  buildFormAndGetData():void{
    this.buildForm();
    this.getMonthCycles();
  }
  getMonthCycles() {
    this.submitted = true;
    if (this.monthCycleFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.getAllMonthCycleByCompanyIdAndFinancialYear();
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }


  createMonthCycle() {
    const createMonthCycleDialogConfig = new MatDialogConfig();
    createMonthCycleDialogConfig.disableClose = true;
    createMonthCycleDialogConfig.autoFocus = true;
    createMonthCycleDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createMonthCycleDialogConfig.width = `${dialogWidth}%`;
    var monthCycle = new MonthCycle();
    monthCycle.companyId = this.companyId;
    createMonthCycleDialogConfig.data = monthCycle;
    const createMonthCycleDialog = this.dialog.open(CreateMonthCycleComponent, createMonthCycleDialogConfig);
    const successfullCreate = createMonthCycleDialog.componentInstance.onMonthCycleCreateEvent.subscribe((data) => {
      this.getAllMonthCycleByCompanyIdAndFinancialYear();
    });
    createMonthCycleDialog.afterClosed().subscribe(() => {
    });
  }

  editMonthCycle(monthCycle: MonthCycle) {
    const editDialogConfig = new MatDialogConfig();
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = monthCycle
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(CreateMonthCycleComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onMonthCycleEditEvent.subscribe((data) => {
      this.getAllMonthCycleByCompanyIdAndFinancialYear();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }

  onDeleteMonthCycle(monthCycle: MonthCycle): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data = "Are you sure to delete the Month Cycle " + monthCycle.monthCycleName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteMonthCycle(monthCycle);
      }
    });
  }

  deleteMonthCycle(monthCycle: MonthCycle) {
    this.monthCycleService.deleteMonthCycle(monthCycle).subscribe((data) => {
      this.getAllMonthCycleByCompanyIdAndFinancialYear();
    }, (error) => {
      console.log(error);
    });

  }

  getAllMonthCycleByCompanyIdAndFinancialYear() {
    this.blockUI.start();
    this.loading = true;
    this.monthCycleService.getMonthCycleByCompanyAndFinancialYear(this.companyId, this.financialYearId).subscribe(result => {
      this.monthCycles = result;
      this.totalItems = this.monthCycles.length;
      this.generateTotalItemsText();
      this.blockUI.stop();
      this.loading = false;

    }, error => {
      this.blockUI.stop();
      this.loading = false;
    })

  }


}
