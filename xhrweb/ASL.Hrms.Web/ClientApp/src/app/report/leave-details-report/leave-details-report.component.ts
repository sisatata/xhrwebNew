import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TransitionCheckState } from '@angular/material/checkbox';
import { DomSanitizer } from '@angular/platform-browser';
import { AuthService } from 'src/app/auth/services/auth.service';
import { DepartmentFilter, DesignationFilter, EmployeeFilter, Guid, LeaveDetailsReport } from 'src/app/shared/models';
import { BranchService, CompanyService, DepartmentService, DesignationService, EmployeeService, LeaveReportService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-leave-details-report',
  templateUrl: './leave-details-report.component.html',
  styleUrls: ['./leave-details-report.component.css']
})
export class LeaveDetailsReportComponent implements OnInit {
  leveDetailsFilterFormGroup: FormGroup;
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
  isAdmin: any;
  leaveDetailsReport: LeaveDetailsReport;
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
  statuses: any[] = [{ id: 'null', name: 'All' }, { id: 3, name: 'Approved' }, { name: 'Declined', id: 9 }, { name: 'Pending', id: 1 }, { name: 'In Progress', id: 2 }];
  statusId: any;
  startDate: any;
  endDate: any;
  empId: string;
  
  constructor(
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private injector: Injector,
    private datePipe: DatePipe,
    private companyService: CompanyService,
    private employeeService: EmployeeService,
    private authService: AuthService,
    private sanitize: DomSanitizer,
    private leaveReportService: LeaveReportService,
    private departmentServce: DepartmentService,
    private designationService: DesignationService,
    private branchService: BranchService,
    private http: HttpClient,
  ) { 

    this.isEmployee = this.authService.isEmployee;
    this.isAdmin = this.authService.isAdmin;
    this.empId = this.authService.getLoggedInUserInfo().employeeId;
  //  console.log(this.isEmployee, this.isAdmin)
  }
  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
    if(this.isAdmin){
    this.onChangeCompany(this.companyId);
    this.getAllEmployees();
    this.getAllBranchByCompanyId();
    }
  }
  buildForm() {
    this.leveDetailsFilterFormGroup = this.formBuilder.group({
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
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId && this.companyId.length > 0) {
      this.getAllBranchByCompanyId();
      this.getALLDepartmentByBranchId(null);
      this.getAllDesignationByDepartmentId(null);
    }
  }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
      this.employees.unshift({ id: Guid.empty, fullName: 'Select All Employee' });
      
    });
  }
  getAllBranchByCompanyId(): void {
    this.branches = [];
    this.loading = true;
    this.branchService.getAllBranchByCompanyId(this.companyId).subscribe((result: any) => {
      this.branches = result;
      // this.loading = false;
      let ids = this.branches.map((item) => {
        return item.id;
      })
      this.getALLDepartmentByBranchId(ids);
    });
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
  getAllEmployeesFilteredList(): void {
    this.employeeFilter.branchIds = this.branchId;
    this.employeeFilter.departmentIds = this.departmentId;
    this.employeeFilter.designationIds = this.designationId;
    this.employeeFilter.employeeIds = this.employeeId;
    this.loading = true;
    this.employeeService.getAllEmployeePagedListWithoutPagination(this.employeeFilter).subscribe(res => {
      this.employees = res;
      this.loading = false;
    }, () => { })
  }
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
    this.leveDetailsFilterFormGroup.controls.departmentId.setValue([]);
    this.leveDetailsFilterFormGroup.controls.designationId.setValue([]);
    this.leveDetailsFilterFormGroup.controls.employeeId.setValue([]);
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
    this.leveDetailsFilterFormGroup.controls.designationId.setValue([]);
    this.leveDetailsFilterFormGroup.controls.employeeId.setValue([]);
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
    this.leveDetailsFilterFormGroup.controls.employeeId.setValue([]);
  }
  onChangeStatus(data: any): void {
    const name = data.value;
    this.statusId = name;
    //this.leaveDetailsReport.statusId = name;
  }
  onSubmit(): void {
    this.submitted = true;
    if (this.leveDetailsFilterFormGroup.invalid) {
      return;
    }
    this.createAndGetLeaveDetailsReport();
  }
  createAndGetLeaveDetailsReport(): void {
    this.loading = true;
    this.leaveDetailsReport = new LeaveDetailsReport(this.leveDetailsFilterFormGroup.value);
    this.leaveDetailsReport.branchIds = this.branchId;
    this.leaveDetailsReport.employeeIds = this.employeeId;
    this.leaveDetailsReport.departmentIds = this.departmentId;
    this.leaveDetailsReport.DesignationIds = this.designationId;
    this.leaveDetailsReport.startDate = this.datePipe.transform(new Date(this.leaveDetailsReport.startDate), 'yyyy-MM-dd');
    this.leaveDetailsReport.endDate = this.datePipe.transform(new Date(this.leaveDetailsReport.endDate), 'yyyy-MM-dd');
    this.leaveDetailsReport.approvalStatusText = this.statusId == 'null'? null : this.statusId;
    if(this.isEmployee  && !this.isAdmin){
      this.leaveDetailsReport.employeeIds=[];
      this.leaveDetailsReport.employeeIds.push(this.empId);
    }
      
    this.leaveReportService.createLeaveDetailsPdfReport(this.leaveDetailsReport).subscribe(res => {
      this.loading = false;
      let url = ((res as any).printPdfUrl);
      this.pdfUrl = this.sanitize.bypassSecurityTrustResourceUrl(url);
    }, () => {
      this.loading = false;
    });
  }
  get f() { return this.leveDetailsFilterFormGroup.controls; }
}
