import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { AuthService } from 'src/app/auth/services/auth.service';
import { AttendanceDetailsReport, DepartmentFilter, DesignationFilter, EmployeeFilter } from 'src/app/shared/models';
import { AttendanceReportService, BranchService, CompanyService, DepartmentService, DesignationService, EmployeeService } from 'src/app/shared/services';
@Component({
  selector: 'app-attendance-report',
  templateUrl: './attendance-report.component.html',
  styleUrls: ['./attendance-report.component.css']
})
export class AttendanceReportComponent implements OnInit {
  attendanceFilterFormGroup: FormGroup;
  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};
  companies: any;
  companyId: any = localStorage.getItem('companyId');
  submitted: boolean;
  incomeTaxSlabs: any;
  loading: boolean;
  startTime: any;
  endTime: any;
  isFormValid: boolean;
  employees: any;
  employeeId: any[] = [];
  departmentFilter: DepartmentFilter = new DepartmentFilter();
  designationFilter: DesignationFilter = new DesignationFilter();
  employeeFilter: EmployeeFilter = new EmployeeFilter();
  isAdmin: boolean=false;
  attendanceDetailsReport: AttendanceDetailsReport;
  isEmployee: any;
  departments: any;
  designations: any;
  departmentId: any[] = [];
  designationId: any[] = [];
  pdfUrl: any;
  branchId: any;
  branches: any;
  selecetdBranches: any;
  selecetdDepartments: any;
  selecetdEmployees: any;
  filterOptions = [{ id: 1, value: 'All' }, { id: 2, value: 'Late' }, { id: 3, value: 'Absent' }, { id: 4, value: "Early Out" }, { id: 5, value: 'In punch miss' },
  { id: 6, value: 'Out punch miss'}, {id:7, value:'Present'}
  ];
  statusId: any;
  startDate: any;
  endDate: any;
  itemList : any= [];
 
  settings = {};
  empId: string;
  constructor(
    private formBuilder: FormBuilder,
    private datePipe: DatePipe,
    private attendanceReportService: AttendanceReportService,
    private companyService: CompanyService,
    private employeeService: EmployeeService,
    private authService: AuthService,
    private sanitize: DomSanitizer,
    private departmentServce: DepartmentService,
    private designationService: DesignationService,
    private branchService: BranchService,
  ) {
    this.isEmployee = this.authService.isEmployee;
    this.isAdmin = this.authService.isAdmin;
    this.empId = this.authService.getLoggedInUserInfo().employeeId;
  }
  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
    this.onChangeCompany(this.companyId);
    this.getAllEmployees();
    this.getAllBranchByCompanyId();
    this.initializeEmployeeDropdown();
    
   
  }
  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId && this.companyId.length > 0) {
      this.getAllBranchByCompanyId();
      this.getALLDepartmentByBranchId(null);
      this.getAllDesignationByDepartmentId(null);
    }
  }
  getALLDepartmentByBranchId(branchId: string[]): void {
    // for single company
    this.departments = [];
    this.departmentFilter.companyId = this.companyId;
    this.departmentFilter.branchIds = branchId;
    this.departmentServce.getAllDepartmentByBranchIds(this.departmentFilter).subscribe((result: any) => {
      this.departments = result;
      let ids = this.departments.map((item) => {
        return item.id;
      });
      this.getAllDesignationByDepartmentId(ids);
    }, () => { });
  }
  getAllDesignationByDepartmentId(departmentIds: any): void {
    this.designations = [];
    this.designationFilter.companyId = this.companyId;
    this.designationFilter.entityIds = departmentIds;
    this.designationService.getAllDesignationByDepartmentIds(this.designationFilter).subscribe(result => {
      this.designations = result;
      this.designationId = this.designations.map((item) => {
        return item.id;
      });
      this.getAllEmployeesFilteredList();
    }, () => {
    })
  }
  buildForm() {
    this.attendanceFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      startDate: [this.startDate, [Validators.required]],
      endDate: [this.endDate, [Validators.required]],
      employeeId: [this.employeeId],
      branchId: [this.branchId],
      departmentId: [this.departmentId],
      designationId: [this.designationId],
      statusId: [this.statusId, [Validators.required]]
    });
  }
  getAllBranchByCompanyId(): void {
    this.branches = [];
    this.loading = true;
    this.branchService.getAllBranchByCompanyId(this.companyId).subscribe((result: any) => {
      this.branches = result;
      let ids = this.branches.map((item) => {
        return item.id;
      });
      this.getALLDepartmentByBranchId(ids);
    });
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  getAllEmployeesFilteredList(): void {
    this.employeeFilter.branchIds = this.branchId;
    this.employeeFilter.departmentIds = this.departmentId;
    this.employeeFilter.designationIds = this.designationId;
    this.employeeFilter.employeeIds = this.employeeId;
    this.loading = true;
    this.employeeService.getAllEmployeePagedListWithoutPagination(this.employeeFilter).subscribe(res => {
      this.employees = res;
     this.itemList=res;
      this.loading = false;
    }, () => { })
  }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
      this.itemList = result;
     if(this.isEmployee && !this.isAdmin){
        const employee = this.employees.filter(x=>x.id === this.empId)[0];
        this.selectedItems.push(employee);
     }
     
    },()=>{});
  }
  get f() { return this.attendanceFilterFormGroup.controls; }

  onChangeBranch(branchId: any) {
    this.branchId = branchId;
    if (this.branchId && this.branchId.length > 0) {
      this.loading = true;
      this.getALLDepartmentByBranchId(this.branchId);
    }
    else {
      this.getALLDepartmentByBranchId(null);
      this.branchId = [];
    }
    this.departmentId = [];
    this.designationId = [];
    this.employeeId = [];
    this.attendanceFilterFormGroup.controls.departmentId.setValue([]);
    this.attendanceFilterFormGroup.controls.designationId.setValue([]);
    this.attendanceFilterFormGroup.controls.employeeId.setValue([]);
  }
  onChangeDepartment(departmentId: any) {
    this.departmentId = departmentId;
    if (departmentId && this.departmentId.length > 0) {
      this.loading = true;
      this.getAllDesignationByDepartmentId(this.departmentId);
    }
    else {
      this.getAllDesignationByDepartmentId(null);
      this.departmentId = [];
    }
    this.designationId = [];
    this.employeeId = [];
    this.attendanceFilterFormGroup.controls.designationId.setValue([]);
    this.attendanceFilterFormGroup.controls.employeeId.setValue([]);
  }
  onChangeEmployee(employees: any): void {
    this.selecetdEmployees = employees;
    this.employeeId = employees;
  }
  onChangeDesignation(designationId: any) {
    if (designationId && designationId.length > 0) {
      this.loading = true;
      this.designationId = designationId;
    }
    else {
      this.designationId = [];
    }
    this.getAllEmployeesFilteredList();
    this.employeeId = [];
    this.attendanceFilterFormGroup.controls.employeeId.setValue([]);
  }
  onSubmit(): void {
    this.submitted = true;
    this.pushEmployeeId();
    if (this.attendanceFilterFormGroup.invalid) {
      return;
    }
    this.createAndGetAttendanceDetailsReport();
  }
  createAndGetAttendanceDetailsReport(): void {
    this.loading = true;
    this.attendanceDetailsReport = new AttendanceDetailsReport(this.attendanceFilterFormGroup.value);
    this.attendanceDetailsReport.branchIds = this.branchId;
    this.attendanceDetailsReport.employeeIds = this.employeeId;
    this.attendanceDetailsReport.departmentIds = this.departmentId;
    this.attendanceDetailsReport.DesignationIds = this.designationId;
    this.attendanceDetailsReport.startDate = this.datePipe.transform(new Date(this.attendanceDetailsReport.startDate), 'yyyy-MM-dd');
    this.attendanceDetailsReport.endDate = this.datePipe.transform(new Date(this.attendanceDetailsReport.endDate), 'yyyy-MM-dd');
    this.attendanceDetailsReport.type = this.attendanceDetailsReport.statusId;
    this.attendanceReportService.createAttendanceDetailsPdfReport(this.attendanceDetailsReport).subscribe(res => {
    this.loading = false;
      let url = ((res as any).printPdfUrl);
      this.pdfUrl = this.sanitize.bypassSecurityTrustResourceUrl(url);
    }, () => {
      this.loading = false;
    });
  }
  
  pushEmployeeId():void{
    this.employeeId=[];
    this.selectedItems.forEach(item=>{
      this.employeeId.push(item.id);
    });
    
    
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
      disabled: (this.isAdmin && this.isEmployee) === true? false:true ,
      maxHeight:"200"
      
  };
  }
}
