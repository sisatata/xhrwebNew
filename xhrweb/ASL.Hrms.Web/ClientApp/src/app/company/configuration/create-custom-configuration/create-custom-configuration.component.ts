import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CompanyService, BranchService, CustomConfigurationService } from 'src/app/shared/services';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Configuration, Guid } from '../../../shared/models';
import { AlertService } from '../../../shared/services/alert.service';
import { AuthService } from 'src/app/auth/services/auth.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-create-custom-configuration',
  templateUrl: './create-custom-configuration.component.html',
  styleUrls: ['./create-custom-configuration.component.css']
})

export class CreateCustomConfigurationComponent implements OnInit {

  onCustomConfigurationCreateEvent: EventEmitter<any> = new EventEmitter();
  onCustomConfigurationEditEvent: EventEmitter<any> = new EventEmitter();
  configurationCreateForm: FormGroup
  submitted = false;
  isEditMode = false;
  branchId: any;
  companies: any;
  branches: any;
  companyId: any;
  configuration: Configuration;
  configurationId: number;
defaultValue:any;
code:any;
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;
  isSystemAdmin: any;
  constructor(
    private companyService: CompanyService,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateCustomConfigurationComponent>,
    private formBuilder: FormBuilder,
    private branchService: BranchService,
     private authService: AuthService,
     private datePipe: DatePipe,
    private configurationService: CustomConfigurationService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.configuration = new Configuration();
    if (isNaN(data)) {
      this.configuration = new Configuration(data);
      this.companyId = this.configuration.companyId;

    } else {

    }


    if (this.configuration.customConfigurationId === undefined
      || this.configuration.customConfigurationId == Guid.empty) {
      this.isEditMode = false;
    }
    else {
      this.isEditMode = true;
      this.code = this.configuration.code;
      this.defaultValue = this.configuration.defaultValue;
    }
    this.isSystemAdmin = this.authService.isSystemAdmin;
   // console.log(this.isSystemAdmin);
    this.buildForm();
  }

  ngOnInit() {
    this.getAllCompanies();
    // this.f.code.setValue({readOnly:true});
  }

  onChangeCompany() {
    this.companyId = this.f.companyId.value;
  }


  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }


  buildForm() {
    this.configurationCreateForm = this.formBuilder.group({
      companyId: [this.configuration.companyId, [Validators.required]],
      code: [{value:this.configuration.code, disabled: true}, [Validators.required, Validators.maxLength(250)]],
      defaultValue: [{value:this.configuration.defaultValue, disabled: true} ,[Validators.required, Validators.maxLength(250)]],
      customValue: [this.configuration.customValue, [Validators.required]],
      description: [{value:this.configuration.description, disabled:true}, [Validators.required, Validators.maxLength(150)]],
      startDate: [this.configuration.startDate,[Validators.required]],
      endDate: [this.configuration.endDate,[Validators.required]]
    });
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.configurationCreateForm.invalid) {
      return;
    }
    if (this.configuration.customConfigurationId === undefined
      || this.configuration.customConfigurationId == Guid.empty || this.configuration.customConfigurationId === null) {
      this.createCustomConfiguration();
    }
    else {
      this.editCustomConfiguration();
    }
    this.dialogRef.close();
  }


  createCustomConfiguration() {
    this.configuration = new Configuration(this.configurationCreateForm.value);
    this.loading = true;
    this.configuration.code = this.code;
    this.configuration.defaultValue = this.defaultValue;
    this.configuration.startDate =  this.datePipe.transform(new Date( this.configuration.startDate), 'yyyy-MM-dd');
    this.configuration.endDate = this.datePipe.transform(new Date( this.configuration.endDate), 'yyyy-MM-dd');
    this.configurationService.createConfiguration(this.configuration).subscribe((data: any) => {
      this.onCustomConfigurationCreateEvent.emit(this.configuration.customConfigurationId);
     if((data as any).status === true){
        this.isFormValid = true;
        this.alertService.success("Custom Configuration added successfully");

     }
     else{
       this.isFormValid = false;
       this.errorMessages = (data as any).message;
      
     }
     this.loading = false;

    }, (error: any) => {
      this.alertService.error(error);
      this.loading = false;

    });
  }

  editCustomConfiguration() {

    let newData = new Configuration(this.configurationCreateForm.value);
     newData.code = this.code;
     newData.defaultValue = this.defaultValue;
    if (this.configuration !== null) {
      // id is already assigned on configuration
      this.configuration.companyId = newData.companyId;
      this.configuration.code = newData.code;
      this.configuration.defaultValue = newData.defaultValue;
      this.configuration.customValue = newData.customValue;
      this.configuration.description = newData.description;
      this.configuration.startDate =  this.datePipe.transform(new Date(newData.startDate), 'yyyy-MM-dd');
      this.configuration.endDate = this.datePipe.transform(new Date(newData.endDate), 'yyyy-MM-dd');
         this.loading = true;
      
      this.configurationService.editConfiguration(this.configuration).subscribe((data: any) => {
        this.onCustomConfigurationEditEvent.emit(this.configuration.customConfigurationId);
         if((data as any).status === true){
           this.isFormValid = true;
           this.alertService.success("Custom CustomConfiguration updated successfully");

         }
         else{
               this.isFormValid = false;
               this.errorMessages = (data as any).message
         }
         this.loading = false;

      }, (error: any) => {
        this.alertService.error(error);
        this.loading = false;

      });

    }
  }


  close() {
    this.dialogRef.close();
  }
  showErrorMessage(error: any) {
    console.log(error);
    this.alertService.error("Internal server error happen");
  }
  get f() { return this.configurationCreateForm.controls; }
}