import { Component, OnInit, EventEmitter, Injector } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { CompanyService, FinancialYearService } from 'src/app/shared/services';
import { LeaveBalanceService } from '../../shared/services/leave/leave-balance.service';
import { LeaveProcess } from 'src/app/shared/models/leave/leave-process';
import { AlertService } from 'src/app/shared/services/alert.service';
import { Guid } from 'src/app/shared/models';
import { MatSelectChange } from '@angular/material/select';
import { MatOption } from '@angular/material/core';
import { EmployeeService } from '../../shared/services/employee/employee.service';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
@Component({
  selector: 'app-leave-process',
  templateUrl: './leave-process.component.html',
  styleUrls: ['./leave-process.component.css']
})
export class LeaveProcessComponent extends BaseAuthorizedComponent implements OnInit {
  onLeaveProcessEvent: EventEmitter<any> = new EventEmitter();
  leaveProcessFilterFormGroup: FormGroup
  leaveProcess: LeaveProcess = new LeaveProcess();
  processingStartDate: any;
  processingEndDate: any;
  companies: any;
  employeeId?: any = Guid.empty;
  //employeeId?:any='eebba2c2-a441-4603-a3ac-cd5f76658fa9';
  financialYears: any;
  leaveCalendarId: any;
  financialYearName: any;
  submitted: boolean;
  employees: any;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  constructor(
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private financialYearService: FinancialYearService,
    private leaveBalanceService: LeaveBalanceService,
    private employeeService: EmployeeService,
    private alertService: AlertService,
    private injector: Injector
  ) {
    super(injector);
    this.setInitialValue();
    this.buildForm();
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
    this.getAllFinancialYearByCompanyId();
  }
  ngOnInit() {
    this.setInitialValue();
    this.buildForm();
    this.getAllCompanies();
    this.getAllFinancialYearByCompanyId();
  }
  get f() { return this.leaveProcessFilterFormGroup.controls; }
  buildForm() {
    this.leaveProcessFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId],
      leaveCalendarId: [this.leaveProcess.leaveCalendarId, [Validators.required]],
      employeeId: [this.leaveProcess.employeeId],
      //processingStartDate: [this.leaveProcess.processingStartDate, Validators.required],
      //processingEndDate: [this.leaveProcess.processingEndDate, Validators.required]
    });
  }
  setInitialValue() {
    var currentDate = new Date();
    this.processingStartDate = currentDate;
    this.processingEndDate = currentDate;
  }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
    });
  }
  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllFinancialYearByCompanyId();
      this.getAllEmployees();
    }
  }
  onChangeFinancialYears(event: MatSelectChange) {
    var financialYearName = (event.source.selected as MatOption).viewValue;
    var financialYearId = event.source.value
    this.leaveCalendarId = financialYearId;
    this.financialYearName = financialYearName;
  }
  getAllFinancialYearByCompanyId() {
    this.financialYearService.getAllFinancialYearByCompanyId(this.companyId).subscribe(result => {
      this.financialYears = result;
    })
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  leaveProcesses() {
    this.leaveProcess.leaveCalendarId = this.leaveCalendarId;
    this.leaveProcess = new LeaveProcess(this.leaveProcessFilterFormGroup.value);
    this.leaveProcess.companyId = this.companyId;
    this.loading = true;
    this.leaveBalanceService.processLeave(this.leaveProcess).subscribe((data: any) => {
      this.onLeaveProcessEvent.emit(this.leaveProcess.id);
      if ((data as any).status === true) {
        this.isFormValid = true;
        this.alertService.success("Leave Process Successfully");
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
  processLeave() {
    this.submitted = true;
    if (this.leaveProcessFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.leaveProcesses();
  }
  showErrorMessage(error: any) {
    console.log(error);
  }
}
