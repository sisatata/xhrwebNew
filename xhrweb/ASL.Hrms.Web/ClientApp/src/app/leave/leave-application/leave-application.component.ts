import { Component, OnInit, Injector } from "@angular/core";
import { BlockUI, NgBlockUI } from "ng-block-ui";
import { LeaveApplication } from "src/app/shared/models/leave/leave-application";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { LeaveApplicationService } from "src/app/shared/services/leave/leave-application.service";
import {
  CompanyService,
  EmployeeService,
  FinancialYearService,
  LeaveTypeService,
} from "src/app/shared/services";
import { CreateLeaveApplicationComponent } from "./create-leave-application/create-leave-application.component";
import { BaseAuthorizedComponent } from "src/app/shared/components/base-authorized/base-authorized.component";
import { DatePipe } from "@angular/common";
import { ViewChild } from "@angular/core";
import { SearchFilterComponent } from "src/app/shared/components/search-filter/search-filter.component";
@Component({
  selector: "app-leave-application",
  templateUrl: "./leave-application.component.html",
  styleUrls: ["./leave-application.component.css"],
})
export class LeaveApplicationComponent
  extends BaseAuthorizedComponent
  implements OnInit
{
  @BlockUI("leaveApplication-list-section") blockUI: NgBlockUI;
  @ViewChild(SearchFilterComponent) searchFilter: SearchFilterComponent;
  leaveApplications: LeaveApplication[] = [];
  leaveApplication: LeaveApplication = new LeaveApplication();
  leaveApplicationId: any;
  leaveApplicationFilterFormGroup: FormGroup;
  companies: any;
  submitted: boolean;
  employeeId: any;
  isAdmin: boolean = false;
  endDate: any;
  startDate: any;
  startDateInitialValue: any;
  endDateInitialValue: any;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  searched: boolean = false;
  userId: any = localStorage.getItem("userId");
  fullList: any;
  employees: any;
  employeeName: any;
  leaveTypes: any;
  status: any;
  leaveName: any;
  statusType: any;
  statuses: any[] = [
    { id: 3, name: "Approved" },
    { name: "Declined", id: 9 },
    { name: "Pending", id: 1 },
  ];
  constructor(
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private leaveApplicationService: LeaveApplicationService,
    private companyService: CompanyService,
    injector: Injector,
    private datePipe: DatePipe,
    private employeeService: EmployeeService,
    private financialYearService: FinancialYearService,
    private leaveTypeService: LeaveTypeService
  ) {
    super(injector);
    this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
    this.isAdmin = this.authService.isAdmin;
    this.buildForm();
    var oneYearAgoDate = new Date();
    oneYearAgoDate.setFullYear(oneYearAgoDate.getFullYear() - 1);
    this.leaveApplicationFilterFormGroup.updateValueAndValidity();
  }
  ngOnInit() {
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
    this.setDates();
    this.getAllEmployees();
    this.getAllLeaveTypeByCompanyId();
  }

  buildForm() {
    this.leaveApplicationFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      startDate: ["", Validators.required],
      endDate: ["", Validators.required],
      statusId: [""],
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
  get f() {
    return this.leaveApplicationFilterFormGroup.controls;
  }
  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getCurrentFinancialYearOfCompanyAndGetLeaveApplicationsOfCurrentYear();
      this.emptyTable();
    }
  }
  getCurrentFinancialYearOfCompanyAndGetLeaveApplicationsOfCurrentYear() {
    this.financialYearService
      .getCurrentFinancialYearByCompanyId(this.companyId)
      .subscribe((result) => {
        if (result) {
        }
      });
  }
  getLeaveApplications() {
    this.submitted = true;
    if (this.leaveApplicationFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.startDate = this.f.startDate.value;
    this.endDate = this.f.endDate.value;
    this.startDate = this.datePipe.transform(
      new Date(this.startDate),
      "yyyy-MM-dd"
    );
    this.endDate = this.datePipe.transform(
      new Date(this.endDate),
      "yyyy-MM-dd"
    );
    this.getLeaveApplicationsByRole();
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  applyForLeave() {
    const applyLeaveDialogConfig = new MatDialogConfig();
    applyLeaveDialogConfig.disableClose = true;
    applyLeaveDialogConfig.autoFocus = true;
    applyLeaveDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90 : 50;
    applyLeaveDialogConfig.width = `${dialogWidth}%`;
    var leaveApplication = new LeaveApplication();
    leaveApplication.companyId = this.companyId;
    applyLeaveDialogConfig.data = leaveApplication;
    const applyLeaveDialog = this.dialog.open(
      CreateLeaveApplicationComponent,
      applyLeaveDialogConfig
    );
    applyLeaveDialog.afterClosed().subscribe(() => {
      if (this.isAdmin) {
        this.getLeaveApplicationsByCompanyId();
      } else {
        this.getLeaveApplicationsByEmployeeId();
      }
    });
  }
  editLeaveApplication(leaveApplication: LeaveApplication): void {
    const editLeaveDialogConfig = new MatDialogConfig();
    editLeaveDialogConfig.disableClose = true;
    editLeaveDialogConfig.autoFocus = true;
    editLeaveDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90 : 50;
    editLeaveDialogConfig.width = `${dialogWidth}%`;
    editLeaveDialogConfig.data = leaveApplication;
    const applyLeaveDialog = this.dialog.open(
      CreateLeaveApplicationComponent,
      editLeaveDialogConfig
    );
    applyLeaveDialog.afterClosed().subscribe(() => {
      if (this.isAdmin) {
        this.getLeaveApplicationsByCompanyId();
      } else {
        this.getLeaveApplicationsByEmployeeId();
      }
    });
  }
  getLeaveApplicationsByRole() {
    if (this.isAdmin) {
      this.getLeaveApplicationsByCompanyId();
    } else {
      this.getLeaveApplicationsByEmployeeId();
    }
  }
  getLeaveApplicationsByCompanyId() {
    this.blockUI.start();
    this.loading = true;
    let stDate = this.datePipe.transform(
      new Date(this.startDate),
      "MM-dd-yyyy"
    );
    let edDate = this.datePipe.transform(new Date(this.endDate), "MM-dd-yyyy");
    this.leaveApplicationService
      .getAllLeaveApplicationByCompanyId(this.companyId, stDate, edDate)
      .subscribe(
        (result) => {
          this.leaveApplications = result;
          this.totalItems = this.leaveApplications.length;
          this.searched = true;
          this.loading = false;
          this.generateTotalItemsText();
          this.blockUI.stop();
        },
        () => {
          this.emptyTable();
        }
      );
  }
  getLeaveApplicationsByEmployeeId() {
    this.blockUI.start();
    this.loading = true;
    let stDate = this.datePipe.transform(
      new Date(this.f.startDate.value),
      "MM-dd-yyyy"
    );
    let edDate = this.datePipe.transform(
      new Date(this.f.endDate.value),
      "MM-dd-yyyy"
    );
    this.leaveApplicationService
      .getAllLeaveApplicationByEmployeeId(this.employeeId, stDate, edDate)
      .subscribe(
        (result) => {
          this.leaveApplications = result.employeeLeaveApplictions;
          this.totalItems = this.leaveApplications.length;
          this.loading = false;
          this.searched = true;
          this.generateTotalItemsText();
          this.blockUI.stop();
        },
        () => {
          this.emptyTable();
        }
      );
  }
  getLeaveApplicationByCompanyAndParameter(searched?: boolean): void {
    if (!this.isAdmin) {
      this.getLeaveApplicationsByEmployeeId();
      return;
    }
    this.loading = true;
    let form = this.searchFilter.employeeFilterFormGroup;
    let stDate = this.datePipe.transform(
      new Date(this.f.startDate.value),
      "MM-dd-yyyy"
    );
    let edDate = this.datePipe.transform(
      new Date(this.f.endDate.value),
      "MM-dd-yyyy"
    );
    this.leaveApplication.companyId = this.companyId;
    this.leaveApplication.branchIds = form.value.branchId;
    this.leaveApplication.departmentIds = form.value.departmentId;
    this.leaveApplication.designationIds = form.value.designationId;
    this.leaveApplication.searchText = form.value.searchText;
    this.leaveApplication.startDate = stDate;
    this.leaveApplication.endDate = edDate;
    this.leaveApplication.leaveTypeName = this.leaveName;
    this.leaveApplication.approvalStatusText = this.statusType;
    this.leaveApplicationService
      .getAllLeaveApplication(this.leaveApplication)
      .subscribe(
        (result) => {
          this.leaveApplications = result;
          this.totalItems = this.leaveApplications.length;
          this.searched = true;
          this.loading = false;
          if (searched === true) this.currentPage = 1;
          this.generateTotalItemsText();
          this.blockUI.stop();
        },
        () => {
          this.loading = false;
        }
      );
  }
  emptyTable(): void {
    this.leaveApplications = [];
    this.totalItems = 0;
    this.searched = false;
    this.loading = false;
    this.blockUI.stop();
  }
  setDates(): void {
    var oneMonthAgoDate = new Date();
    var oneDayAgoDate = new Date();
    var today = new Date();
    oneDayAgoDate.setDate(oneDayAgoDate.getDate() - 1);
    oneMonthAgoDate.setMonth(oneMonthAgoDate.getMonth() - 1);
    this.f.startDate.setValue(oneMonthAgoDate);
    this.f.endDate.setValue(today);
  }
  getAllLeaveTypeByCompanyId() {
    this.blockUI.start();
    this.loading = true;
    this.leaveTypeService
      .getAllLeaveTypeByCompanyIdForCombo(this.companyId)
      .subscribe(
        (result) => {
          this.leaveTypes = result;
          this.loading = false;
          this.blockUI.stop();
        },
        () => {
          this.blockUI.stop();
          this.loading = false;
        }
      );
  }

  getAllLeaveTypeByEmployeeId() {
    this.blockUI.start();
    this.loading = true;
    this.leaveTypeService
      .getAllLeaveTypeByEmployeeId(this.employeeId)
      .subscribe(
        (result) => {
          this.leaveTypes = result;
          this.loading = false;
          console.log(this.leaveTypes)
          this.blockUI.stop();
        },
        () => {
          this.blockUI.stop();
          this.loading = false;
        }
      );
  }
  onChangeEmployee(data: any): void {
    const name = data.value;
    this.employeeName = name;
    let obj = this.employees.find((i) => i.fullName === name);
    this.employeeId = obj.id;
    this.getAllLeaveTypeByEmployeeId();
  }

  onChangeStatus(data: any): void {
    const name = data.value;
    this.statusType = name;
  }
  onChangeLeaveTypeName(data: any): void {
    const name = data.value;
    this.leaveName = name;
  }
}
