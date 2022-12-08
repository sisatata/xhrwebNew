import { Component, OnInit, Injector } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { FinancialYear, Holiday } from '../../shared/models';
import { HolidayService, BranchService, CompanyService, FinancialYearService } from '../../shared/services';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { CreateHolidayComponent } from './create-holiday/create-holiday.component';
import { AlertService } from 'src/app/shared/services/alert.service';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-holiday',
  templateUrl: './holiday.component.html',
  styleUrls: ['./holiday.component.css']
})

export class HolidayComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('holiday-list-section') blockUI: NgBlockUI;
  holidays: Holiday[] = [];
  holidayId: any;
  branchId: any;
  companies: any;
  branches: any;
  holidayFilterFormGroup: FormGroup;
  submitted: boolean;
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;
  financialYears: FinancialYear[];
  financialYearId: any;
  financialYear: string;

  constructor(private holidayService: HolidayService,
    private branchService: BranchService, private formBuilder: FormBuilder,
    private financialYearService: FinancialYearService,
    private companyService: CompanyService,
    private alertService: AlertService,
    private datePipe: DatePipe,
    private dialog: MatDialog, private injector: Injector) {
    super(injector);
  }

  ngOnInit() {
    this.buildForm();
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
    this.getAllFinancialYearByCompanyId();
  }


  buildForm() {
    this.holidayFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      branchId: [this.branchId, Validators.required],
      financialYearId: [this.financialYearId, Validators.required],

    });
  }
  getAllFinancialYearByCompanyId() {
    this.financialYearService.getAllFinancialYearByCompanyId(this.companyId).subscribe(result => {
      this.financialYears = result;
      const runnigyear = result.find(x => x.isRunningYear === true);
      this.financialYear = runnigyear ? runnigyear.financialYearName : null;
      this.financialYearId = runnigyear ? runnigyear.id : null;
      this.buildFormAndGetData();
     
      this.getAllHolidayByCompanyId();
      
      
    })
  }
  buildFormAndGetData():void{
    this.buildForm();
    //this.getMonthCycles();
  }
  get f() { return this.holidayFilterFormGroup.controls; }


  onChangeCompany(companyId: any) {
    this.companyId = companyId;

    if (companyId) {
      //this.getAllHolidayByCompanyId();
    }
  }

  onChangeBranch(branchId: any) {
    this.branchId = branchId;
    if (branchId) {
     // this.getAllHolidayByCompanyId();
    }
  }

  getHolidays() {
    this.submitted = true;
    if (this.holidayFilterFormGroup.invalid) {
      return;
    }
    this.branchId = this.f.branchId.value;

    this.getAllHolidayByCompanyId();
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }


  getAllHolidayByCompanyId() {
    this.blockUI.start();
    this.loading = true;
    this.holidayService.getHolidayByFinancialYear(this.companyId, this.financialYear).subscribe(result => {
      this.holidays = result;     
     this.loading = false
      this.totalItems = this.holidays.length;
      this.generateTotalItemsText();
      
      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
      this.loading = false

    })
  }

  createHoliday() {
    const createholidayDialogConfig = new MatDialogConfig();
    createholidayDialogConfig.disableClose = true;
    createholidayDialogConfig.autoFocus = true;
    createholidayDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createholidayDialogConfig.width =  `${dialogWidth}%`;
    var holiday = new Holiday();
    holiday.companyId = this.companyId;
    createholidayDialogConfig.data = holiday;
    const createHolidayDialog = this.dialog.open(CreateHolidayComponent, createholidayDialogConfig)
    const successfullCreate = createHolidayDialog.componentInstance.onHolidayCreateEvent.subscribe((data) => {
      this.getAllHolidayByCompanyId();
    });
    createHolidayDialog.afterClosed().subscribe(() => {
    });

  }

  editHoliday(holiday: Holiday) {
    const editDialogConfig = new MatDialogConfig();
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = holiday
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(CreateHolidayComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onHolidayEditEvent.subscribe((data) => {
      this.getAllHolidayByCompanyId();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }

  onDeleteHoliday(holiday: Holiday): void {
    const confirmationDialogConfig = new MatDialogConfig();
    holiday.holidayDate = this.datePipe.transform(new Date( holiday.holidayDate), 'yyyy-MM-dd');
    confirmationDialogConfig.data = "Are you sure to delete the holiday " + holiday.holidayDate;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteHoliday(holiday);
      }
    });
  }

  deleteHoliday(holiday: Holiday) {
    this.holidayService.deleteHoliday(holiday).subscribe((data) => {
      this.alertService.success("Holiday deleted successfully");
      this.getAllHolidayByCompanyId();
    },
      (error) => {
        console.log(error);
      });
  }
  onChangeFinancialYear(data:any):void{
    let financialYear = this.financialYears.find(x=>x.id === data.value);
    this.financialYear = financialYear.financialYearName;
    this.getAllHolidayByCompanyId();
  }
}

