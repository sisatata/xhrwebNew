import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeAddress } from 'src/app/shared/models';
import { CompanyService, EmployeeAddressService, CommonLookUpTypeService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';
@Component({
  selector: 'app-create-employee-address',
  templateUrl: './create-employee-address.component.html',
  styleUrls: ['./create-employee-address.component.css']
})
export class CreateEmployeeAddressComponent implements OnInit {
  onEmployeeAddressCreateEvent: EventEmitter<number> = new EventEmitter();
  onEmployeeAddressEditEvent: EventEmitter<number> = new EventEmitter();
  employeeAddressCreateForm: FormGroup
  submitted = false;
  employeeId: any;
  companies: any = [];
  thanas: any = [];
  districts: any = [];
  countries: any = [];
  addressTypes: any = [];
  employeeAddress: EmployeeAddress;
  employeeAddressId: any;
  isEditMode = false;
  companyId: any = localStorage.getItem('companyId');// = '2d86dab8-5fc1-436b-856a-60096ded9e16'; // It will come after user login. Now set as default;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  constructor(
    private companyService: CompanyService,
    private dialogRef: MatDialogRef<CreateEmployeeAddressComponent>,
    private formBuilder: FormBuilder,
    private employeeAddressservice: EmployeeAddressService,
    private alertService: AlertService,
    private commonLookUpTypeService: CommonLookUpTypeService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.employeeAddress = new EmployeeAddress();
    if (isNaN(data)) {
      this.employeeAddress = new EmployeeAddress(data);
      this.employeeId = this.employeeAddress.employeeId;
      this.employeeAddressId = this.employeeAddress.id;
      
    }
    if (this.employeeAddress.id) {
      this.isEditMode = true;
      this.getAllThanas(this.employeeAddress.districtId);
    }
    else {
      this.isEditMode = false;
    }
    this.buildForm();
    this.getAllCompanies();
  }
  ngOnInit() {
    this.getAllAddressTypes();
    this.getAllDistricts();
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  onChangeCompany() {
    this.companyId = this.f.companyId.value;
    this.getAllAddressTypes();
  }
  getAllDistricts():void{
    this.employeeAddressservice.getAllDistricts().subscribe(result=>{
        this.districts = result;
       
    });
  }
  getAllThanas(districtId: any):void{
      this.employeeAddressservice.getAllThanas(districtId).subscribe(result=>{
        this.thanas = result;
      })
  }
  getAllAddressTypes() {
    if (this.companyId) {
      this.commonLookUpTypeService.getCommonLookUpTypeByParentCode(this.companyId, "AddressType").subscribe(result => {
        this.addressTypes = result;
      });
    }
  }
  buildForm() {
    this.employeeAddressCreateForm = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      addressLine1: [this.employeeAddress.addressLine1, [Validators.required, Validators.maxLength(150)]],
      addressLine2: [this.employeeAddress.addressLine2, [Validators.maxLength(150)]],
      village: [this.employeeAddress.village, [Validators.required, Validators.maxLength(50)]],
      postOffice: [this.employeeAddress.postOffice, [Validators.required, Validators.maxLength(50)]],
      thanaId: [this.employeeAddress.thanaId],
      districtId: [this.employeeAddress.districtId],
      countryId: [this.employeeAddress.countryId],
      latitude: [this.employeeAddress.latitude],
      longitude: [this.employeeAddress.longitude],
      addressTypeId: [this.employeeAddress.addressTypeId],
    });
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.employeeAddressCreateForm.invalid) {
      return;
    }
    if (this.employeeAddress.id === undefined) {
      this.createEmployeeAddress();
    }
    else {
      this.editEmployeeAddress();
    }
   // this.dialogRef.close();
  }
  createEmployeeAddress() {
    this.employeeAddress = new EmployeeAddress(this.employeeAddressCreateForm.value);
    this.employeeAddress.employeeId = this.employeeId;
     this.loading = true;
    this.employeeAddressservice.createEmployeeAddress(this.employeeAddress).subscribe((data: any) => {
      this.onEmployeeAddressCreateEvent.emit(this.employeeAddress.employeeId);
      //this.alertService.success("EmployeeAddress added successfully");
      if(data.status === true){
        this.isFormValid = true;
        this.alertService.success("Employe Address created successfully");
        this.dialogRef.close();
       }
       else{
        // this.employeeAddressCreateForm.controls['addressLine1'].setErrors({'incorrect': true});
        this.isFormValid = false;
        this.errorMessages = data.message;
        
       }
       this.loading = false;
    }, (error: any) => {
      this.showErrorMessage(error);
      this.loading = false;

    });
  }
  editEmployeeAddress() {
    let newData = new EmployeeAddress(this.employeeAddressCreateForm.value);
    if (this.employeeAddress !== null) {
      this.employeeAddress.employeeAddressId = this.employeeAddressId;
      this.employeeAddress.addressLine1 = newData.addressLine1;
      this.employeeAddress.addressLine2 = newData.addressLine2;
      this.employeeAddress.village = newData.village;
      this.employeeAddress.postOffice = newData.postOffice;
      this.employeeAddress.thanaId = newData.thanaId;
      this.employeeAddress.districtId = newData.districtId;
      this.employeeAddress.countryId = newData.countryId;
      this.employeeAddress.latitude = newData.latitude;
      this.employeeAddress.longitude = newData.longitude;
      this.employeeAddress.addressTypeId = newData.addressTypeId;
      this.loading = true;
      this.employeeAddressservice.editEmployeeAddress(this.employeeAddress).subscribe((data: any) => {
        this.onEmployeeAddressEditEvent.emit(this.employeeAddress.id);
        if(data.status === true){
          this.isFormValid = true;
          this.alertService.success("Employe Address updated successfully");
          this.dialogRef.close();
         }
         else{
          // this.employeeAddressCreateForm.controls['addressLine1'].setErrors({'incorrect': true});
          this.isFormValid = false;
          this.errorMessages = data.message;
          
         }
        this.loading = false;
      }, (error: any) => {
        this.alertService.success(error);
          this.showErrorMessage(error);
          this.loading = false;

      });
    }
  }
  close() {
    this.dialogRef.close();
  }
  showErrorMessage(error: any) {
    console.log(error);
  }
 
  get f() { return this.employeeAddressCreateForm.controls; }
}
