import { Component, OnInit, EventEmitter, Injector } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { CompanyService, FinancialYearService } from 'src/app/shared/services';
import { AttendanceProcessService } from 'src/app/shared/services/attendance/attendance-process.service';
import { AttendanceProcess } from 'src/app/shared/models/attendance/attendance-process';
import { AlertService } from 'src/app/shared/services/alert.service';
import { Guid } from 'src/app/shared/models';
import { MatSelectChange } from '@angular/material/select';
import { MatOption } from '@angular/material/core';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-attendance-process',
  templateUrl: './attendance-process.component.html',
  styleUrls: ['./attendance-process.component.css']
})
export class AttendanceProcessComponent extends BaseAuthorizedComponent implements OnInit {
  onAttendanceProcessEvent: EventEmitter<any> = new EventEmitter();
  attendanceProcessFilterFormGroup: FormGroup
  attendanceProcess: AttendanceProcess = new AttendanceProcess();
  processingStartDate: any;
  processingEndDate: any;
  companies: any;
  loading: boolean;
  employeeId?: any = Guid.empty;
  //employeeId?:any='eebba2c2-a441-4603-a3ac-cd5f76658fa9';
  financialYears: any;
  attendanceCalendarId: any;
  financialYearName: any;
  submitted: boolean;
  constructor(
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private financialYearService: FinancialYearService,
    private attendanceProcessService: AttendanceProcessService,
    private alertService: AlertService,
    private datePipe: DatePipe,
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

  get f() { return this.attendanceProcessFilterFormGroup.controls; }

  buildForm() {

    this.attendanceProcessFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId],
      attendanceCalendarId: [this.attendanceProcess.attendanceCalendarId],
      processingStartDate: [this.attendanceProcess.processingStartDate, Validators.required],
      processingEndDate: [this.attendanceProcess.processingEndDate, Validators.required]
    });
  }

  setInitialValue() {
    var currentDate = new Date();
    this.processingStartDate = currentDate;
    this.processingEndDate = currentDate;
  }

  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllFinancialYearByCompanyId();
    }
  }

  onChangeFinancialYears(event: MatSelectChange) {
    var financialYearName = (event.source.selected as MatOption).viewValue;
    var financialYearId = event.source.value
    this.attendanceCalendarId = financialYearId;
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

  attendanceProcesses() {
    this.attendanceProcess.attendanceCalendarId = this.attendanceCalendarId;
    this.attendanceProcess = new AttendanceProcess(this.attendanceProcessFilterFormGroup.value);
    this.attendanceProcess.processingStartDate = this.datePipe.transform(new Date(this.attendanceProcess.processingStartDate), 'yyyy/MM/dd');
    this.attendanceProcess.processingEndDate = this.datePipe.transform(new Date(this.attendanceProcess.processingEndDate), 'yyyy/MM/dd');
    this.attendanceProcess.companyId = this.companyId;
    this.loading = true;
    this.attendanceProcessService.processAttendance(this.attendanceProcess).subscribe((data: any) => {
      this.onAttendanceProcessEvent.emit(this.attendanceProcess.id);
      this.alertService.success("Attendance Process Successfully");
      this.loading = false;

    }, (error: any) => {
      this.showErrorMessage(error);
      this.loading = false;
    });

  }
  processAttendance() {
    this.submitted = true;
    if (this.attendanceProcessFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.attendanceProcesses();
  }

  showErrorMessage(error: any) {
    console.log(error);

  }

}
