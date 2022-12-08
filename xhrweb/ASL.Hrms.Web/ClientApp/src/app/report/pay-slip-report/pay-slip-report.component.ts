import { DatePipe } from '@angular/common';
import { ThrowStmt } from '@angular/compiler';
import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatSelectChange } from '@angular/material/select';
import { AuthService } from 'src/app/auth/services/auth.service';
import { Guid, PayslipReport, SalaryReport } from 'src/app/shared/models';
import { AttendanceReportService, BillClaimReportService, CompanyService, EmployeeService, FinancialYearService, MonthCycleService, SalaryReportService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
  selector: 'app-pay-slip-report',
  templateUrl: './pay-slip-report.component.html',
  styleUrls: ['./pay-slip-report.component.css']
})
export class PaySlipReportComponent implements OnInit {
  payslipReportFilterFormGroup: FormGroup;
  companyId: any = localStorage.getItem('companyId');
  financialYearId: any;
  monthCycleId: any;
  loading: boolean= false;
  companies: any;
  financialYears: any;
  monthCycles: any;
  submitted: boolean= false;
  salaryeReport: any;
  employeeId: any;
  employees: any;
  payslipReport: PayslipReport;
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
    private financialYearService: FinancialYearService,
    private monthCycleService: MonthCycleService,
    private salaryReportService: SalaryReportService

  ) { }

  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
    this.onChangeCompany(this.companyId);
    this.getAllEmployees();
  }
  onChangeEmployee(data:any):void{
    this.employeeId = data.value;
   }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
      this.employees.unshift({ id: Guid.empty, fullName: 'Select All Employee' });
    });
  }
  buildForm():void {
    this.payslipReportFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      financialYearId: [this.financialYearId, [Validators.required]],
      monthCycleId: [this.monthCycleId, [Validators.required]],
      employeeId:[this.employeeId]
      
    });
  }
  get f() { return this.payslipReportFilterFormGroup.controls; }
  getAllCompanies():void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })  
  }
  onChangeMonthCycle(event: MatSelectChange):void {
    var monthCycleName = (event.source.selected as MatOption).viewValue;
    var monthCycleId = event.source.value;
    this.monthCycleId = event.value;
  
    
  }
  onChangeFinancialYears(event: MatSelectChange):void {
    var financialYearName = (event.source.selected as MatOption).viewValue;
    var financialYearId = event.value;
    this.financialYearId = financialYearId;
  
    if (financialYearId) {
      this.getAllMonthCycleByCompanyIdAndFinancialYear();
     
      
    }
  }
  
  onChangeCompany(companyId: any):void {
    this.companyId = companyId;
    if (companyId) {
     
    
      this.getAllFinancialYearByCompanyId();
      //this.getShiftAllocationByCompanyAndBranch();
    }
  }
  getAllFinancialYearByCompanyId():void {
    this.financialYearService.getAllFinancialYearByCompanyId(this.companyId).subscribe(result => {
      this.financialYears = result;

     
    })
  }
  getAllMonthCycleByCompanyIdAndFinancialYear():void {
    this.monthCycleService.getMonthCycleByCompanyAndFinancialYear(this.companyId, this.financialYearId).subscribe(result => {

      this.monthCycles = result;

     

    })
  }

  onSubmit():void{
   this.submitted = true;
   if(this.payslipReportFilterFormGroup.invalid){
     return ;
   }
   this.createAndGetPayslipReport();

  }
  createAndGetPayslipReport():void{
    this.payslipReport = new PayslipReport(this.payslipReportFilterFormGroup.value);
    this.loading = true;
    this.salaryReportService.createPayslipPdfReport(this.payslipReport).subscribe(result=>{
      this.loading = false;
      let url = ((result as any).printPdfUrl);
      window.open(url)
    },()=>{
      this.loading = false;
    })
  }
}
