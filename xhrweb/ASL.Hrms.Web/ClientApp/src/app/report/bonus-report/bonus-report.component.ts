import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSelectChange } from '@angular/material/select';
import { DomSanitizer } from '@angular/platform-browser';
import { AuthService } from 'src/app/auth/services/auth.service';
import { BonusReport } from 'src/app/shared/models';
import { BonusReportService, CommonLookUpTypeService, CompanyService, EmployeeService, FinancialYearService } from 'src/app/shared/services';
@Component({
  selector: 'app-bonus-report',
  templateUrl: './bonus-report.component.html',
  styleUrls: ['./bonus-report.component.css']
})
export class BonusReportComponent implements OnInit {
  bonusReportFilterFormGroup: FormGroup;
  companyId: any = localStorage.getItem('companyId');
  financialYearId: any;
  monthCycleId: any;
  loading: boolean = false;
  companies: any;
  financialYears: any;
  pdfUrl: any;
  bonusReport: BonusReport;
  downloaded: boolean = false;
  isEmployee: any;
  payrollManager: any;
  bonusTypeId: any;
  submitted: boolean;
  bonustypes: any[];
  employees: any;
  employeeId: any;
  isAdmin: any;
  constructor(
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private authService: AuthService,
    private financialYearService: FinancialYearService,
    private commonLookUpTypeService: CommonLookUpTypeService,
    private bonusReportService: BonusReportService,
    private employeeService: EmployeeService,
    private sanitize: DomSanitizer
  ) {
    this.isEmployee = this.authService.isEmployee;
    this.payrollManager = this.authService.isPayrollManger;
    if (!this.isAdmin || !this.payrollManager)
      this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
  }
  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
    this.getBonusTypes();
    this.getAllFinancialYearByCompanyId();
    this.getAllEmployees();
  }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  buildForm(): void {
    this.bonusReportFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      financialYearId: [this.financialYearId, [Validators.required]],
      bonusTypeId: [this.bonusTypeId, [Validators.required]],
      employeeId: [this.employeeId]
    });
  }
  getBonusTypes(): void {
    this.commonLookUpTypeService.getCommonLookUpTypeByParentCode(this.companyId, 'FestivalBonusTypes').subscribe(res => {
      this.bonustypes = res;
    }, () => { })
  }
  onChangeFinancialYears(event: MatSelectChange): void {
    var financialYearId = event.value;
    this.financialYearId = financialYearId;
  }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
      this.employees.unshift({ id: null, fullName: 'Select All Employee' });
    });
  }
  getAllFinancialYearByCompanyId(): void {
    this.financialYearService.getAllFinancialYearByCompanyId(this.companyId).subscribe(result => {
      this.financialYears = result;
    });
  }
  onChangeEmployee(data: any): void {
    this.employeeId = data.value;
  }
  onSubmit(): void {
    this.submitted = true;
    if (this.bonusReportFilterFormGroup.invalid) {
      return;
    }
    this.createAndGetBonusReport();
  }
  createAndGetBonusReport(): void {
    this.loading = true;
    this.bonusReport = new BonusReport(this.bonusReportFilterFormGroup.value);
    this.bonusReportService.createBonusPdfReport(this.bonusReport).subscribe(res => {
      this.loading = false;
      let url = ((res as any).printPdfUrl);
      this.pdfUrl = this.sanitize.bypassSecurityTrustResourceUrl(url);
    }, () => {
    })
  }
  get f() { return this.bonusReportFilterFormGroup.controls; }
}
