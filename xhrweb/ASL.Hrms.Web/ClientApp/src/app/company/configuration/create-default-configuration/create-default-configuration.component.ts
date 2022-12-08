
import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CompanyService, BranchService, ConfigurationService } from 'src/app/shared/services';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Configuration } from '../../../shared/models';
import { AlertService } from '../../../shared/services/alert.service';
@Component({
  selector: 'app-create-default-configuration',
  templateUrl: './create-default-configuration.component.html',
  styleUrls: ['./create-default-configuration.component.css']
})
export class CreateDefaultConfigurationComponent implements OnInit {
  onConfigurationCreateEvent: EventEmitter<any> = new EventEmitter();
  onConfigurationEditEvent: EventEmitter<any> = new EventEmitter();
  configurationCreateForm: FormGroup
  submitted = false;
  isEditMode = false;
  branchId: any;
  companies: any;
  branches: any;
  companyId: any = localStorage.getItem('companyId');
  configuration: Configuration;
  configurationId: number;
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;

  constructor(
    private companyService: CompanyService,
    private alertService:AlertService,
    private dialogRef: MatDialogRef<CreateDefaultConfigurationComponent>,
    private formBuilder: FormBuilder,
    private branchService: BranchService,
    private configurationService: ConfigurationService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.configuration = new Configuration();
    if (isNaN(data)) {
      this.configuration = new Configuration(data);
      this.companyId = this.configuration.companyId;
     
    } else {
      
    }
    if (this.configuration.id === undefined) {
      this.isEditMode = false;
    }
    else {
      this.isEditMode = true;
    }
    
    this.buildForm();
    this.getAllBranchByCompanyId(this.companyId);
  }
  ngOnInit() {
    this.getAllCompanies();
  }
  onChangeCompany() {
    this.companyId = this.f.companyId.value;
    if (this.companyId) {
      this.getAllBranchByCompanyId(this.companyId);
    }
  }
  onChangeBranch() {
    this.branchId = this.f.branchId.value;
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  getAllBranchByCompanyId(companyId) {
    this.branchService.getAllBranchByCompanyId(companyId).subscribe((result: any) => {
      this.branches = result;
    });
  }
  buildForm() {
    this.configurationCreateForm = this.formBuilder.group({
      companyId: [this.configuration.companyId, [Validators.required]],
      code: [this.configuration.code, [Validators.required, Validators.maxLength(250)]],
      defaultValue: [this.configuration.defaultValue, [Validators.required, Validators.maxLength(250)]],
      description: [this.configuration.description, [Validators.required,Validators.maxLength(150)]],
    });
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.configurationCreateForm.invalid) {
      return;
    }
    if (this.configuration.id === undefined) {
      this.createConfiguration();
    }
    else  {
      this.editConfiguration();
    }
    this.dialogRef.close();
  }
  createConfiguration() {
    this.configuration = new Configuration(this.configurationCreateForm.value);
    this.loading = true;
    this.configurationService.createConfiguration(this.configuration).subscribe((data: any) => {
      this.onConfigurationCreateEvent.emit(this.configuration.id);
      if((data as any).status === true){
        this.alertService.success("Configuration added successfully");
        this.isFormValid = true;
       }
       else{
         this.isFormValid = false;
         this.errorMessages = (data as any).message;
       }
       this.loading =false;
     
    }, (error: any) => {
     this.alertService.error(error);
     this.loading =false;
    });
  }
  editConfiguration() {
    
    let newData = new Configuration(this.configurationCreateForm.value);
    if (this.configuration !== null) {
      this.configuration.companyId = newData.companyId;
      this.configuration.code = newData.code;
      this.configuration.defaultValue = newData.defaultValue;
      this.configuration.description = newData.description;
      this.loading = true;
      this.configurationService.editConfiguration(this.configuration).subscribe((data: any) => {
        this.onConfigurationEditEvent.emit(this.configuration.id)
       if((data as any).status === true){
        this.alertService.success("Configuration updated successfully");
        this.isFormValid = true;
       }
       else{
         this.isFormValid = false;
         this.errorMessages = (data as any).message;
       }
       this.loading =false;
      }, (error: any) => {
       this.alertService.error(error);
       this.loading =false;
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
