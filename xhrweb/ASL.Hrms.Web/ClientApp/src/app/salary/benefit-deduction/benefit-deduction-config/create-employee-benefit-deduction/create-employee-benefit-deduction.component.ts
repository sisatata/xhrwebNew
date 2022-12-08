import { DatePipe } from '@angular/common';
import { Injector } from '@angular/core';
import { Component, EventEmitter, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { BenefitDeductionEmployee, Employee } from 'src/app/shared/models';
import { BenefitDeductionEmployeeService, CompanyService, EmployeeService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-create-employee-benefit-deduction',
  templateUrl: './create-employee-benefit-deduction.component.html',
  styleUrls: ['./create-employee-benefit-deduction.component.css']
})
export class CreateEmployeeBenefitDeductionComponent extends BaseAuthorizedComponent implements OnInit {
  onEmployeeBenefitDeductionCreateEvent: EventEmitter<any> = new EventEmitter();
  onEmployeeBenefitDeductionEditEvent: EventEmitter<any> = new EventEmitter();
  EmployeeBenefitDeductionCreateForm: FormGroup
  submitted = false;
  isEditMode = false;
  branchId: any;
  companies: any;
  branches: any;
  employeeBenefitDeduction: BenefitDeductionEmployee;
  companyId: any;
  configurationId: number;
  employeeId: any;
  isFormValid: boolean;
  errorMessages: any;
  hideme = [];
  loading: boolean = false;
  customValue: any = '';
  employees: Employee[];
  constructor(
    private dialogRef: MatDialogRef<CreateEmployeeBenefitDeductionComponent>,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private alertService: AlertService,
    private datePipe: DatePipe,
    private employeeService: EmployeeService,
    private benefitDeductionEmployeeService: BenefitDeductionEmployeeService,
    private injector: Injector,
    @Inject(MAT_DIALOG_DATA) data) {
    super(injector);
    this.employeeBenefitDeduction = new BenefitDeductionEmployee();
    if (isNaN(data)) {
      this.employeeBenefitDeduction = new BenefitDeductionEmployee(data);
    }
    if (this.employeeBenefitDeduction.id) {
      this.isEditMode = true;
    }
    // get logged in user on create
     this.employeeBenefitDeduction.employeeId =  this.employeeBenefitDeduction.employeeId === undefined? this.authService.getLoggedInUserInfo().employeeId : this.employeeBenefitDeduction.employeeId ;
  }
  ngOnInit(): void {
    this.getAllCompanies();
    this.getAllEmployees();
    this.buildForm();
  }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  getAllEmployees(): void {
    this.loading = true;
    this.employeeService.getAllEmployees().subscribe(result => {
      this.employees = result;
      this.loading = false;
    }, () => {
      //this.emptyTable();
      this.loading = false;
    })
  }
  buildForm(): void {
    this.EmployeeBenefitDeductionCreateForm = this.formBuilder.group({
      benefitDeductionId: [this.employeeBenefitDeduction.benefitDeductionId, [Validators.required]],
      employeeId: [this.employeeBenefitDeduction.employeeId, [Validators.required, Validators.maxLength(250)]],
      remarks: [this.employeeBenefitDeduction.remarks, [Validators.maxLength(50)]],
      startDate: [this.employeeBenefitDeduction.startDate, [Validators.required] ],
      endDate: [this.employeeBenefitDeduction.endDate , [Validators.required]],
      amount: [this.employeeBenefitDeduction.amount, [Validators.required]]
    });
  }
  onChangeEmployee(employee: any): void {
    var empId = employee.source.value;
    this.employeeId = empId;
  }
  close(): void {
    this.dialogRef.close();
  }
  onSubmit(): void {
    this.submitted = true; 
    // stop here if form is invalid
    if (this.EmployeeBenefitDeductionCreateForm.invalid) {
      return;
    }
    if (this.employeeBenefitDeduction.id === undefined || this.employeeBenefitDeduction.id === null) {
      this.createEmployeeBenefitDeduction();
    }
    else {
      //approve method here
      this.editEmployeeBenefitDeduction();
    }
  }
  createEmployeeBenefitDeduction(): void {
    this.employeeBenefitDeduction = new BenefitDeductionEmployee(this.EmployeeBenefitDeductionCreateForm.value);
    this.employeeBenefitDeduction.startDate = this.datePipe.transform(this.employeeBenefitDeduction.startDate, "yyyy-MM-dd");
    this.employeeBenefitDeduction.endDate = this.datePipe.transform(this.employeeBenefitDeduction.endDate, "yyyy-MM-dd");
    this.loading = true;
    this.benefitDeductionEmployeeService.createEmployeeBenefitDeduction(this.employeeBenefitDeduction).subscribe(result => {
      this.onEmployeeBenefitDeductionCreateEvent.emit((result as any).id);
      if ((result as any).status === true) {
        this.isFormValid = true;
        this.alertService.success("Employee benefit deduction successfully created");
        this.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (result as any).message;
      }
      this.loading = false;
    }, () => {
      this.loading = false;
    })
  }
  editEmployeeBenefitDeduction(): void {
    let newData = new BenefitDeductionEmployee(this.EmployeeBenefitDeductionCreateForm.value);
    if (this.employeeBenefitDeduction !== null) {
      this.employeeBenefitDeduction.remarks = newData.remarks;
      this.employeeBenefitDeduction.amount = newData.amount;
      this.employeeBenefitDeduction.employeeId = newData.employeeId;
      this.employeeBenefitDeduction.startDate = this.employeeBenefitDeduction.startDate = this.datePipe.transform(newData.startDate, "yyyy-MM-dd");
      this.employeeBenefitDeduction.endDate = this.employeeBenefitDeduction.endDate = this.datePipe.transform(newData.endDate, "yyyy-MM-dd");
      this.loading = true;
      this.benefitDeductionEmployeeService.editEmployeeBenefitDeduction(this.employeeBenefitDeduction).subscribe(result => {
        this.onEmployeeBenefitDeductionEditEvent.emit((result as any).id);
        if ((result as any).status === true) {
          this.isFormValid = true;
          this.alertService.success("Employee benefit deduction successfully updated");
          this.close();
        }
        else {
          this.isFormValid = false;
          this.errorMessages = (result as any).message;
        }
        this.loading = false;
      }, () => {
        this.loading = false;
      })
    }
  }
  get f() { return this.EmployeeBenefitDeductionCreateForm.controls; }
}
