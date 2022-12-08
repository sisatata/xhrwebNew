import { Component, EventEmitter, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CompanyPhone } from 'src/app/shared/models';
import { CommonLookUpTypeService, CompanyPhoneService, CompanyService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
  selector: 'app-create-company-phone',
  templateUrl: './create-company-phone.component.html',
  styleUrls: ['./create-company-phone.component.css']
})
export class CreateCompanyPhoneComponent implements OnInit {
  onCompanyPhoneCreateEvent: EventEmitter<any> = new EventEmitter();
  onCompanyPhoneEditEvent: EventEmitter<any> = new EventEmitter();

  companyPhoneCreateForm: FormGroup
  submitted = false;
  companyId: any;
  companies: any = [];
  thanas: any = [];
  districts: any = [];
  countries: any = [];
  phoneTypes: any = [];
  companyPhone: CompanyPhone;
  companyPhoneId: number;
  isEditMode = false;
  phoneNumber:any;
  phoneTypeId:any;
  isFormValid: boolean ;
  errorMessages: any ;
  loading: boolean = false;
  constructor( private dialogRef: MatDialogRef<CreateCompanyPhoneComponent>,
    private commonLookUpTypeService: CommonLookUpTypeService,
    private formBuilder: FormBuilder,
    private companyPhoneservice: CompanyPhoneService,
   // private commonLookUpTypeService: CommonLookUpTypeService,
    private companyService : CompanyService,
    private alertService: AlertService,
    @Inject(MAT_DIALOG_DATA) data
    ) { 

      this.companyPhone = new CompanyPhone();
      if (isNaN(data)) {
        this.companyPhone = new CompanyPhone(data);
        this.companyId = this.companyPhone.companyId;
      }
  
      if (this.companyPhone.id) {
        this.isEditMode = true;
      }
      else {
        this.isEditMode = false;
      }
      this.buildForm();
    }

  ngOnInit(): void {
    this.buildForm();
  
  }
  buildForm() {
    this.companyPhoneCreateForm = this.formBuilder.group({
      companyId:[this.companyId,[Validators.required]],
      phoneNumber: [this.companyPhone.phoneNumber, [Validators.required, Validators.maxLength(150), Validators.pattern("^[0-9]+")]],
      phoneTypeId: [this.companyPhone.phoneTypeId]
    });
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }

  onChangeCompany() {
    this.companyId = this.f.companyId.value;
    this.getAllPhoneTypes();
  }

  getAllPhoneTypes() {
    if (this.companyId) {
      this.commonLookUpTypeService.getCommonLookUpTypeByParentCode(this.companyId, "PhoneType").subscribe(result => {
        this.phoneTypes = result
      });
    }
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.companyPhoneCreateForm.invalid) {
      return;
    }
    if (this.companyPhone.id === undefined) {
      this.createCompanyPhone();
    }
    else {
      this.editCompanyPhone();
    }
   // this.dialogRef.close();
  }

  createCompanyPhone() {
    this.companyPhone = new CompanyPhone(this.companyPhoneCreateForm.value);

    //this.companyPhone.companyId = this.companyId;
    this.loading = true;
    this.companyPhoneservice.createCompanyPhone(this.companyPhone).subscribe((data: any) => {
      this.onCompanyPhoneCreateEvent.emit(this.companyPhone.id);

      if(data.status === true){
         this.alertService.success("Company Phone added successfully");
         this.isFormValid = true;
         this.dialogRef.close();
      }
      else{
        this.companyPhoneCreateForm.controls['phoneNumber'].setErrors({'incorrect': true});

      //  this.isFormValid = false;
        //this.errorMessages = data.message;
      
      }
     this.loading = false;
    }, (error: any) => {
      this.showErrorMessage(error);
      this.loading = false;
    });
  }


  editCompanyPhone() {
    let newData = new CompanyPhone(this.companyPhoneCreateForm.value);
    
    if (this.companyPhone !== null) {
      this.companyPhone.phoneNumber = newData.phoneNumber;
      this.companyPhone.phoneTypeId = newData.phoneTypeId;
      this.loading = true;
      this.companyPhoneservice.editCompanyPhone(this.companyPhone).subscribe((data: any) => {
        this.onCompanyPhoneEditEvent.emit(this.companyPhone.id)
        if(data.status === true){
          this.alertService.success("Company Phone edited successfully");
          this.isFormValid = true;
          this.dialogRef.close();
       }
       else{
        this.companyPhoneCreateForm.controls['phoneNumber'].setErrors({'incorrect': true});

        // this.isFormValid = false;
        // this.errorMessages = data.message;
       }
       this.loading = false;
      }, (error: any) => {
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
  get f() { return this.companyPhoneCreateForm.controls; }

}
