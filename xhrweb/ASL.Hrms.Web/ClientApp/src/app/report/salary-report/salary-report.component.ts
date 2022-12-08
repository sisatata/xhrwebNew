import { DatePipe } from '@angular/common';
import { ThrowStmt } from '@angular/compiler';
import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatSelectChange } from '@angular/material/select';
import { AuthService } from 'src/app/auth/services/auth.service';
import { Pipe, PipeTransform } from '@angular/core';

import { DomSanitizer } from '@angular/platform-browser';
import { Guid, PayslipReport, SalaryReport } from 'src/app/shared/models';
import { AttendanceReportService, BillClaimReportService, CompanyService, EmployeeService, FinancialYearService, MonthCycleService, SalaryReportService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-salary-report',
  templateUrl: './salary-report.component.html',
  styleUrls: ['./salary-report.component.css']
})
export class SalaryReportComponent implements OnInit {
  salaryReportFilterFormGroup: FormGroup;
  companyId: any = localStorage.getItem('companyId');
  financialYearId: any;
  monthCycleId: any;
  loading: boolean = false;
  companies: any;
  financialYears: any;
  monthCycles: any;
  submitted: boolean = false;
  salaryeReport: any;
  salaryReport: SalaryReport;
  employeeId: any;
  employees: any;
  payslipReport: any;
  pdfUrl:any;
  downloaded:boolean=false;
  isEmployee: any;
  payrollManager:any;
  isAdmin: any;
  constructor(
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private employeeService: EmployeeService,
    private authService: AuthService,
    private financialYearService: FinancialYearService,
    private monthCycleService: MonthCycleService,
    private salaryReportService: SalaryReportService,
    private sanitize: DomSanitizer
    

  ) { 
    this.isEmployee = this.authService.isEmployee;

    this.isAdmin = this.authService.isAdmin;
    this.payrollManager = this.authService.isPayrollManger;
    if (!this.isAdmin || !this.payrollManager)
      this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
      

  }
  ngOnInit(): void {  
    this.buildForm();
    this.getAllCompanies();
    this.onChangeCompany(this.companyId);
    this.getAllEmployees();
  }
  buildForm(): void {
    this.salaryReportFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      financialYearId: [this.financialYearId, [Validators.required]],
      monthCycleId: [this.monthCycleId, [Validators.required]],
      employeeId: [this.employeeId]
    });
  }
  get f() { return this.salaryReportFilterFormGroup.controls; }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  onChangeEmployee(data: any): void {
    this.employeeId = data.value;
  }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
      this.employees.unshift({ id: Guid.empty, fullName: 'Select All Employee' });
    });
  }
  onChangeMonthCycle(event: MatSelectChange): void {
    this.monthCycleId = event.value;
  }
  onChangeFinancialYears(event: MatSelectChange): void {
    var financialYearId = event.value;
    this.financialYearId = financialYearId;
    if (financialYearId) {
      this.getAllMonthCycleByCompanyIdAndFinancialYear();
    }
  }
  onChangeCompany(companyId: any): void {
    this.companyId = companyId;
    if (companyId) {
      this.getAllFinancialYearByCompanyId();
    }
  }
  getAllFinancialYearByCompanyId(): void {
    this.financialYearService.getAllFinancialYearByCompanyId(this.companyId).subscribe(result => {
      this.financialYears = result;
    })
  }
  getAllMonthCycleByCompanyIdAndFinancialYear(): void {
    this.monthCycleService.getMonthCycleByCompanyAndFinancialYear(this.companyId, this.financialYearId).subscribe(result => {
      this.monthCycles = result;
    })
  }
  onSubmit(): void {
    this.submitted = true;
    if (this.salaryReportFilterFormGroup.invalid) {
      return;
    }
    this.createAndGetSalaryReport();
  }
  downloadPayslipReport(): void {
    this.submitted = true;
    if (this.salaryReportFilterFormGroup.invalid) {
      return;
    }
    this.createAndGetPayslipReport();
  }
  createAndGetSalaryReport(): void {
    this.salaryReport = new SalaryReport(this.salaryReportFilterFormGroup.value);
    this.loading = true;
    this.salaryReportService.createSalaryPdfReport(this.salaryReport).subscribe(result => {
      this.loading = false;
     
      let url = ((result as any).printPdfUrl);
      this.pdfUrl = this.sanitize.bypassSecurityTrustResourceUrl(url);
      
    }, () => {
      this.loading = false;
    })
  }
  createAndGetPayslipReport(): void {
    this.payslipReport = new PayslipReport(this.salaryReportFilterFormGroup.value);
    this.loading = true;
    this.salaryReportService.createPayslipPdfReport(this.payslipReport).subscribe(result => {
      this.loading = false;
      let url = ((result as any).printPdfUrl);
     
      this.pdfUrl =   this.sanitize.bypassSecurityTrustResourceUrl(url);
     // window.open(url)
    }, () => {
      this.loading = false;
    })
  }
}
