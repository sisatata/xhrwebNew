import { Component, EventEmitter, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CompanyAddress } from 'src/app/shared/models';
import { CommonLookUpTypeService, CompanyAddressService, DistrictsService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-create-company-address',
  templateUrl: './create-company-address.component.html',
  styleUrls: ['./create-company-address.component.css']
})
export class CreateCompanyAddressComponent implements OnInit {
  onCompanyAddressCreateEvent: EventEmitter<number> = new EventEmitter();
  onCompanyAddressEditEvent: EventEmitter<number> = new EventEmitter();
  companyAddressCreateForm: FormGroup;
  submitted = false;
  companyId: any;
  companies: any = [];
  thanas: any = [];
  districts: any = [];
  countries: any = [];
  addressTypes: any = [];
  companyAddress: CompanyAddress = new CompanyAddress();
  companyAddressId: any;
  isEditMode = false;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  constructor(private dialogRef: MatDialogRef<CreateCompanyAddressComponent>,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private commonLookUpTypeService: CommonLookUpTypeService,
    private districtService: DistrictsService,
    private companyAddressService: CompanyAddressService,
    @Inject(MAT_DIALOG_DATA) data) {
    if (isNaN(data)) {
      this.companyAddress = new CompanyAddress(data);
      this.companyId = this.companyAddress.companyId;
      this.companyAddressId = this.companyAddress.id;
    }
    if (this.companyAddress.id) {
      this.isEditMode = true;
    }
    else {
      this.isEditMode = false;
    }
    if (this.companyAddress.id) {
      this.isEditMode = true;
      this.getAllThanas(this.companyAddress.districtId);
    }
    this.buildForm();
    this.getAllDistricts();
  }
  ngOnInit(): void {
    this.getAllAddressTypes();
  }
  getAllAddressTypes() {
    if (this.companyId) {
      this.commonLookUpTypeService.getCommonLookUpTypeByParentCode(this.companyId, "companyAddressType").subscribe(result => {
        this.addressTypes = result;
      });
    }
  }  
  getAllDistricts(): void {
    this.districtService.getAllDistricts().subscribe(result => {
      this.districts = result;
    });
  }
  getAllThanas(districtId: any): void {
    this.districtService.getAllThanas(districtId).subscribe(result => {
      this.thanas = result;
    })
  }
  buildForm() {
    this.companyAddressCreateForm = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      addressLine1: [this.companyAddress.addressLine1, [Validators.required, Validators.maxLength(150)]],
      addressLine2: [this.companyAddress.addressLine2, [Validators.maxLength(150)]],
      village: [this.companyAddress.village, [Validators.maxLength(50)]],
      postOffice: [this.companyAddress.postOffice, [Validators.maxLength(50)]],
      thanaId: [this.companyAddress.thanaId, [Validators.required]],
      districtId: [this.companyAddress.districtId, [Validators.required]],
      countryId: [this.companyAddress.countryId],
      latitude: [this.companyAddress.latitude],
      longitude: [this.companyAddress.longitude],
      addressTypeId: [this.companyAddress.addressTypeId],
    });
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.companyAddressCreateForm.invalid) {
      return;
    }
    if (this.companyAddress.id === undefined) {
      this.createCompanyAddress();
    }
    else {
      this.editCompanyAddress();
    }
    // this.dialogRef.close();
  }
  createCompanyAddress() {
    this.companyAddress = new CompanyAddress(this.companyAddressCreateForm.value);
    this.companyAddress.companyId = this.companyId;
    this.loading = true;
    this.companyAddressService.createCompanyAddress(this.companyAddress).subscribe((data: any) => {
      this.onCompanyAddressCreateEvent.emit(this.companyAddress.companyId);
      if ((data as any).status === true) {
        this.isFormValid = true;
        this.dialogRef.close();
        this.alertService.success("Company Address added successfully");
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (data as any).message;
      }
      this.loading = false;
    }, (error: any) => {
      this.showErrorMessage(error);
      this.loading = false;
    });
  }
  editCompanyAddress() {
    let newData = new CompanyAddress(this.companyAddressCreateForm.value);
    if (this.companyAddress !== null) {
      this.companyAddress.companyId = this.companyId;
      this.companyAddress.addressLine1 = newData.addressLine1;
      this.companyAddress.addressLine2 = newData.addressLine2;
      this.companyAddress.village = newData.village;
      this.companyAddress.postOffice = newData.postOffice;
      this.companyAddress.thanaId = newData.thanaId;
      this.companyAddress.districtId = newData.districtId;
      this.companyAddress.countryId = newData.countryId;
      this.companyAddress.latitude = newData.latitude;
      this.companyAddress.longitude = newData.longitude;
      this.companyAddress.addressTypeId = newData.addressTypeId;
      this.loading = true;
 
      this.companyAddressService.editCompanyAddress(this.companyAddress).subscribe((data: any) => {
        this.onCompanyAddressEditEvent.emit(this.companyAddress.id);
        if (data.status === true) {
          this.isFormValid = true;
          this.dialogRef.close();
          this.alertService.success("Employe Address edited successfully");
        }
        else {
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
  get f() { return this.companyAddressCreateForm.controls; }
}
