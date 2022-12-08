import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSelectChange } from '@angular/material/select';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { Guid } from 'src/app/shared/models';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { EmployeePaidSalary } from 'src/app/shared/models/payroll/emp-paid-salary';
import { CompanyService, EmployeeSalaryProcessService, FinancialYearService, MonthCycleService } from 'src/app/shared/services';
import { SearchFilterComponent } from 'src/app/shared/components/search-filter/search-filter.component';
@Component({
  selector: 'app-emp-paid-salary',
  templateUrl: './emp-paid-salary.component.html',
  styleUrls: ['./emp-paid-salary.component.css']
})
export class EmpPaidSalaryComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('employee-paid-salary-list-section') blockUI: NgBlockUI;
  @ViewChild(SearchFilterComponent) searchFilter: SearchFilterComponent;
  companies: any;
  employeePaidSalaryFilterFormGroup: FormGroup;
  financialYearName: any;
  financialYearId: any;
  monthCycles: any;
  empPaidSalaries: EmployeePaidSalary[];
  empPaidSalary: EmployeePaidSalary = new EmployeePaidSalary();
  monthCycleId: any;
  Index: any;
  financialYears: any;
  submitted: boolean = false;
  childList: any = [];
  hideme = [];
  isFormValid: boolean;
  errorMessages: any;
  isSearched: boolean = false;
  isEmpty: boolean = true;
  loading: boolean = false;
  employeeId: string;
  isAdmin: boolean = false;
  isEmployee: boolean = false;
  isPayrollManager: boolean = false;
  constructor(
    private injector: Injector,
    private companyService: CompanyService,
    private formBuilder: FormBuilder,
    private monthCycleService: MonthCycleService,
    private financialYearService: FinancialYearService,
    private dialog: MatDialog,
    private salaryProcessService: EmployeeSalaryProcessService) {
    super(injector);
    this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
    this.isAdmin = this.authService.isAdmin;
    this.isEmployee = this.authService.isEmployee;
    this.isPayrollManager = this.authService.isPayrollManger;
    // this.isAdmin = false;
  }
  ngOnInit(): void {
    this.getAllCompanies();
    this.getAllFinancialYearByCompanyId();
    this.buildForm();
    // this.getEmployeeSalaryProcessedData();
  }
  get f() { return this.employeePaidSalaryFilterFormGroup.controls; }
  onChangeCompany(companyId: Guid): void {
    this.emptyTable();
    this.companyId = companyId;
    if (companyId) {
      this.getAllFinancialYearByCompanyId();
    }
  }
  getAllCompanies() {
    this.loading =true;
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
     // this.loading =false;

    })
  }
  buildForm(): void {
    this.employeePaidSalaryFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      financialYearId: [this.financialYearId, Validators.required],
      monthCycleId: [this.monthCycleId, Validators.required],
    })
  }
  getAllFinancialYearByCompanyId(): void {
    this.loading = true;
    this.financialYearService.getAllFinancialYearByCompanyId(this.companyId).subscribe(result => {
      this.financialYears = result;
      const runningYear = result.find(x => x.isRunningYear === true);
      this.financialYearId = runningYear.id;
      this.getAllMonthCycleByCompanyIdAndFinancialYear();

    })
  }
  onChangeFinancialYears(event: MatSelectChange): void {
    this.emptyTable();
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
      const isRunningMonth = result.find(x => x.runningFlag === true);
      this.monthCycleId = isRunningMonth ? isRunningMonth.id : null;;
      if (this.f.financialYearId.pristine === true &&
        this.f.monthCycleId.pristine === true &&
        this.f.companyId.pristine === true) {
        this.buildForm();
        this.submitForm();
      }
      else {
        this.emptyTable();
        this.buildForm();
      }

    })
  }
  onChangeMonthCycle(event: MatSelectChange): void {
    this.emptyTable();
    this.monthCycleId = event.source.value;
  }
  getAllEmployeesSalaryProcessedData(): void {
    this.loading = true;
    this.salaryProcessService.getEmployeesSalaryProcessedData(this.companyId, this.financialYearId, this.monthCycleId).subscribe(result => {
      this.isEmpty = result.length > 0 ? false : true; // status not
      this.empPaidSalaries = result;
      this.isFormValid = true;
      this.totalItems = this.empPaidSalaries.length;
      this.processeUI();
    }, () => {
      this.blockUI.stop();
      
    })
  }
  submitForm(): void {
    this.submitted = true;
    if (this.employeePaidSalaryFilterFormGroup.valid) {
      if (this.isPayrollManager) {
        this.search(true);
      }
      else {
        this.getEmployeeSalaryProcessedData();
      }
    }
    else {
      return;
    }
  }
  public showChildList(index, structureId) {
    let empDetails = this.empPaidSalaries.find(x => x.id === structureId);
    this.childList[index] = empDetails.employeeSalaryProcessedDataComponentList;
    this.hideme[index] = !this.hideme[index];
    this.Index = index;
    
  }
  getEmployeeSalaryProcessedData(): void {
    this.loading = true;
    this.salaryProcessService.
    getEmployeeSalaryProcessedData(this.employeeId, this.financialYearId, this.monthCycleId).subscribe(result => {
      this.empPaidSalaries = result;
     
      this.isEmpty = result.length > 0 ? false : true;
      this.processeUI();
    });
  }
  processeUI(): void {
    this.loading = false;
    this.isSearched = true;
    this.loading = false;
    this.generateTotalItemsText();
  }
  emptyTable(): void {
    this.totalItems = 0;
    this.empPaidSalaries = [];
    this.isEmpty = false;
    this.loading= false;
  }
  search(searched?:boolean):void{
    this.loading = true;
    let form = this.searchFilter.employeeFilterFormGroup;
    this.empPaidSalary.companyId = this.companyId;
    this.empPaidSalary.branchIds = form.value.branchId;
    this.empPaidSalary.departmentIds = form.value.departmentId;
    this.empPaidSalary.designationIds = form.value.designationId;
    this.empPaidSalary.searchText = form.value.searchText;
    this.empPaidSalary.financialYearId = this.financialYearId;
    this.empPaidSalary.monthCycleId = this.monthCycleId;
    this.salaryProcessService.getCompanySalaryProcessedData(this.empPaidSalary).subscribe(res=>{
      this.empPaidSalaries = res;
      this.loading = false;
      if(searched===true)
        this.currentPage = 1;
    },()=>{
      this.loading = false;
    });
  }
  
}
