import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatOption } from "@angular/material/core";
import { MatSelectChange } from "@angular/material/select";
import { DomSanitizer } from "@angular/platform-browser";
import { AuthService } from "src/app/auth/services/auth.service";
import {
  Company,
  FinancialYear,
  MonthCycle,
  ProvidentFundReport,
  TaskReport,
} from "src/app/shared/models";
import {
  CompanyService,
  EmployeeService,
  FinancialYearService,
  MonthCycleService,
  ProvidentFundReportService,
} from "src/app/shared/services";
@Component({
  selector: "app-provident-fund-report",
  templateUrl: "./provident-fund-report.component.html",
  styleUrls: ["./provident-fund-report.component.css"],
})
export class ProvidentFundReportComponent implements OnInit {
  providentFundReportFilterFormGroup: FormGroup;
  dropdownList = [];
  providentFundReport: ProvidentFundReport = new ProvidentFundReport();
  selectedItems = [];
  dropdownSettings = {};
  taskReport: TaskReport = new TaskReport();
  companyId: any = localStorage.getItem("companyId");
  submitted: boolean;
  isFormValid: boolean;
  employees: any;
  employeeId: any;
  loading: boolean = false;
  isAdmin: boolean = false;
  isEmployee: boolean = false;
  startDate: any;
  endDate: any;
  companies: Company[];
  pdfUrl: any;
  itemList: any = [];
  settings = {};
  empId: string;
  monthCycles: MonthCycle[];
  financialYears: FinancialYear[];
  financialYearId: any;
  constructor(
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private employeeService: EmployeeService,
    private providentFundReportService: ProvidentFundReportService,
    private authService: AuthService,
    private financialYearService: FinancialYearService,
    private monthCycleService: MonthCycleService,
    private sanitize: DomSanitizer
  ) {
    this.isEmployee = this.authService.isEmployee;
    this.isAdmin = this.authService.isAdmin;
    this.empId = this.authService.getLoggedInUserInfo().employeeId;
    this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
  }
  ngOnInit(): void {
    this.getAllCompanies();
    this.getAllEmployees();
    this.buildForm();
    this.initializeEmployeeDropdown();
    this.getAllFinancialYearByCompanyId();
  }
  buildForm(): void {
    this.providentFundReportFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      financialYearId: [
        this.providentFundReport.financialYearId,
        [Validators.required],
      ],
      monthCycleId: [
        this.providentFundReport.monthCycleId,
        [Validators.required],
      ],
      employeeId: [this.employeeId],
    });
  }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  getAllEmployees(): void {
    this.employeeService.getAllEmployees().subscribe(
      (result: any) => {
        this.employees = result;
        this.itemList = result;
        const employee = this.employees.filter(
          (x) => x.id === this.employeeId
        )[0];
        this.selectedItems.push(employee);
      },
      () => {}
    );
  }
  onSubmit(): void {
    this.submitted = true;
    if (this.providentFundReportFilterFormGroup.invalid) {
      return;
    }
    this.getProvidentFundReport();
  }
  getProvidentFundReport(): void {
    this.providentFundReport = new ProvidentFundReport(
      this.providentFundReportFilterFormGroup.value
    );
    this.providentFundReport.employeeId =
      this.selectedItems.length === 0 ? null : this.f.employeeId.value[0].id;
    this.loading = true;
    this.providentFundReportService
      .createProvidenfundPdfReport(this.providentFundReport)
      .subscribe(
        (res) => {
          this.loading = false;
          let url = (res as any).printPdfUrl;
          this.pdfUrl = this.sanitize.bypassSecurityTrustResourceUrl(url);
        },
        () => {
          this.loading = false;
        }
      );
  }
  onChangeFinancialYear(event: MatSelectChange): void {
    // var financialYearName = (event.source.selected as MatOption).viewValue;
    var financialYearId = event.value;
    this.financialYearId = financialYearId;
    if (financialYearId) {
      this.getAllMonthCycleByCompanyIdAndFinancialYear();
    }
  }
  getAllMonthCycleByCompanyIdAndFinancialYear(): void {
    this.monthCycleService
      .getMonthCycleByCompanyAndFinancialYear(
        this.companyId,
        this.financialYearId
      )
      .subscribe(
        (result) => {
          this.monthCycles = result;
        },
        () => {}
      );
  }
  getAllFinancialYearByCompanyId(): void {
    this.financialYearService
      .getAllFinancialYearByCompanyId(this.companyId)
      .subscribe(
        (result) => {
          this.financialYears = result;
        },
        () => {}
      );
  }
  initializeEmployeeDropdown(): void {
    this.selectedItems = [];
    const width = window.screen.width > 768 ? "2" : "1";
    this.settings = {
      text: "Select employee",
      enableSearchFilter: true,
      singleSelection: true,
      unSelectAllText: "Un Select All",
      classes: "myclass custom-class",
      labelKey: "fullName",
      primaryKey: "id",
      badgeShowLimit: width,
      showCheckbox: true,
      enableFilterSelectAll: true,
      disabled: (this.isAdmin && this.isEmployee) === true ? false : true,
      maxHeight: "200",
    };
  }
  get f() {
    return this.providentFundReportFilterFormGroup.controls;
  }
}
