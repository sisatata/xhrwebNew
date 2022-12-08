import { Component, OnInit, Injector, ViewChild } from "@angular/core";
import {
  EmployeeService,
  DataService,
} from "src/app/shared/services";
import { FormGroup } from "@angular/forms";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { ConfirmationDialogComponent } from "src/app/shared/components/confirmation-dialog/confirmation-dialog.component";
import { BlockUI, NgBlockUI } from "ng-block-ui";
import {
  DepartmentFilter,
  DesignationFilter,
  Employee,
  EmployeeFilter,
  EmployeePagedListInput,
} from "../shared/models";
import { CreateEmployeeComponent } from "./create-employee/create-employee.component";
import { AlertService } from "../shared/services/alert.service";
import { BaseAuthorizedComponent } from "../shared/components/base-authorized/base-authorized.component";
import { SearchFilterComponent } from "../shared/components/search-filter/search-filter.component";
import { TaskDetailComponent } from "../task/task-detail/task-detail.component";
import { ImportEmployeeComponent } from "./import-employee/import-employee.component";
@Component({
  selector: "app-employee",
  templateUrl: "./employee.component.html",
  styleUrls: ["./employee.component.css"],
})
export class EmployeeComponent
  extends BaseAuthorizedComponent
  implements OnInit
{
  @BlockUI("employee-list-section") blockUI: NgBlockUI;
  @ViewChild(SearchFilterComponent) searchFilter:SearchFilterComponent;
  employees: Employee[];
  employeeId: any;
  branchId: any[] = [];
  companies: any;
  branches: any;
  inputValue: string = "";
  allEmployees: any;
  public codeValue: string;
  departmentFilter: DepartmentFilter = new DepartmentFilter();
  designationFilter: DesignationFilter = new DesignationFilter();
  employeeFilter: EmployeeFilter = new EmployeeFilter();
  //companyId: any;
  employeeFilterFormGroup: FormGroup;
  submitted: boolean;
  loading: boolean = false;
  input: EmployeePagedListInput = new EmployeePagedListInput();
  departments: any;
  designations: any;
  departmentId: any[] = [];
  designationId: any[] = [];
  searchText: string = "";
  constructor(
    private employeeService: EmployeeService,
    private alertService: AlertService,
    private data: DataService,
   // private searchFilterComponent: SearchFilterComponent,
    injector: Injector,
    private dialog: MatDialog
  ) {
    super(injector);
  }
  ngOnInit() {
    this.getAllEmployees();
   
  }
 
  getAllEmployees() {
    this.blockUI.start();
    this.input.pageNumber = this.currentPage;
    this.input.pageSize = this.pageSize;
    this.loading = true;
    this.employeeService.getAllEmployeePagedList(this.input).subscribe(
      (result) => {
        this.employees = result.data;
        this.allEmployees = result.data;
        this.loading = false;
        this.totalItems = result.rowCount;
        this.generateTotalItemsText();
        this.blockUI.stop();
      },
      () => {
        this.blockUI.stop();
        this.loading = false;
      }
    );
  }
  createEmployee() {
    const createemployeeDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createemployeeDialogConfig.disableClose = true;
    createemployeeDialogConfig.autoFocus = true;
    createemployeeDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90 : 70;
    createemployeeDialogConfig.width = `${dialogWidth}%`;
    var employee = new Employee();
    employee.companyId = this.companyId;
    employee.branchId = this.branchId;
    createemployeeDialogConfig.data = employee;
    const createEmployeeDialog = this.dialog.open(
      CreateEmployeeComponent,
      createemployeeDialogConfig
    );
    createEmployeeDialog.afterClosed().subscribe(() => {});
  }
  editEmployee(employee: Employee) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = employee;
    const dialogWidth = window.screen.width <= 576 ? 90 : 70;
    editDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(
      CreateEmployeeComponent,
      editDialogConfig
    );
    outletEditDialog.afterClosed().subscribe(() => {});
  }
  importEmployee():void{
    const importDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    importDialogConfig.disableClose = true;
    importDialogConfig.autoFocus = true;
   
    const dialogWidth = window.screen.width <= 576 ? 50 : 50;
    importDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(
      ImportEmployeeComponent,
      importDialogConfig
    );
    outletEditDialog.afterClosed().subscribe(() => {});
  }
  onDeleteEmployee(employee: Employee): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data =
      "Are you sure to delete the employee " + employee.fullName;
    confirmationDialogConfig.panelClass = "xHR-dialog";
    const confirmationDialog = this.dialog.open(
      ConfirmationDialogComponent,
      confirmationDialogConfig
    );
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteEmployee(employee);
      }
    });
  }
  deleteEmployee(employee: Employee) {
    this.employeeService.deleteEmployee(employee).subscribe(
      () => {
        this.alertService.success("Employee deleted successfully");
        this.getAllEmployees();
      },
      (error) => {
        this.alertService.error(error);
        console.log(error);
      }
    );
  }
  pageChanged(event) {
    this.currentPage = event;
    this.search();
    //this.generateTotalItemsText();
  }
  onChangePaginationPerPage() {
    this.currentPage = 1;
    this.pageSize = this.pageSize;
    this.search();
   // this.generateTotalItemsText();
  }
  inputChange(data: any): void {
    this.employees = [];
    this.employees = this.allEmployees.filter((item) =>
      item.fullName.includes(data)
    );
    
  }
  public saveCode(e): void {
    let id = e.target.value;
  }
  search(searched?:boolean): void {

    let form = this.searchFilter.employeeFilterFormGroup;
    this.loading = true;
    this.input.pageNumber =searched === true? 1:  this.currentPage;
    this.input.pageSize = this.pageSize;
    this.employeeFilter.branchIds = form.value.branchId;
    this.employeeFilter.departmentIds = form.value.departmentId;
    this.employeeFilter.designationIds = form.value.designationId;
    this.employeeFilter.searchText = form.value.searchText;
    this.employeeFilter.pageNumber = searched === true? 1:  this.currentPage;
    this.employeeFilter.pageSize = this.pageSize;
    this.employeeService
      .getAllEmployeeFilteredList(this.employeeFilter)
      .subscribe(
        (res) => {
          this.loading = false;
          this.employees = (res as any).data;
          this.allEmployees = (res as any).data;
          this.loading = false;
          this.totalItems = (res as any).rowCount;
        if(searched === true)
        this.currentPage = 1;
          this.generateTotalItemsText();
        },
        () => {
          this.loading = false;
        }
      );
  }
  employeeExport(): void {
    let form = this.searchFilter.employeeFilterFormGroup;
    this.loading = true;
    this.input.pageNumber = this.currentPage;
    this.input.pageSize = this.pageSize;
    this.employeeFilter.branchIds = form.value.branchId;
    this.employeeFilter.departmentIds = form.value.departmentId;
    this.employeeFilter.designationIds = form.value.designationId;
    this.employeeFilter.searchText = form.value.searchText;
    this.employeeFilter.pageNumber = this.currentPage;
    this.employeeFilter.pageSize = this.pageSize;
    this.employeeService.employeeListExport(this.employeeFilter).subscribe(res=>{
      this.generateExcel(res, 'Employee-list');
      
      this.loading = false;
      this.submitted = false;
    },()=>{
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
  
}
