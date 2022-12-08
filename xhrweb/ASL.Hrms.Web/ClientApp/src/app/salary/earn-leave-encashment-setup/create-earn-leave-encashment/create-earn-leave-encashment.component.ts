import { DatePipe } from "@angular/common";
import { Component, EventEmitter, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef } from "@angular/material/dialog";
import {
  Company,
  EarnLeaveEncashment,
  Employee,
  FinancialYear,
  LeaveType,
  MonthCycle,
} from "src/app/shared/models";
import {
  CompanyService,
  EarnLeaveEncashmentService,
  EmployeeService,
  FinancialYearService,
  LeaveTypeService,
  MonthCycleService,
} from "src/app/shared/services";
import { AlertService } from "src/app/shared/services/alert.service";
@Component({
  selector: "app-create-earn-leave-encashment",
  templateUrl: "./create-earn-leave-encashment.component.html",
  styleUrls: ["./create-earn-leave-encashment.component.css"],
})
export class CreateEarnLeaveEncashmentComponent implements OnInit {
  onEmployeeEarnLeaveEncashmentCreateEventCreateEvent: EventEmitter<boolean> =
    new EventEmitter();
  employeeEarnLeaveEncashmentCreateForm: FormGroup;
  isFormValid: boolean;
  errorMessages: any;
  earnLeaveEncashment: EarnLeaveEncashment = new EarnLeaveEncashment();
  submitted = false;
  companyId: any = localStorage.getItem("companyId");
  loading: boolean = false;
  companies: Company[] = [];
  employees: Employee[] = [];
  leaveTypes: LeaveType[] = [];
  employeeId: any;
  financialYears: FinancialYear[];
  financialYearId: any;
  monthCycles: MonthCycle[];
  constructor(
    private companyService: CompanyService,
    private employeeService: EmployeeService,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateEarnLeaveEncashmentComponent>,
    private formBuilder: FormBuilder,
    private datePipe: DatePipe,
    private leaveTypeService: LeaveTypeService,
    private earnLeaveEncashmentService: EarnLeaveEncashmentService,
    private financialYearService: FinancialYearService,
    private monthCycleService: MonthCycleService
  ) {

    this.earnLeaveEncashment.companyId = this.companyId;
  }
  ngOnInit(): void {
    this.buildForm();
    this.getAllEmployees();
  //  this.getAllCompanies();
    this.getAllFinancialYearByCompanyId();
   
   

  }
  buildForm() {
    this.employeeEarnLeaveEncashmentCreateForm = this.formBuilder.group({
      employeeId: [this.earnLeaveEncashment.employeeId, [Validators.required]],
      leaveTypeId: [
        this.earnLeaveEncashment.leaveTypeId,
        [Validators.required],
      ],
      financialYearId: [
        this.earnLeaveEncashment.financialYearId,
        [Validators.required],
      ],
      monthCycleId: [
        this.earnLeaveEncashment.monthCycleId,
        [Validators.required],
      ],
      elEncashedDays: [
        this.earnLeaveEncashment.elEncashedDays,
        [Validators.required],
      ],
      remarks: [this.earnLeaveEncashment.remarks],
      companyId: [this.earnLeaveEncashment.companyId, [Validators.required]],
      encashDate: [this.earnLeaveEncashment.encashDate, [Validators.required]],
    });
  }
  getAllEmployees() {
    this.loading = true;
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
      this.loading = false;
    });
  }
  onChangeEmployee(data: any): void {
   
    this.employeeId = data;
    this.getAllLeaveTypeByEmployeeId();
  }
  getAllCompanies():void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  createEmployeeLeaveEncashment(): void {
    this.earnLeaveEncashment = new EarnLeaveEncashment(
      this.employeeEarnLeaveEncashmentCreateForm.value
    );
  }
  getAllFinancialYearByCompanyId() {
    this.financialYearService
      .getAllFinancialYearByCompanyId(this.companyId)
      .subscribe(
        (result) => {
          this.financialYears = result;
        },
        () => {}
      );
  }
  onChangeFinancialYear(financialYearId: any) {
  
    this.financialYearId = financialYearId;
    if (financialYearId) {
      this.getAllMonthCycleByCompanyIdAndFinancialYear();
    }
  }
  getAllMonthCycleByCompanyIdAndFinancialYear() {
    this.loading = true;
    this.monthCycleService
      .getMonthCycleByCompanyAndFinancialYear(
        this.companyId,
        this.financialYearId
      )
      .subscribe(
        (result) => {
          this.monthCycles = result;
          this.loading = false;
        },
        (error) => {
          this.loading = false;
        }
      );
  }
  getAllLeaveTypeByEmployeeId(): void {
    this.loading = true;
    // console.log(this.employeeId);
    this.leaveTypeService
   
      .getAllLeaveTypeByEmployeeId(this.employeeId)
      .subscribe(
        (result) => {
          this.leaveTypes = result;
          this.loading = false;
        },
        () => {
          this.loading = false;
        }
      );
  }
  onSubmit(): void {
    this.submitted = true;
    // console.log(this.employeeEarnLeaveEncashmentCreateForm);
    this.employeeEarnLeaveEncashmentCreateForm.value.companyId = this.companyId;
    if (this.employeeEarnLeaveEncashmentCreateForm.invalid) return;
    this.createEmployeeEarnLeaveEncashment();
  }
  createEmployeeEarnLeaveEncashment(): void {
    this.loading = true;
    this.earnLeaveEncashment = new EarnLeaveEncashment(
      this.employeeEarnLeaveEncashmentCreateForm.value
    );
    this.earnLeaveEncashment.encashDate = this.datePipe.transform(
      new Date(this.earnLeaveEncashment.encashDate),
      "yyyy-MM-dd"
    );
    this.earnLeaveEncashment.companyId = this.companyId;
    this.earnLeaveEncashmentService
      .createEmployeeLeaveEncashment(this.earnLeaveEncashment)
      .subscribe(
        (res) => {
          // console.log(res);
          if ((res as any).status === true) {
            this.onEmployeeEarnLeaveEncashmentCreateEventCreateEvent.emit(true);
            this.isFormValid = true;
            
            this.close();
            this.alertService.success(
              'Earn leave encashment created successfully'
            );
          } else {
            this.isFormValid = false;
           
            this.errorMessages = (res as any).message;
            // console.log(this.errorMessages, res);
            
          }
          this.loading = false;
        },
        () => {
          this.loading = false;
        }
      );
  }
  close(): void {
    this.dialogRef.close();
  }
  get f() {
    return this.employeeEarnLeaveEncashmentCreateForm.controls;
  }
}
