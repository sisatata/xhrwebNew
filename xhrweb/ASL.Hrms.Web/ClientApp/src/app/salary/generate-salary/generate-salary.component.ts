import { Component, OnInit, Injector } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CompanyService, EmployeeService, FinancialYearService, MonthCycleService, EmployeeSalaryProcessService } from 'src/app/shared/services';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { GenerateSalary } from 'src/app/shared/models/payroll/generate-salary';
import { Guid } from 'src/app/shared/models';
import { AlertService } from 'src/app/shared/services/alert.service';
import { MatSelectChange } from '@angular/material/select';
import { MatOption } from '@angular/material/core';
@Component({
  selector: 'app-generate-salary',
  templateUrl: './generate-salary.component.html',
  styleUrls: ['./generate-salary.component.css']
})
export class GenerateSalaryComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('salary-generate') blockUI: NgBlockUI;
  generateSalary: GenerateSalary = new GenerateSalary();
  generateSalaryFilterFormGroup: FormGroup;
  employeeId?: any = Guid.empty;
  financialYearId: any;
  financialYears: any;
  financialYearName: any;
  companies: any;
  submitted: boolean;
  monthCycles: any;
  monthCycleName: any;
  monthCycleId: any;
  employeeName: any;
  employees: any;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  constructor(
    injector: Injector,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private alertService: AlertService,
    private financialYearService: FinancialYearService,
    private monthCycleService: MonthCycleService,
    private generateSalaryService: EmployeeSalaryProcessService,
    private employeeService: EmployeeService,
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.getAllCompanies();
    this.getAllEmployees();
    this.buildForm();
    this.getAllFinancialYearByCompanyId();
  }
  get f() { return this.generateSalaryFilterFormGroup.controls; }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  onChangeCompany(companyId: Guid): void {
    this.companyId = companyId;
    
    if (companyId) {
      this.getAllFinancialYearByCompanyId();
    }
  }
  buildForm(): void {
    this.generateSalaryFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      financialYearId: [this.financialYearId, Validators.required],
      monthCycleId: [this.monthCycleId, Validators.required],
      employeeId: [this.employeeId],
    })
  }
  getAllFinancialYearByCompanyId(): void {
    this.financialYearService.getAllFinancialYearByCompanyId(this.companyId).subscribe(result => {
      this.financialYears = result;
      const runningYear = result.find(x => x.isRunningYear === true);
      this.financialYearId = runningYear.id;
      this.getAllMonthCycleByCompanyIdAndFinancialYear();
    })
  }
  onChangeFinancialYears(event: MatSelectChange): void {
    var financialYearName = (event.source.selected as MatOption).viewValue;
    var financialYearId = event.source.value
    this.financialYearId = financialYearId;
    this.financialYearName = financialYearName;
    if (financialYearId) {
      this.getAllMonthCycleByCompanyIdAndFinancialYear();
    }
  }
  getAllMonthCycleByCompanyIdAndFinancialYear() {
    this.loading = true;
    this.monthCycleService.getMonthCycleByCompanyAndFinancialYear(this.companyId, this.financialYearId).subscribe(result => {
      this.monthCycles = result;
      this.loading = false;
      const isRunningMonth = result.find(x => x.runningFlag === true);
      this.monthCycleId = isRunningMonth ? isRunningMonth.id : null;
        this.buildForm();
      
    },()=>{
      this.loading = false;
    })
  }
  onChangeMonthCycle(event: MatSelectChange): void {
    this.monthCycleName = (event.source.selected as MatOption).viewValue;
    this.monthCycleId = event.source.value;
  }
  onChangeEmployee(event: MatSelectChange): void {
    this, this.employeeName = (event.source.selected as MatOption).viewValue
    var empId = event.source.value;
    this.employeeId = empId;;
  }
  getAllEmployees(): void {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
    });
  }
  processGenerateSalary(): void {
    this.generateSalary = new GenerateSalary(this.generateSalaryFilterFormGroup.value);
    this.generateSalary.companyId = this.f.companyId.value;
    this.blockUI.start();
    this.loading = true;
    this.generateSalaryService.createGenerateSalary(this.generateSalary).subscribe(result => {
      if (result.status === true) {
        this.isFormValid = true;
        this.alertService.success("Salary Successfully Generarted");
      } else {
        this.isFormValid = false;
        this.errorMessages = result.message;
      }
      this.loading = false;
      this.blockUI.stop();
    }, () => {
      this.blockUI.stop();
      this.alertService.success("Salary Generation Failed");
    })
  }
  submitForm(): void {
    this.submitted = true;
    if (this.generateSalaryFilterFormGroup.valid) {
      this.processGenerateSalary();
    }
    else {
      return;
    }
  }
}
