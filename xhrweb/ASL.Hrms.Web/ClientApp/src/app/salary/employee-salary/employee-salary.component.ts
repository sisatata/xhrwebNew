import { Component, OnInit, Injector, ViewChild } from "@angular/core";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { EmployeeSalary } from "src/app/shared/models/payroll/employee-salary";
import { CreateEmployeeSalaryComponent } from "./create-employee-salary/create-employee-salary.component";
import { ConfirmationDialogComponent } from "src/app/shared/components/confirmation-dialog/confirmation-dialog.component";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import {
  CompanyService,
  EmployeeSalaryService,
  EmployeeService,
} from "src/app/shared/services";
import { BlockUI, NgBlockUI } from "ng-block-ui";
import { BaseAuthorizedComponent } from "src/app/shared/components/base-authorized/base-authorized.component";
import { SearchFilterComponent } from "src/app/shared/components/search-filter/search-filter.component";
@Component({
  selector: "employee-salary",
  templateUrl: "./employee-salary.component.html",
  styleUrls: ["./employee-salary.component.css"],
})
export class EmployeeSalaryComponent
  extends BaseAuthorizedComponent
  implements OnInit
{
  @BlockUI("employee-salary-list-section") blockUI: NgBlockUI;
  @ViewChild(SearchFilterComponent) searchFilter: SearchFilterComponent;
  employeesalarys: EmployeeSalary[];
  employeeSalary: EmployeeSalary = new EmployeeSalary();
  employeesalaryId: any;
  employeesalaryFilterFormGroup: FormGroup;
  companies: any;
  submitted: boolean;
  employees: any;
  Index: any;
  childList: any = [];
  hideme = [];
  isEmployee: boolean = false;
  loading: boolean = false;
  employeeId: string;
  userId: string;
  isAdmin: any;
  isPayrollManager: any;
  constructor(
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private employeesalaryService: EmployeeSalaryService,
    private companyService: CompanyService,
    injector: Injector
  ) {
    super(injector);
    this.isEmployee = this.authService.isEmployee;
    this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
    this.userId = this.authService.getLoggedInUserInfo().userId;
    this.isAdmin = this.authService.isAdmin;
    this.isPayrollManager = this.authService.isPayrollManger;
  }
  ngOnInit() {
    this.buildForm();
   // if (this.isAdmin && this.isPayrollManager)
     // this.onChangeCompany(this.companyId);
    this.getAllCompanies();
    if (this.isEmployee && !this.isPayrollManager)
      this.getEmployeeSalaryByEmployeeId();
  }
  ngAfterViewInit():void{
     if (this.isAdmin && this.isPayrollManager)
      this.onChangeCompany(this.companyId);
  }
  buildForm() {
    this.employeesalaryFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
    });
  }
  get f() {
    return this.employeesalaryFilterFormGroup.controls;
  }
  onChangeCompany(companyId: any) {
    this.companyId = companyId;
   
    if (companyId) {
      this.search();
    //  this.getAllEmployeeSalaryByCompanyId();
    }
  }
  getEmployeeSalary() {
    this.submitted = true;
    if (this.employeesalaryFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.getAllEmployeeSalaryByCompanyId();
  }
  getEmployeeSalaryByEmployeeId(): void {
    this.loading = true;
    this.employeesalaryService
      .getEmployeeSalaryByEmployeeId(this.employeeId)
      .subscribe(
        (result) => {
          this.employeesalarys = result;
          this.loading = false;
          this.totalItems = result.length;
          this.generateTotalItemsText();
        },
        () => {
          this.loading = false;
        }
      );
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  public showChildList(index, structureId) {
    let empsal = this.employeesalarys.find((x) => x.id == structureId);
    this.childList[index] = empsal.employeeSalaryComponentList; // this.employees.find(x => x.id == this.employeeId);
    //this.salaryStructures
    //this.getAllBrancgesByBankId(index, bankId);
    this.hideme[index] = !this.hideme[index];
    this.Index = index;
  }
  createEmployeeSalary() {
    const createEmployeeSalaryDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createEmployeeSalaryDialogConfig.disableClose = true;
    createEmployeeSalaryDialogConfig.autoFocus = true;
    createEmployeeSalaryDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90 : 50;
    createEmployeeSalaryDialogConfig.width = `${dialogWidth}%`;
    var employeesalary = new EmployeeSalary();
    employeesalary.companyId = this.companyId;
    createEmployeeSalaryDialogConfig.data = employeesalary;
    const createEmployeeSalaryDialog = this.dialog.open(
      CreateEmployeeSalaryComponent,
      createEmployeeSalaryDialogConfig
    );
    const successFullEdit =
      createEmployeeSalaryDialog.componentInstance.onEmployeeSalaryCreateEvent.subscribe(
        (data) => {
          this.getAllEmployeeSalaryByCompanyId();
        }
      );
    createEmployeeSalaryDialog.afterClosed().subscribe(() => {});
  }
  editEmployeeSalary(employeesalary: EmployeeSalary) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = employeesalary;
    editDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90 : 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(
      CreateEmployeeSalaryComponent,
      editDialogConfig
    );
    const successFullEdit =
      outletEditDialog.componentInstance.onEmployeeSalaryEditEvent.subscribe(
        (data) => {
         // this.getAllEmployeeSalaryByCompanyId();
         this.search();
        }
      );
    outletEditDialog.afterClosed().subscribe(() => {});
  }
  onDeleteEmployeeSalary(employeesalary: EmployeeSalary): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data =
      "Are you sure to delete the Employee Salary " + employeesalary.fullName;
    confirmationDialogConfig.panelClass = "xHR-dialog";
    const confirmationDialog = this.dialog.open(
      ConfirmationDialogComponent,
      confirmationDialogConfig
    );
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteEmployeeSalary(employeesalary);
      }
    });
  }
  deleteEmployeeSalary(employeesalary: EmployeeSalary) {
    this.loading = true;
    this.employeesalaryService.deleteEmployeeSalary(employeesalary).subscribe(
      () => {
       // this.getAllEmployeeSalaryByCompanyId();
       this.search();
        this.loading = false;
      },
      (error) => {
        console.log(error);
        this.loading = false;
      }
    );
  }
  getAllEmployeeSalaryById(brnchId) {
    this.employeesalaryService
      .getEmployeeSalaryById(brnchId)
      .subscribe((result) => {
        this.employeesalarys = result;
      });
  }
  getAllEmployeeSalaryByCompanyId() {
    this.blockUI.start();
    this.loading = true;
    this.employeesalaryService
      .getAllEmployeeSalaryByCompanyId(this.companyId)
      .subscribe(
        (result) => {
          this.loading = false;
          this.employeesalarys = result;
          this.totalItems = this.employeesalarys.length;
          this.generateTotalItemsText();
          this.blockUI.stop();
        },
        () => {
          this.blockUI.stop();
          this.loading = false;
        }
      );
  }
  search(searched?:boolean):void{
    this.loading = true;
    let form = this.searchFilter.employeeFilterFormGroup;
    this.employeeSalary.companyId = this.companyId;
    this.employeeSalary.branchIds = form.value.branchId;
    this.employeeSalary.departmentIds = form.value.departmentId;
    this.employeeSalary.designationIds = form.value.designationId;
    this.employeeSalary.searchText = form.value.searchText;
    
    this.employeesalaryService.getCurrentSalary(this.employeeSalary).subscribe(res=>{
      this.loading = false;
   
      this.employeesalarys = res as any;
      this.totalItems = (res as any).length;
      if(searched===true)
      this.currentPage = 1;
      this.generateTotalItemsText();
      
      
    },()=>{
      this.loading = false;
    });
    
  }
}
