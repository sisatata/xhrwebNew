import { DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { DomSanitizer } from "@angular/platform-browser";
import { AuthService } from "src/app/auth/services/auth.service";
import { LeaveApplication } from "src/app/shared/models";
import { LeaveReport } from "src/app/shared/models/report/leave-report";

import {
  CompanyService,
  EmployeeService,
  LeaveReportService,
  LeaveTypeService,
} from "src/app/shared/services";

@Component({
  selector: "app-leave-report",
  templateUrl: "./leave-report.component.html",
  styleUrls: ["./leave-report.component.css"],
})
export class LeaveReportComponent implements OnInit {
  leaveReportFilterFormGroup: FormGroup;
  companies: any;
  companyId: any = localStorage.getItem("companyId");
  isAdmin: any;
  isEmployee: any;
  submitted: boolean;
  loading: boolean;
  startDate: any;
  endDate: any;
  employeeId: string;
  leaveReport: LeaveReport;
  pdfUrl: any;
  ///
  leaveApplications: LeaveApplication[] = [];
  leaveApplicationId: any;
  leaveApplicationFilterFormGroup: FormGroup;

  startDateInitialValue: any;
  endDateInitialValue: any;
  isFormValid: boolean;
  errorMessages: any;
  leaveReportSubscription: any;
  searched: boolean = false;
  userId: any = localStorage.getItem("userId");
  fullList: any;
  employees: any;
  employeeName: any;
  leaveTypes: any;
  status: any;
  leaveTypeName: any;
  statusType: any;
  statuses: any[] = [
    { id: "null", name: "All" },
    { id: 3, name: "Approved" },
    { name: "Declined", id: 9 },
    { name: "Pending", id: 1 },
  ];
  ///
  constructor(
    private formBuilder: FormBuilder,
    private datePipe: DatePipe,
    private companyService: CompanyService,
    private employeeService: EmployeeService,
    private authService: AuthService,
    private leaveReportService: LeaveReportService,
    private sanitize: DomSanitizer,
    private leaveTypeService: LeaveTypeService  ) {
    this.isEmployee = this.authService.isEmployee;
    this.isAdmin = this.authService.isAdmin;
    this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
  }

  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
    this.getAllLeaveTypeByCompanyId();
    this.getAllEmployees();
  }
  buildForm() {
    this.leaveReportFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      startDate: [this.startDate, [Validators.required]],
      endDate: [this.endDate, [Validators.required]],
      statusId: ["null"],
      leaveTypeName: [this.leaveTypeName],
      employeeName: [this.employeeName],
    });
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  get f() {
    return this.leaveReportFilterFormGroup.controls;
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.leaveReportFilterFormGroup.invalid) return;
    this.createAndGetLeaveReport();
  }
  createAndGetLeaveReport(): void {
    this.loading = true;
    this.leaveReport = new LeaveReport(this.leaveReportFilterFormGroup.value);
    this.leaveReport.employeeName = this.employeeName;
    this.leaveReport.approvalStatusText = this.statusType;
    this.leaveReport.leaveTypeName = this.leaveTypeName;
    this.leaveReport.startDate = this.datePipe.transform(
      new Date(this.leaveReport.startDate),
      "yyyy-MM-dd"
    );
    this.leaveReport.endDate = this.datePipe.transform(
      new Date(this.leaveReport.endDate),
      "yyyy-MM-dd"
    );
    this.leaveReportSubscription = this.leaveReportService
      .createLeavePdfReport(this.leaveReport)
      .subscribe(
        (result) => {
          this.loading = false;
          let url = (result as any).printPdfUrl;
          this.pdfUrl = this.sanitize.bypassSecurityTrustResourceUrl(url);
        },
        () => {
          this.loading = false;
        }
      );
  }
  getAllEmployees(): void {
    this.loading = true;
    this.employeeService.getAllEmployees().subscribe(
      (result) => {
        this.employees = result;
        this.employees.unshift({ fullName: "All", id: "null" });
        this.loading = false;
      },
      () => {
        // this.emptyTable();
        this.loading = false;
      }
    );
  }
  onChangeCompany(companyId: any): void {
    this.companyId = companyId;
    this.leaveReport.companyId = companyId;
  }
  onChangeEmployee(data: any): void {
    const name = data.value;
    this.employeeName = name;
  }

  onChangeStatus(data: any): void {
    const name = data.value;
    this.statusType = name;
  }
  onChangeLeaveTypeName(data: any): void {
    const name = data.value;
    this.leaveTypeName = name;
  }

  getAllLeaveTypeByCompanyId() {
    this.loading = true;
    this.leaveTypeService.getAllLeaveTypeByCompanyIdForCombo(this.companyId).subscribe(
      (result) => {
        this.leaveTypes = result;
        this.loading = false;
        this.leaveTypes.unshift({ leaveTypeName: "All" });
      },
      () => {
        this.loading = false;
      }
    );
  }
}
