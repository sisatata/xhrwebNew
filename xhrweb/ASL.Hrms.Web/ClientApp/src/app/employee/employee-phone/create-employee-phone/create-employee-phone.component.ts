import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeePhone } from 'src/app/shared/models';
import { EmployeePhoneService, CommonLookUpTypeService, CompanyService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-create-employee-phone',
  templateUrl: './create-employee-phone.component.html',
  styleUrls: ['./create-employee-phone.component.css']
})
export class CreateEmployeePhoneComponent implements OnInit {
  onEmployeePhoneCreateEvent: EventEmitter<any> = new EventEmitter();
  onEmployeePhoneEditEvent: EventEmitter<any> = new EventEmitter();
  employeePhoneCreateForm: FormGroup;
  submitted = false;
  employeeId: any;
  companies: any = [];
  thanas: any = [];
  districts: any = [];
  countries: any = [];
  phoneTypes: any = [];
  employeePhone: EmployeePhone;
  employeePhoneId: number;
  isEditMode = false;
  companyId: any = localStorage.getItem('companyId');
  isFormValid: boolean = true;
  errorMessages: any;
  constructor(
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateEmployeePhoneComponent>,
    private formBuilder: FormBuilder,
    private employeePhoneservice: EmployeePhoneService,
    private commonLookUpTypeService: CommonLookUpTypeService,
    private companyService: CompanyService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.employeePhone = new EmployeePhone();
    if (isNaN(data)) {
      this.employeePhone = new EmployeePhone(data);
      this.employeeId = this.employeePhone.employeeId;
    }
    if (this.employeePhone.id) {
      this.isEditMode = true;
    }
    else {
      this.isEditMode = false;
    }
    this.buildForm();
  }
  ngOnInit() {
    this.getAllCompanies();
    this.getAllPhoneTypes();
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  onChangeCompany() {
    this.companyId = this.f.companyId.value;
  }
  getAllPhoneTypes() {
    if (this.companyId) {
      this.commonLookUpTypeService.getCommonLookUpTypeByParentCode(this.companyId, "PhoneType").subscribe(result => {
        this.phoneTypes = result;
      });
    }
  }
  buildForm() {
    this.employeePhoneCreateForm = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      phoneNumber: [this.employeePhone.phoneNumber, [Validators.required, Validators.maxLength(150), Validators.pattern("^[0-9]+")]],
      phoneTypeId: [this.employeePhone.phoneTypeId]
    });
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.employeePhoneCreateForm.invalid) {
      return;
    }
    if (this.employeePhone.id === undefined) {
      this.createEmployeePhone();
    }
    else {
      this.editEmployeePhone();
    }
  }
  createEmployeePhone() {
    this.employeePhone = new EmployeePhone(this.employeePhoneCreateForm.value);
    this.employeePhone.employeeId = this.employeeId;
    this.employeePhoneservice.createEmployeePhone(this.employeePhone).subscribe((data: any) => {
      this.onEmployeePhoneCreateEvent.emit(this.employeePhone.employeeId);
      if (data.status === true) {
        this.isFormValid = true;
        this.alertService.success("Employee Phone added successfully");
        this.dialogRef.close();
      }
      else {
        this.employeePhoneCreateForm.controls['phoneNumber'].setErrors({ 'incorrect': true });
        // this.isFormValid = false;
        this.errorMessages = (data as any).message;
      }
    }, (error: any) => {
      this.showErrorMessage(error);
    });
  }
  editEmployeePhone() {
    let newData = new EmployeePhone(this.employeePhoneCreateForm.value);
    if (this.employeePhone !== null) {
      this.employeePhone.phoneNumber = newData.phoneNumber;
      this.employeePhone.phoneTypeId = newData.phoneTypeId;
      this.employeePhoneservice.editEmployeePhone(this.employeePhone).subscribe((data: any) => {
        this.onEmployeePhoneEditEvent.emit(this.employeePhone.id)
        if (data.status === true) {
          this.isFormValid = true;
          this.alertService.success("Employee Phone updated successfully");
          this.dialogRef.close();
        }
        else {
          this.employeePhoneCreateForm.controls['phoneNumber'].setErrors({ 'incorrect': true });
          // this.employeePhoneCreateForm.controls['phoneNumber'].setErrors({'incorrect': true});
          //  this.isFormValid = false;
          this.errorMessages = (data as any).message;
        }
      }, (error: any) => {
        this.showErrorMessage(error);
      });
    }
  }
  close() {
    this.dialogRef.close();
  }
  showErrorMessage(error: any) {
    console.log(error);
  }
  get f() { return this.employeePhoneCreateForm.controls; }
}
