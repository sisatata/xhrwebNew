import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MonthCycle } from 'src/app/shared/models';
import { CompanyService, FinancialYearService, MonthCycleService } from 'src/app/shared/services';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AlertService } from '../../../shared/services/alert.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-create-month-cycle',
  templateUrl: './create-month-cycle.component.html',
  styleUrls: ['./create-month-cycle.component.css']
})
export class CreateMonthCycleComponent implements OnInit {

  onMonthCycleCreateEvent: EventEmitter<any> = new EventEmitter();
  onMonthCycleEditEvent: EventEmitter<any> = new EventEmitter();
  monthCycleCreateForm: FormGroup
  submitted = false;
  companyId: any;
  companies: any;
  financialYears: any;
  financialYearId: any;
  monthCycle: MonthCycle;
  monthCycleId: any;
  isEditMode = false;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  constructor(
    private companyService: CompanyService,
    private financialYearService: FinancialYearService,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateMonthCycleComponent>,
    private formBuilder: FormBuilder,
    private monthCycleService: MonthCycleService,
    private datePipe: DatePipe,
    @Inject(MAT_DIALOG_DATA) data
  ) {

    this.monthCycle = new MonthCycle();

    if (isNaN(data)) {
      this.monthCycle = new MonthCycle(data);
      this.companyId = this.monthCycle.companyId;
      this.financialYearId = this.monthCycle.financialYearId;
    } else {

    }
    if(this.monthCycle.id){
      this.isEditMode = true;
    }
    this.buildForm();
    this.getAllFinancialYearByCompanyId();
  }

  ngOnInit() {
    this.getAllCompanies();
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }


  onChangeCompany() {
    this.companyId = this.f.companyId.value;
    if (this.companyId) {
      this.getAllFinancialYearByCompanyId();
    }
  }

  onChangeFinancialYear() {
    this.financialYearId = this.f.financialYearId.value;
  }

  getAllFinancialYearByCompanyId() {
    this.financialYearService.getAllFinancialYearByCompanyId(this.companyId).subscribe(result => {
      this.financialYears = result;
    })
  }

  buildForm() {
    this.monthCycleCreateForm = this.formBuilder.group({
      companyId: [this.monthCycle.companyId],
      financialYearId: [this.monthCycle.financialYearId],
      monthCycleName: [this.monthCycle.monthCycleName, [Validators.required, Validators.maxLength(250)]],
      monthCycleLocalizedName: [this.monthCycle.monthCycleLocalizedName, [Validators.maxLength(150)]],
      monthStartDate: [this.monthCycle.monthStartDate, [Validators.required]],
      monthEndDate: [this.monthCycle.monthEndDate, [Validators.required]],
      totalWorkingDays: [this.monthCycle.totalWorkingDays, Validators.required],
      runningFlag: [this.monthCycle.runningFlag],
      sortOrder: [this.monthCycle.sortOrder, [Validators.min(1)]],
    });
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.monthCycleCreateForm.invalid) {
      return;
    }
    if (this.monthCycle.id === undefined) {
      this.createMonthCycle();
    }
    else {
      this.editMonthCycle();
    }
   // this.dialogRef.close();
  }

  createMonthCycle() {
    this.monthCycle = new MonthCycle(this.monthCycleCreateForm.value);
    this.monthCycle.monthStartDate = this.datePipe.transform(new Date(this.monthCycle.monthStartDate), 'yyyy/MM/dd');
    this.monthCycle.monthEndDate = this.datePipe.transform(new Date(this.monthCycle.monthEndDate), 'yyyy/MM/dd');
    this.loading = true;
    this.monthCycleService.createMonthCycle(this.monthCycle).subscribe((data: any) => {
      this.onMonthCycleCreateEvent.emit(this.monthCycle.id);
      if ((data as any).status === true) {
        this.isFormValid = true;
        this.alertService.success('Month Cycle Added Successfully');
        this.dialogRef.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (data as any).message;
      }
      this.loading = false;

    }, (error: any) => {
      this.showErrorMessage(error);
      this.loading = false;

    });
  }
  editMonthCycle() {
    let changeData = new MonthCycle(this.monthCycleCreateForm.value);
    if (this.monthCycle !== null) {
      this.monthCycle.monthCycleName = changeData.monthCycleName;
      this.monthCycle.monthCycleLocalizedName = changeData.monthCycleLocalizedName;
      this.monthCycle.monthStartDate = this.datePipe.transform(new Date(changeData.monthStartDate), 'yyyy/MM/dd');
      this.monthCycle.monthEndDate = this.datePipe.transform(new Date(changeData.monthEndDate), 'yyyy/MM/dd');
      this.monthCycle.totalWorkingDays = changeData.totalWorkingDays;
      this.monthCycle.runningFlag = changeData.runningFlag;
      this.monthCycle.sortOrder = changeData.sortOrder;
      this.monthCycle.companyId = changeData.companyId;
      this.monthCycle.financialYearId = changeData.financialYearId;
      this.loading = true;
      this.monthCycleService.editMonthCycle(this.monthCycle).subscribe((data: any) => {
        this.onMonthCycleEditEvent.emit(this.monthCycle.id);
        if ((data as any).status === true) {
          this.isFormValid = true;
          this.alertService.success("Month Cycle updated successfully");
          this.dialogRef.close();
        }
        else {
          this.isFormValid = false;
          this.errorMessages = (data as any).message;
        }
        this.loading = false;
       
      }, (error: any) => {
        this.showErrorMessage(error);
        this.loading = false;
      });
    }
  }

  close() {
    this.dialogRef.close();
  }
  showErrorMessage(error: any) {
    console.log(error);

  }
  get f() { return this.monthCycleCreateForm.controls; }

}
