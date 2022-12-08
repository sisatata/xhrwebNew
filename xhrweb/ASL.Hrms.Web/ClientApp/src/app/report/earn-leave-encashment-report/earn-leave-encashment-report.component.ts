import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSelectChange } from '@angular/material/select';
import { DomSanitizer } from '@angular/platform-browser';
import { AuthService } from 'src/app/auth/services/auth.service';
import {  EarnLeaveEncashmentReport } from 'src/app/shared/models';
import { CompanyService, EarnLeaveEncashmentReportService, FinancialYearService, MonthCycleService } from 'src/app/shared/services';

@Component({
  selector: 'app-earn-leave-encashment-report',
  templateUrl: './earn-leave-encashment-report.component.html',
  styleUrls: ['./earn-leave-encashment-report.component.css']
})
export class EarnLeaveEncashmentReportComponent implements OnInit {
  leaveEncahsmentReportFilterFormGroup: FormGroup;
  companyId: any = localStorage.getItem('companyId');
  financialYearId: any;
  monthCycleId: any;
  loading: boolean = false;
  companies: any;
  financialYears: any;
  monthCycles: any;
  submitted: boolean = false;
  earnLeaveEncashmentReport: EarnLeaveEncashmentReport;


  employeeId: any;
  employees: any;
  downloaded:boolean=false;
  pdfUrl: any;
 
  constructor(
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private authService: AuthService,
    private financialYearService: FinancialYearService,
    private monthCycleService: MonthCycleService,
    private sanitize: DomSanitizer,
    private earnLeaveEncashmentReportService : EarnLeaveEncashmentReportService

  ) { 
    

  }
  ngOnInit(): void {  
    this.buildForm();
    this.getAllCompanies();
    this.onChangeCompany(this.companyId);
    
  }
  buildForm(): void {
    this.leaveEncahsmentReportFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      financialYearId: [this.financialYearId, [Validators.required]],
      monthCycleId: [this.monthCycleId, [Validators.required]],
    
    });
  }
  get f() { return this.leaveEncahsmentReportFilterFormGroup.controls; }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  onChangeEmployee(data: any): void {
    this.employeeId = data.value;
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
    if (this.leaveEncahsmentReportFilterFormGroup.invalid) {
      return;
    }
    this.createAndGetEarnLeaveEncashReport();
     
  }
  
  createAndGetEarnLeaveEncashReport(): void {
    this.earnLeaveEncashmentReport = new EarnLeaveEncashmentReport(this.leaveEncahsmentReportFilterFormGroup.value);
    this.loading = true;
    this.earnLeaveEncashmentReportService.createEmployeeLeaveEncashmentReport(this.earnLeaveEncashmentReport).subscribe(result => {
      this.loading = false;
      let url = ((result as any).printPdfUrl);
      this.pdfUrl = this.sanitize.bypassSecurityTrustResourceUrl(url);
    }, () => {
      this.loading = false;
    })
  }
 
}
