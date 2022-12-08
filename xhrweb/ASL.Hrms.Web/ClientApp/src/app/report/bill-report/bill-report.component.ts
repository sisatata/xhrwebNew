import { DatePipe } from '@angular/common';
import { HttpResponse } from '@angular/common/http';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthService } from 'src/app/auth/services/auth.service';
import { AttendanceReport, BillClaimReport, Guid } from 'src/app/shared/models';
import { AttendanceReportService, BillClaimReportService, CompanyService, EmployeeService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-bill-report',
  templateUrl: './bill-report.component.html',
  styleUrls: ['./bill-report.component.css']
})
export class BillReportComponent implements OnInit {
  reportFilterFormGroup: FormGroup;
  companies: any;
  companyId: any = localStorage.getItem('companyId');
  submitted: boolean;
  incomeTaxSlabs: any;
  loading: boolean;
  startTime: any;
  endTime: any;
  billClaimReport: BillClaimReport;
  attendanceReport: AttendanceReport;
  isFormValid: boolean;
  employees: any;
  employeeId: any = Guid.empty;
  isAdmin: any;
  isEmployee: any;
  pdfUrl: any;
  constructor(
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private injector: Injector,
    private datePipe: DatePipe,
    private attendanceReportService: AttendanceReportService,
    private billClaimReportService: BillClaimReportService,
    private companyService: CompanyService,
    private employeeService: EmployeeService,
    private authService: AuthService,
    private sanitize: DomSanitizer,
    private http: HttpClient,
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
  downloadBillClaimReport(): void {
    this.submitted = true;
    if (this.reportFilterFormGroup.invalid) {
      return;
    }
    this.billClaimReport = new BillClaimReport(this.reportFilterFormGroup.value);
    this.billClaimReport.startTime = this.datePipe.transform(new Date(this.billClaimReport.startTime), 'yyyy-MM-dd');
    this.billClaimReport.endTime = this.datePipe.transform(new Date(this.billClaimReport.endTime), 'yyyy-MM-dd');
    this.loading = true;
    this.billClaimReportService.createBillClaimReport(this.billClaimReport).subscribe(result => {
      this.generateExcel(result, 'Bill-Claim-Report');
      this.loading = false;
      this.submitted = false;
    }, error => {
      this.loading = false;
      this.submitted = false;
    })
  }
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
  onChangeEmployee(data: any): void {
    this.employeeId = data.value;
  }
  downloadEmployeeBillClaimReport(): void {
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
  downloadAttendanceSummaryReport(): void {
    this.submitted = true;
    if (this.reportFilterFormGroup.invalid) {
      return;
    }
    this.attendanceReport = new AttendanceReport(this.reportFilterFormGroup.value);
    this.attendanceReport.startTime = this.datePipe.transform(new Date(this.attendanceReport.startTime), 'yyyy-MM-dd');
    this.attendanceReport.endTime = this.datePipe.transform(new Date(this.attendanceReport.endTime), 'yyyy-MM-dd');
    this.loading = true;
    this.attendanceReportService.createAttendanceSummaryPdfReport(this.attendanceReport).subscribe(result => {
      this.loading = false;
      let url = ((result as any).printPdfUrl);
      this.pdfUrl = this.sanitize.bypassSecurityTrustResourceUrl(url);
    }, () => {
      this.loading = false;
      this.submitted = false;
    })
  }
}
