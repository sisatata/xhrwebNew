import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CompanyService, BranchService, CustomConfigurationService, EmployeeService, CustomEmployeeConfigurationService } from 'src/app/shared/services';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Configuration, CustomEmployeeConfiguration, Employee, Guid } from '../../../shared/models';
import { AlertService } from '../../../shared/services/alert.service';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-create-custom-employee-configuration',
  templateUrl: './create-custom-employee-configuration.component.html',
  styleUrls: ['./create-custom-employee-configuration.component.css']
})
export class CreateCustomEmployeeConfigurationComponent implements OnInit {
  onCustomConfigurationEmployeeCreateEvent: EventEmitter<any> = new EventEmitter();
  onCustomConfigurationEmployeeEditEvent: EventEmitter<any> = new EventEmitter();
  configurationCreateForm: FormGroup
  submitted = false;
  isEditMode = false;
  branchId: any;
  companies: any;
  branches: any;
  employeeConfiguration: CustomEmployeeConfiguration;
  companyId: any;
  customEmployeeConfiguration: CustomEmployeeConfiguration ;
  configuration: Configuration;
  configurationId: number;
  employeeId: any;
  employees: Employee[] = [];
  isFormValid: boolean;
  errorMessages: any;
  hideme = [];
  loading: boolean = false;
  customValue: any = '';
  createEmployee:boolean = false;
  constructor(
    private companyService: CompanyService,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateCustomEmployeeConfigurationComponent>,
    private formBuilder: FormBuilder,

    private customEmployeeConfigurationService: CustomEmployeeConfigurationService,
    private employeeService: EmployeeService,

    private datePipe: DatePipe,
    @Inject(MAT_DIALOG_DATA) data) {
    this.customEmployeeConfiguration = new CustomEmployeeConfiguration();
    if (isNaN(data)) {
      this.customEmployeeConfiguration = new CustomEmployeeConfiguration(data);
      this.companyId = this.customEmployeeConfiguration.companyId;
    }
    if (this.customEmployeeConfiguration.id) {
      this.customValue = data.value;
    }
   if(this.customEmployeeConfiguration.employeeId===null || this.customEmployeeConfiguration.employeeId === undefined){
         this.createEmployee = true;
   }
  }
  ngOnInit(): void {
    this.getAllCompanies();
    this.getAllEmployees();
    this.buildForm();
  }
  onChangeCompany(): void {
    this.companyId = this.f.companyId.value;
  }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  onChangeEmployee(data: any): void {
    //this.buildForm();
    this.getEmployeeCustomConfiguration();
  }
  getEmployeeCustomConfiguration(): void {
  }
  onSubmit(): void {
    this.submitted = true;
    // stop here if form is invalid
    if (this.configurationCreateForm.invalid) {
      return;
    }
    else if (this.customEmployeeConfiguration.id === null || this.customEmployeeConfiguration.id === undefined)
      this.createCustomEmployeeConfiguration();
    else {
      this.editCustomEmployeeConfiguration();
    }
    //this.dialogRef.close();
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
    this.configurationCreateForm = this.formBuilder.group({
      companyId: [this.customEmployeeConfiguration.companyId, [Validators.required]],
      code: [{ value: this.customEmployeeConfiguration.code, disabled: true }, [Validators.required, Validators.maxLength(250)]],
      defaultValue: [{ value: this.customEmployeeConfiguration.defaultValue, disabled: true }, [Validators.required, Validators.maxLength(250)]],
      customValue: [this.customValue, [Validators.required]],
      description: [this.customEmployeeConfiguration.description, [Validators.required, Validators.maxLength(150)]],
      startDate: [this.customEmployeeConfiguration.startDate , [Validators.required]],
      endDate: [this.customEmployeeConfiguration.endDate , [Validators.required]],
      employeeId: [this.customEmployeeConfiguration.employeeId, [Validators.required]]
    });
  }
  close(): void {
    this.dialogRef.close();
  }
  showErrorMessage(error: any): void {
    console.log(error);
    this.alertService.error("Internal server error happen");
  }
  get f() { return this.configurationCreateForm.controls; }
  createCustomEmployeeConfiguration(): void {
    this.employeeConfiguration = new CustomEmployeeConfiguration(this.configurationCreateForm.value);
    this.employeeConfiguration.code = this.customEmployeeConfiguration.code;
    this.employeeConfiguration.defaultValue = this.customEmployeeConfiguration.defaultValue;
    // this.employeeConfiguration.customValue = this.customEmployeeConfiguration.customValue;
    // transform dates
    this.employeeConfiguration.startDate = this.datePipe.transform(new Date(this.employeeConfiguration.startDate), 'yyyy-MM-dd');
    this.employeeConfiguration.endDate = this.datePipe.transform(new Date(this.employeeConfiguration.endDate), 'yyyy-MM-dd');
    this.loading = true;
    this.customEmployeeConfigurationService.createConfiguration(this.employeeConfiguration).subscribe(result => {
      this.onCustomConfigurationEmployeeCreateEvent.emit((result as any).id);
      if ((result as any).status === true) {
        this.isFormValid = true;
        this.dialogRef.close();
        this.alertService.success("Custom Employee Custom Configuration created successfully");
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
  editCustomEmployeeConfiguration(): void {
    let newData = new CustomEmployeeConfiguration(this.configurationCreateForm.value);
    this.customEmployeeConfiguration.customValue =  newData.customValue;
    this.customEmployeeConfiguration.description = newData.description;
    this.customEmployeeConfiguration.startDate =  this.datePipe.transform(new Date(newData.startDate), 'yyyy-MM-dd');
    this.customEmployeeConfiguration.endDate = this.datePipe.transform(new Date(newData.endDate), 'yyyy-MM-dd');
   this.loading = true;
  // console.log(this.customEmployeeConfiguration)
    this.customEmployeeConfigurationService.editConfiguration(this.customEmployeeConfiguration).subscribe(result => {
      this.onCustomConfigurationEmployeeEditEvent.emit((result as any).id);

       if((result as any).status===true){
         this.isFormValid = true;
         this.dialogRef.close();
         this.alertService.success("Custom Employee Custom Configuration updated successfully");

       }
       else{
         this.isFormValid = false;
         this.errorMessages = (result as any).message;
       }
       this.loading = false;
    },()=>{
      this.loading = false;
    })
    
  }
}
