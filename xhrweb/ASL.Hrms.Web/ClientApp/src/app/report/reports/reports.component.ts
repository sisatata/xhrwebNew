import { DatePipe } from '@angular/common';
import { HttpResponse } from '@angular/common/http';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthService } from 'src/app/auth/services/auth.service';
import { SearchFilterComponent } from 'src/app/shared/components/search-filter/search-filter.component';
import { AttendanceReport, BillClaimReport, EmployeeFilter, Guid } from 'src/app/shared/models';
import { AttendanceReportService, BillClaimReportService, CompanyService, EmployeeService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-report',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {
  @ViewChild(SearchFilterComponent) searchFilter: SearchFilterComponent;
  reportFilterFormGroup: FormGroup;
  companies: any;
  employeeFilter: EmployeeFilter = new EmployeeFilter();
  companyId: any = localStorage.getItem('companyId');
  submitted: boolean;
  incomeTaxSlabs: any;
  loading: boolean;
  startTime: any;
  endTime: any;
  billClaimReport: BillClaimReport;
  attendanceReport: AttendanceReport;
  isFormValid: boolean;
  daysInMonth:number=31;
  employees: any;
  employeeId: any = Guid.empty;
  isAdmin: any;
  isEmployee: any;
  pdfUrl: any;
  constructor(
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private datePipe: DatePipe,
    private attendanceReportService: AttendanceReportService,
    private billClaimReportService: BillClaimReportService,
    private companyService: CompanyService,
    private employeeService: EmployeeService,
    private authService: AuthService,
    private sanitize: DomSanitizer,
  ) {
    this.isEmployee = this.authService.isEmployee;
    this.isAdmin = this.authService.isAdmin;
    if (!this.isAdmin)
      this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
  }
  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
    this.getAllEmployees();
   
  }
  buildForm() {
    this.reportFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      startTime: [this.startTime, [Validators.required]],
      endTime: [this.endTime, [Validators.required]],
      employeeId: [this.employeeId]
    });
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
      this.employees.unshift({ id: Guid.empty, fullName: 'Select All Employee' });
    });
  }
  get f() { return this.reportFilterFormGroup.controls; }
  generateExcel(result: any, fileName: any): void {
    if (window.navigator.msSaveOrOpenBlob) {
      window.navigator.msSaveBlob(result.body, fileName);
    } else {
      const link = window.URL.createObjectURL(result.body);
      const a = document.createElement('a');
      document.body.appendChild(a);
      a.setAttribute('style', 'display: none');
      a.href = link;
      a.download = fileName;
      a.click();
      window.URL.revokeObjectURL(link);
      a.remove();
    }
  }
  downloadAttndancePdfReport(): void {
    this.submitted = true;
    if (this.reportFilterFormGroup.invalid) {
      return;
    }
    const form = this.searchFilter?.employeeFilterFormGroup;
    this.attendanceReport = new AttendanceReport(this.reportFilterFormGroup.value);
    this.attendanceReport.startTime = this.datePipe.transform(new Date(this.attendanceReport.startTime), 'yyyy-MM-dd');
    this.attendanceReport.endTime = this.datePipe.transform(new Date(this.attendanceReport.endTime), 'yyyy-MM-dd');
    this.attendanceReport.branchIds = form?.value.branchId;
    this.attendanceReport.departmentIds = form?.value.departmentId;
    this.attendanceReport.designationIds = form?.value.designationId;
    this.attendanceReport.employeeIds = this.searchFilter?.employeeId === undefined ? []: this.searchFilter.employeeId ;
    if(!this.isAdmin && this.isEmployee){
     this.attendanceReport.employeeIds = [this.employeeId];
    }
    this.loading = true;
    this.attendanceReportService.createAttendancePdfReport(this.attendanceReport).subscribe(result => {
      this.loading = false;
      let url = ((result as any).printPdfUrl);
      this.pdfUrl = this.sanitize.bypassSecurityTrustResourceUrl(url);
    }, () => {
      this.loading = false;
      this.submitted = false;
    })
  }
  downloadAttndanceExcelReport(): void {
    this.submitted = true;
    if (this.reportFilterFormGroup.invalid) {
      return;
    }
    
    this.attendanceReport = new AttendanceReport(this.reportFilterFormGroup.value);
    this.attendanceReport.startTime = this.datePipe.transform(new Date(this.attendanceReport.startTime), 'yyyy-MM-dd');
    this.attendanceReport.endTime = this.datePipe.transform(new Date(this.attendanceReport.endTime), 'yyyy-MM-dd');
    this.loading = true;
    this.attendanceReportService.createAttendanceExcelReport(this.attendanceReport).subscribe(result => {
      this.generateExcel(result, 'Attendance-Report');
      this.loading = false;
      this.submitted = false;
    }, () => {
      this.loading = false;
      this.submitted = false;
    })
  }
  downloadBillClaimPdfReport(): void {
    this.submitted = true;
    if (this.reportFilterFormGroup.invalid) {
      return;
    }
    this.billClaimReport = new BillClaimReport(this.reportFilterFormGroup.value);
    this.billClaimReport.startTime = this.datePipe.transform(new Date(this.billClaimReport.startTime), 'yyyy-MM-dd');
    this.billClaimReport.endTime = this.datePipe.transform(new Date(this.billClaimReport.endTime), 'yyyy-MM-dd');
    this.loading = true;
    this.billClaimReportService.createBillClaimePdfReport(this.billClaimReport).subscribe(result => {
      this.loading = false;
      let url = ((result as any).printPdfUrl);
      this.pdfUrl = this.sanitize.bypassSecurityTrustResourceUrl(url);
    }, () => {
      this.loading = false;
    })
  }
  downloadJobCardSummaryReport():void{
    this.submitted = true;
    if (this.reportFilterFormGroup.invalid) {
      return;
    }
    const form = this.searchFilter?.employeeFilterFormGroup;
    
    this.attendanceReport = new AttendanceReport(this.reportFilterFormGroup.value);
    this.attendanceReport.startTime = this.datePipe.transform(new Date(this.attendanceReport.startTime), 'yyyy-MM-dd');
    this.attendanceReport.endTime = this.datePipe.transform(new Date(this.attendanceReport.endTime), 'yyyy-MM-dd');
    this.attendanceReport.branchIds = form?.value.branchId;
    this.attendanceReport.departmentIds = form?.value.departmentId;
    this.attendanceReport.designationIds = form?.value.designationId;
    this.attendanceReport.employeeIds = this.searchFilter?.employeeId === undefined ? []: this.searchFilter.employeeId ;
    this.loading = true;
    if(!this.isAdmin && this.isEmployee){
     this.attendanceReport.employeeIds = [this.employeeId];
    }
    this.attendanceReportService.createJobCardSummaryReport(this.attendanceReport).subscribe(result => {
      this.loading = false;
      let url = ((result as any).printPdfUrl);
      this.pdfUrl = this.sanitize.bypassSecurityTrustResourceUrl(url);
    }, () => {
      this.loading = false;
      this.submitted = false;
    })
  }
  onChangeEmployee(data: any): void {
    this.employeeId = data.value;
  }
  downloadAttendanceSummaryReport(): void {
    this.submitted = true;
    if (this.reportFilterFormGroup.invalid) {
      return;
    }
    const form = this.searchFilter?.employeeFilterFormGroup;
    this.attendanceReport = new AttendanceReport(this.reportFilterFormGroup.value);
    this.attendanceReport.startTime = this.datePipe.transform(new Date(this.attendanceReport.startTime), 'yyyy-MM-dd');
    this.attendanceReport.endTime = this.datePipe.transform(new Date(this.attendanceReport.endTime), 'yyyy-MM-dd');
    let stDate = new Date(this.attendanceReport.startTime);
    let edDate = new Date(this.attendanceReport.endTime);
    let diffOfDays = this.calculateDiffOfDays(stDate, edDate);
    if (diffOfDays > this.daysInMonth) {
      this.alertService.error('Difference of start and end date should maximum 31 days');
      return;
    }
    this.loading = true;
    this.attendanceReport.branchIds = form?.value.branchId;
    this.attendanceReport.departmentIds = form?.value.departmentId;
    this.attendanceReport.designationIds = form?.value.designationId;
    this.attendanceReport.employeeIds = this.searchFilter?.employeeId === undefined ? []: this.searchFilter.employeeId ;
    if(!this.isAdmin && this.isEmployee){
     this.attendanceReport.employeeIds = [this.employeeId];
    }
    this.attendanceReportService.createAttendanceSummaryPdfReport(this.attendanceReport).subscribe(result => {
      this.loading = false;
      let url = ((result as any).printPdfUrl);
      this.pdfUrl = this.sanitize.bypassSecurityTrustResourceUrl(url);
    }, () => {
      this.loading = false;
      this.submitted = false;
    })
  }
  calculateDiffOfDays(startTime: any, endTime: any): number {
    if (startTime && endTime) {
      let startDate = startTime;
      let endDate = endTime;
      let diffTime = Math.abs(startDate - endDate);
      let diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
      return diffDays;
    }
  }
}
