import {
  Component,
  EventEmitter,
  Injector,
  Input,
  OnInit,
  Output,
  ViewChild,
} from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialog } from "@angular/material/dialog";
import { Subscription } from "rxjs";
import { EmployeeComponent } from "src/app/employee/employee.component";
import {
  DepartmentFilter,
  DesignationFilter,
  Employee,
  EmployeeFilter,
  EmployeePagedListInput,
} from "../../models";
import {
  BranchService,
  CompanyService,

  DepartmentService,
  DesignationService,
  EmployeeService,
} from "../../services";
import { AlertService } from "../../services/alert.service";
@Component({
  selector: "app-search-filter",
  templateUrl: "./search-filter.component.html",
  styleUrls: ["./search-filter.component.css"],
})
export class SearchFilterComponent implements OnInit {
  @Input() companyId: any;
  @Input() showInputField: boolean;
  @Input() showMultiple: boolean;
  @Input() showEmployeeList:boolean;
  message: any;
  subscription: Subscription;
 @Output('formData') formValue = new EventEmitter<any>();
  private formReady: EventEmitter<FormGroup> = new EventEmitter<any>();
  dropdownList = [];
  selectedItems:any = [];
  dropdownSettings = {};
  settings = {};
  employees: Employee[];
  employeeList:any;
  employeeId: any;
  itemList:any;
 
  branchId: any[] = [];
  companies: any;
  branches: any;
  inputValue: string = "";
  allEmployees: any;
  public codeValue: string;
  departmentFilter: DepartmentFilter = new DepartmentFilter();
  designationFilter: DesignationFilter = new DesignationFilter();
  employeeFilter: EmployeeFilter = new EmployeeFilter();
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
    private branchService: BranchService,
    private alertService: AlertService,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private departmentServce: DepartmentService,
    private designationService: DesignationService,
   
   
    //private router: Router,
    private injector: Injector,
    private dialog: MatDialog
  ) {
    
  }
  ngOnInit() {
    this.getAllCompanies();
    this.onChangeCompany(this.companyId);
    this.buildForm();
    this.getAllEmployees();
    3
    this.showInputField = this.showInputField === false ? false : true;
    this.showMultiple = this.showMultiple === false ? false : true;
    this.showEmployeeList = this.showEmployeeList === true?true:false;
   
    this.initializeEmployeeDropdown();
  }
  buildForm() {
    this.employeeFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      branchId: [this.branchId],
      departmentId: [this.branchId],
      designationId: [this.designationId],
      searchText: [this.searchText],
      employeeId:[this.employeeId]
    });
  }
  get f() {
    return this.employeeFilterFormGroup.controls;
  }
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
    } else {
      this.getALLDepartmentByBranchId(null);
      this.branchId = [];
    }
    this.departmentId = [];
    this.designationId = [];
    this.employeeId =[];
    this.employeeFilterFormGroup.controls.employeeId.setValue([]);
    this.employeeFilterFormGroup.controls.departmentId.setValue([]);
    this.employeeFilterFormGroup.controls.designationId.setValue([]);
  }
  
  onChangeDepartment(departmentId: any) {
    this.departmentId = departmentId;
    if (departmentId && this.departmentId.length > 0) {
      this.getAllDesignationByDepartmentId(this.departmentId);
    } else {
      this.getAllDesignationByDepartmentId(null);
      this.departmentId = [];
    }
    this.designationId = [];
    this.employeeId =[];
    this.employeeFilterFormGroup.controls.employeeId.setValue([]);
    this.employeeFilterFormGroup.controls.designationId.setValue([]);
  }
  onChangeDesignation(designationId: any) {
    if (designationId && designationId.length > 0){
      this.designationId = designationId;
     
    
    }
      
    else {
      this.designationId = [];
    }
    this.getAllEmployeesFilteredList();
    this.employeeId =[];
    this.employeeFilterFormGroup.controls.employeeId.setValue([]);
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
    this.branches = [];
    this.loading = true;
    this.branchService
      .getAllBranchByCompanyId(this.companyId)
      .subscribe((result: any) => {
        this.branches = result;
       // this.loading = false;
        let ids = this.branches.map((item) => {
          return item.id;
        });
        this.getALLDepartmentByBranchId(ids);
      });
  }
  getALLDepartmentByBranchId(branchId: string[]): void {
    // for single company
   if(typeof branchId === 'string'){
     branchId = [branchId];
   }
    this.departments = [];
    this.loading = true;
    this.departmentFilter.companyId = this.companyId;
    this.departmentFilter.branchIds = branchId;
    this.departmentServce
      .getAllDepartmentByBranchIds(this.departmentFilter)
      .subscribe(
        (result: any) => {
          this.departments = result;
        //  this.loading = false;
          let ids = this.departments.map((item) => {
            return item.id;
          });
          this.getAllDesignationByDepartmentId(ids);
        },
        () => {}
      );
  }
  getAllDesignationByDepartmentId(departmentIds: any): void {
    if(typeof departmentIds === 'string'){
      departmentIds = [departmentIds];
    }
    this.designations = [];
    this.loading = true;
    this.designationFilter.companyId = this.companyId;
    this.designationFilter.entityIds = departmentIds;
    this.designationService
      .getAllDesignationByDepartmentIds(this.designationFilter)
      .subscribe((result) => {
        this.designations = result;
      //  this.loading = false;
        this.designationId = this.designations.map((item) => {
          return item.id;
        });
        this.getAllEmployeesFilteredList()
      });
  }
  getAllEmployees() {
    this.loading = true;
    this.employeeService.getAllEmployeePagedList(this.input).subscribe(
      (result) => {
        this.employees = result.data;
        this.allEmployees = result.data;
        this.loading = false;
      },
      (error) => {
        this.loading = false;
      }
    );
  }
  getAllEmployeeByBranchId() {
    this.loading = true;
    this.employeeService.getAllEmployeeByBranchId(this.branchId).subscribe(
      (result) => {
        this.employees = result;
        this.loading = false;
      },
      (error) => {
        this.loading = false;
      }
    );
  }
  public saveCode(e): void {
    let id = e.target.value;
    let list = this.allEmployees.filter((x) => x.fullName === id)[0];
  }
  search(): any {
   
   return this.formValue.emit(this.employeeFilterFormGroup);
  
  }
  getAllEmployeesFilteredList(): void {
    this.employeeFilter.branchIds = this.branchId;
    this.employeeFilter.departmentIds = this.departmentId;
    this.employeeFilter.designationIds = this.designationId;
    this.loading = true;
    this.employeeService.getAllEmployeePagedListWithoutPagination(this.employeeFilter).subscribe(res => {
    this.employeeList = res;
    this.itemList=res;
    this.loading = false;
    }, () => { })
  }
   onItemSelect(item:any){
        this.extractEmployeeIdFromEmployeeList();
        
    }
    OnItemDeSelect(item:any){
      this.extractEmployeeIdFromEmployeeList();
        
    }
    onSelectAll(items: any){
      this.extractEmployeeIdFromEmployeeList();
    }
    onDeSelectAll(items: any){
      this.extractEmployeeIdFromEmployeeList();
    }
  
  initializeEmployeeDropdown():void{
    this.selectedItems = [];
    const width = window.screen.width > 768 ? '2': '1';
    this.settings = {
      text: "Select employees",
      enableSearchFilter: true,
      unSelectAllText:"Un Select All",
      classes: "myclass custom-class",
      labelKey:"fullName",
      primaryKey:"id",
      badgeShowLimit:width,
      showCheckbox:true,
      enableFilterSelectAll:true,
      disabled: false,
      maxHeight:"180"
    }
      
  };
  extractEmployeeIdFromEmployeeList():void{
    const ids =this.selectedItems.map(item=> item.id);
    this.employeeId = ids;
   
  }
}
