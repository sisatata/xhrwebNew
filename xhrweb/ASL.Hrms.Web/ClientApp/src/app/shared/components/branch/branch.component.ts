import { Component, Injector, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DepartmentFilter, DesignationFilter, Employee, EmployeeFilter, EmployeePagedListInput } from '../../models';
import { BranchService, CompanyService, DepartmentService, DesignationService, EmployeeService } from '../../services';
import { AlertService } from '../../services/alert.service';
import { BaseAuthorizedComponent } from '../base-authorized/base-authorized.component';

@Component({
  selector: 'app-branch-list',
  templateUrl: './branch.component.html',
  styleUrls: ['./branch.component.css']
})
export class SearchFilterComponent extends BaseAuthorizedComponent implements OnInit {
@Input() companyId:any;
employees: Employee[];
  employeeId: any;
  branchId: any[] = [];
  companies: any;
  branches: any;
  inputValue: string = '';
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
  searchText: string='';
  constructor(private employeeService: EmployeeService,
    private branchService: BranchService,
    private alertService: AlertService,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private departmentServce: DepartmentService,
    private designationService: DesignationService,
    //private router: Router,
    private injector: Injector,
    private dialog: MatDialog) {
     
    super(injector);
    debugger;
  }
    ngOnInit() {
      this.getAllCompanies();
      this.onChangeCompany(this.companyId);
      this.buildForm();
      this.getAllEmployees();
    }
    buildForm() {
      this.employeeFilterFormGroup = this.formBuilder.group({
        companyId: [this.companyId, Validators.required],
        branchId: [this.branchId],
        departmentId: [this.branchId],
        designationId: [this.designationId],
        searchText:[this.searchText]
      });
    }
    get f() { return this.employeeFilterFormGroup.controls; }
    onChangeCompany(companyId: any) {
      this.companyId = companyId;
      if (companyId && this.companyId.length > 0) {
        this.getAllBranchByCompanyId();
        this.getALLDepartmentByBranchId(null);
        this.getAllDesignationByDepartmentId(null);
      }
      
    }
    onChangeBranch(branchId: any) {
      this.branchId = branchId;
      if (this.branchId && this.branchId.length > 0) {
        this.getALLDepartmentByBranchId(this.branchId);
        
      }
      else {
        
        this.getALLDepartmentByBranchId(null);
        this.branchId=[];
        
      }
      this.departmentId=[];
      this.designationId=[];
       this.employeeFilterFormGroup.controls.departmentId.setValue([]);
         this.employeeFilterFormGroup.controls.designationId.setValue([]);
    }
    onChangeDepartment(departmentId: any) {
      this.departmentId = departmentId;
      if (departmentId && this.departmentId.length > 0) {
        this.getAllDesignationByDepartmentId(this.departmentId);
      }
      else {
        
        this.getAllDesignationByDepartmentId(null);
        this.departmentId=[];
      }
      this.designationId=[];
      this.employeeFilterFormGroup.controls.designationId.setValue([]);
    }
    onChangeDesignation(designationId: any) {
      if(designationId && designationId.length > 0)
      this.designationId = designationId;
      else{
        this.designationId=[];
      }
    }
    getEmployees() {
      this.submitted = true;
      if (this.employeeFilterFormGroup.invalid) {
        return;
      }
      this.branchId = this.f.branchId.value;
      this.getAllEmployeeByBranchId();
    }
    getAllCompanies() {
      this.companyService.getAllCompanies().subscribe((result: any) => {
        this.companies = result;
      });
    }
    getAllBranchByCompanyId(): void {
      this.branches=[];
      this.loading = true;
      this.branchService.getAllBranchByCompanyId(this.companyId).subscribe((result: any) => {
        this.branches = result;
        this.loading = false;
        let ids = this.branches.map((item)=>{
          return item.id;
        })
        this.getALLDepartmentByBranchId(ids);
      });
    }
    getALLDepartmentByBranchId(branchId: string[]): void {
      // for single company
      this.departments=[];
      this.loading = true;
      this.departmentFilter.companyId = this.companyId;
      this.departmentFilter.branchIds = branchId;
      this.departmentServce.getAllDepartmentByBranchIds(this.departmentFilter).subscribe((result: any) => {
        this.departments = result;
        this.loading = false;
        let ids = this.departments.map((item)=>{
          return item.id;
        });
        this.getAllDesignationByDepartmentId(ids);
      }, () => { });
    }
    getAllDesignationByDepartmentId(departmentIds: any): void {
      this.designations=[];
      this.loading = true;
      this.designationFilter.companyId = this.companyId;
      this.designationFilter.entityIds = departmentIds;
      this.designationService.getAllDesignationByDepartmentIds(this.designationFilter).subscribe(result => {
        this.designations = result;
        this.loading = false;
        this.designationId = this.designations.map((item)=>{
          return item.id;
         
          
        });
      })
    }
    getAllEmployees() {
     
      this.input.pageNumber = this.currentPage;
      this.input.pageSize = this.pageSize;
      this.loading = true;
      this.employeeService.getAllEmployeePagedList(this.input).subscribe(result => {
        this.employees = result.data;
        this.allEmployees = result.data;
        this.loading = false;
        this.totalItems = result.rowCount;
        this.generateTotalItemsText();
       
      }, error => {
        
        this.loading = false;
      })
    }
    getAllEmployeeByBranchId() {
     
      this.loading = true;
      this.employeeService.getAllEmployeeByBranchId(this.branchId).subscribe(result => {
        this.employees = result;
        this.totalItems = this.employees.length;
        this.generateTotalItemsText();
      
        this.loading = false;
      }, error => {
       
        this.loading = false;
      })
    }
    createEmployee() {
      // const createemployeeDialogConfig = new MatDialogConfig;
      // // Setting different dialog configurations
      // createemployeeDialogConfig.disableClose = true;
      // createemployeeDialogConfig.autoFocus = true;
      // createemployeeDialogConfig.panelClass = "xHR-dialog";
      // const dialogWidth = window.screen.width <= 576 ? 90: 50;
      // createemployeeDialogConfig.width = `${dialogWidth}%`;
      // var employee = new Employee();
      // employee.companyId = this.companyId;
      // employee.branchId = this.branchId;
      // createemployeeDialogConfig.data = employee;
      // const createEmployeeDialog = this.dialog.open(CreateEmployeeComponent, createemployeeDialogConfig)
      // const successfullCreate = createEmployeeDialog.componentInstance.onEmployeeCreateEvent.subscribe((data) => {
      //   this.getAllEmployees();
      // });
      // createEmployeeDialog.afterClosed().subscribe(() => {
      // });
    }
    editEmployee(employee: Employee) {
      // const editDialogConfig = new MatDialogConfig();
      // // Setting different dialog configurations
      // editDialogConfig.disableClose = true;
      // editDialogConfig.autoFocus = true;
      // editDialogConfig.data = employee
      // const dialogWidth = window.screen.width <= 576 ? 90: 50;
      // editDialogConfig.width = `${dialogWidth}%`;
      // const outletEditDialog = this.dialog.open(CreateEmployeeComponent, editDialogConfig);
      // const successFullEdit = outletEditDialog.componentInstance.onEmployeeEditEvent.subscribe((data) => {
      //   this.getAllEmployees();
      // });
      // outletEditDialog.afterClosed().subscribe(() => {
      // });
    }
    //viewDetailEmployee(employee: Employee) {
    //  this.router.navigate('/employee/employee-details', { state: { id: employee.id, name:employee.fullName } });
    //}
    onDeleteEmployee(employee: Employee): void {
      // const confirmationDialogConfig = new MatDialogConfig();
      // // Setting different dialog configurations
      // confirmationDialogConfig.data = "Are you sure to delete the employee " + employee.fullName;
      // confirmationDialogConfig.panelClass = 'xHR-dialog';
      // const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
      // confirmationDialog.afterClosed().subscribe((result) => {
      //   if (result != undefined) {
      //     this.deleteEmployee(employee);
      //   }
      // });
    }
    deleteEmployee(employee: Employee) {
      // this.employeeService.deleteEmployee(employee).subscribe((data) => {
      //   this.alertService.success('Employee deleted successfully');
      //   this.getAllEmployees();
      // },
      //   (error) => {
      //     this.alertService.error(error);
      //     console.log(error);
      //   });
    }
    pageChanged(event) {
      this.currentPage = event;
      this.getAllEmployees();
      this.generateTotalItemsText();
    }
    onChangePaginationPerPage() {
      this.currentPage = 1;
      this.pageSize = this.pageSize;
      this.getAllEmployees();
      this.generateTotalItemsText();
    }
    inputChange(data: any): void {
      this.employees = [];
      this.employees = this.allEmployees.filter(item => item.fullName.includes(data));
      // console.log(this.employees);
    }
    public saveCode(e): void {
      let id = e.target.value;
      let list = this.allEmployees.filter(x => x.fullName === id)[0];
    }
    search(): void {
      if (this.employeeFilterFormGroup.invalid) {
        return;
      }
      this.loading = true; 
      this.input.pageNumber = this.currentPage;
      this.input.pageSize = this.pageSize;
      this.employeeFilter.branchIds = this.branchId;
      this.employeeFilter.departmentIds = this.departmentId;
      this.employeeFilter.designationIds = this.designationId;
      this.employeeFilter.searchText = this.employeeFilterFormGroup.value.searchText;
      this.employeeService.getAllEmployeeFilteredList(this.employeeFilter).subscribe(res => {
        this.loading = false;
        this.employees = (res as any).data;
        this.allEmployees = (res as any).data;
        this.loading = false;
        this.totalItems = (res as any).rowCount;
        this.generateTotalItemsText();
      }, () => {
      })
    }
}
