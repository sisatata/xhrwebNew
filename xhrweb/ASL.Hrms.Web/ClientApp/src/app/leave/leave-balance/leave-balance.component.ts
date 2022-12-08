import { Component, OnInit, Injector, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialog } from "@angular/material/dialog";
import { BaseAuthorizedComponent } from "src/app/shared/components/base-authorized/base-authorized.component";
import { SearchFilterComponent } from "src/app/shared/components/search-filter/search-filter.component";
import {
  EmployeePagedListInput,
  FinancialYear,
  Guid,
  LeaveBalance,
  LeaveType,
} from "src/app/shared/models";
import { EmployeeService, FinancialYearService, LeaveTypeService } from "src/app/shared/services";
import { LeaveBalanceService } from "src/app/shared/services/leave/leave-balance.service";
@Component({
  selector: "app-leave-balance",
  templateUrl: "./leave-balance.component.html",
  styleUrls: ["./leave-balance.component.css"],
})
export class LeaveBalanceComponent
  extends BaseAuthorizedComponent
  implements OnInit
{
  @ViewChild(SearchFilterComponent) searchFilter: SearchFilterComponent;
  input: EmployeePagedListInput = new EmployeePagedListInput();
  companyId: any = localStorage.getItem("companyId");
  leaveBalances: LeaveBalance[] = [];
  leaveBalance: LeaveBalance = new LeaveBalance();
  leaveTypes: {leaveTypeName:string}[]=[];
  leaveTypeId: any;
  leaveTypeFilterFormGroup: FormGroup;
  leaveBalanceFilterFormGroup: FormGroup;
  leaveTypeName:string;
  employeeId: any;
  allLeaveBalances:LeaveBalance[] = [];
  financialYear: any = "";
  loading: boolean = false;
  employees: any;
  submitted: boolean = false;
  isSearched: boolean = false;
  isAdmin: boolean = false;
  financialYears: FinancialYear[];
  constructor(
    private injector: Injector,
    private formBuilder: FormBuilder,
    private employeeService: EmployeeService,
    private leaveBalanceService: LeaveBalanceService,
    private financialYearService: FinancialYearService,
    private leaveTypeService: LeaveTypeService,
    private dialog: MatDialog
  ) {
    super(injector);
    this.isAdmin = this.authService.isAdmin;
    if (!this.isAdmin) {
      this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
    }
    this.getAllEmployees();
    this.getAllFinancialYearByCompanyId();
  }
  ngOnInit(): void {
    this.buildForm();
    this.getAllLeaveTypeByCompanyId();
  }
  getAllFinancialYearByCompanyId() {
    this.financialYearService
      .getAllFinancialYearByCompanyId(this.companyId)
      .subscribe((result) => {
        this.financialYears = result;
      });
  }
  getAllLeaveTypeByCompanyId() {
    
    this.loading = true;
    this.leaveTypeService.getAllLeaveTypeByCompanyId(this.companyId).subscribe(
      (result) => {
        
        this.loading = false;
        
       
        this.leaveTypes = result.map(item=>{
          return{
            leaveTypeName: item.leaveTypeName
          };
        });
        this.leaveTypes.unshift({leaveTypeName:'All'});
      
      },
      (error) => {
       
        this.loading = false;
      }
    );
  }
  onChangeFinancialYear(financialYear: any): void {
    this.financialYear = financialYear;
    this.emptyTable();
  }
  buildForm(): void {
    this.leaveBalanceFilterFormGroup = this.formBuilder.group({
      financialYearId: [this.financialYear, Validators.required],
      employeeId: [this.employeeId],
    });
  }
  getAllEmployees(): void {
    this.loading = true;
    this.employeeService.getAllEmployees().subscribe(
      (result) => {
        this.employees = result;
        this.loading = false;
      },
      () => {
        this.emptyTable();
      }
    );
  }
  onChangeEmployee(employee: any): void {
    var empId = employee.source.value;
    this.employeeId = empId;
    this.emptyTable();
  }
  onChangeLeaveType(leaveType:any):void{
    this.leaveTypeName = leaveType.value;
  }
  onSubmit(): void {
    this.submitted = true;
    if (this.leaveBalanceFilterFormGroup.invalid) {
      return;
    }
    if (!this.isAdmin) this.getEmployeeLeaveBalance();
    else {
      this.getLeaveBalance(true);
    }
  }
  getEmployeeLeaveBalance(): void {
    this.loading = true;
    this.loading = true;
    //console.log(this.totalItems);
    this.leaveBalanceService.getEmployeeLeaveBalanceByCalendar(this.employeeId, this.financialYear).subscribe(result => {
      this.leaveBalances = result;
      this.loading = false;
      this.allLeaveBalances = result;
      this.totalItems = this.leaveBalances.length;
     // this.totalItems = this.leaveBalances.length;
    // console.log(this.totalItems);
      this.generateTotalItemsText();
      this.processeUI();
    }, () => {
      this.emptyTable();
    });
  }
  getLeaveBalance(searched?:boolean): void {
    this.loading = true;
    let form = this.searchFilter.employeeFilterFormGroup;
    this.leaveBalance.companyId = this.companyId;
    this.leaveBalance.leaveCalendar =
      this.leaveBalanceFilterFormGroup.value.financialYearId;
    this.leaveBalance.branchIds = form.value.branchId;
    this.leaveBalance.departmentIds = form.value.departmentId;
    this.leaveBalance.designationIds = form.value.designationId;
    this.leaveBalance.searchText = form.value.searchText;
    //console.log(this.totalItems);
    this.leaveBalanceService
      .getLeaveBalanceSummary(this.leaveBalance)
      .subscribe(
        (result) => {
          this.leaveBalances = result as any;
          this.allLeaveBalances = result as any;
          this.loading = false;
          if(this.leaveTypeName &&  this.leaveTypeName.length>0){
            this.filterWithLeaveType();
          }
          this.totalItems = this.leaveBalances.length;
          if(searched===true)
          this.currentPage = 1;
          this.generateTotalItemsText();
         // this.processeUI();
        },
        () => {
          this.emptyTable();
        }
      );
  }
  filterWithLeaveType():void{
    if(this.leaveTypeName !=='All')
    this.leaveBalances = this.allLeaveBalances.filter(item=> item.leaveTypeName === this.leaveTypeName);
    else{
      this.leaveBalances = this.allLeaveBalances;
    }
    this.totalItems = this.leaveBalances.length;
  }
  emptyTable(): void {
    this.leaveBalances = [];
    this.totalItems = 0;
    this.isSearched = false;
    this.loading = false;
  }
  processeUI(): void {
    this.loading = false;
    this.isSearched = true;
    this.loading = false;
    this.totalItems = this.leaveBalances.length;
    this.generateTotalItemsText();
  }
  search(): void {}
  get f() {
    return this.leaveBalanceFilterFormGroup.controls;
  }
}
