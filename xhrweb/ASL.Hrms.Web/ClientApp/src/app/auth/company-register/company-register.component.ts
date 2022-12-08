import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, FormGroupDirective, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SharedService } from '../../shared/services/shared.service';
import { ErrorStateMatcher } from '@angular/material/core';
import { CompanyService, FileUploadService } from 'src/app/shared/services';
import { CompanyRegisterModel } from '../models/company-register';
import { Company } from 'src/app/shared/models';
import { AlertService } from 'src/app/shared/services/alert.service';
import { ThrowStmt } from '@angular/compiler';
import { RecaptchaErrorParameters } from 'ng-recaptcha';
import { AppSettingsService } from '../../../app/app-settings.service';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const invalidCtrl = !!(control && control.invalid && control.parent.dirty);
    const invalidParent = !!(control && control.parent && control.parent.invalid && control.parent.dirty);
    return (invalidCtrl || invalidParent);
    //return control.dirty && form.invalid;
  }
}

   
@Component({
  selector: 'app-company-register',
  templateUrl: './company-register.component.html',
  styleUrls: ['./company-register.component.css']
})
export class CompanyRegisterComponent implements OnInit {

  validDOB: any = new Date();

  registrationForm: FormGroup;
  matcher = new MyErrorStateMatcher();
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;
isCapthaChecked:boolean=false;
  returnUrl: any;
  genders: any[];
  hidePassword: boolean = true;
  hideConfirmPassword: boolean = true;
  isRegistrationCompleted: boolean = false;
  isRegistrationInvalid: boolean = false;
  fileToUpload: File;
  public formSubmitValid: boolean;
  errorMessage: string;

  imagePreviewPath: string = 'assets/images/avatar.png';
  //defaultImagePreviewPath: string = 'assets/images/avatar.png';

  company: Company= new Company();
  fileName: any;
  uploadedFiles: any;
  fileValidationError: any;
  registrationFormData: FormData = new FormData();
  siteKey: any;

  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private companyService: CompanyService,
    private fileUploadService: FileUploadService,
    private sharedService: SharedService,
    private appSettingsService: AppSettingsService,
    private alertService: AlertService) {
      this.siteKey = this.appSettingsService.siteKey;
    
  }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams.returnUrl;

    this.getGenders();

    this.registrationForm = this.fb.group({
      companyName: [this.company.companyName, [Validators.required, Validators.maxLength(250)]],
      companyLocalizedName: [this.company.companyLocalizedName, [Validators.maxLength(150)]],
      shortName: [this.company.shortName, [Validators.required, Validators.maxLength(50)]],
      companySlogan: [this.company.companySlogan, [Validators.maxLength(50)]],
      sortOrder: [this.company.sortOrder, [Validators.maxLength(50)]],
      licenseKey: [this.company.licenseKey, [Validators.maxLength(250)]],
      isActive: [this.company.isActive],
      userId: [this.company.userId],
      password: [this.company.password],
      EmployeFullName :[this.company.EmployeFullName, [Validators.required, Validators.maxLength(50)] ],
      confirmPassword: [''],
    }, { validator: this.checkPasswords });
  }
  getGenders() {
    this.genders = this.sharedService.getGenders();
  }

  async onSubmit() {
    this.isFormValid = false;
    //this.formSubmitAttempt = false;
    if (this.registrationForm.valid) {
      this.createCompany();
     

    }
  }

  createCompany() {
    this.company = new Company(this.registrationForm.value);
    this.loading = true;
    this.companyService.createCompany(this.company)
      .subscribe((data: any) => {
        if (data.status === true) {
          this.isRegistrationInvalid = false;
          this.formSubmitValid = true;   
          this.alertService.success("Company registration request done successfully. Please wait for admin approval.");
          this.router.navigate(['/']);
          
        }
        else {
          this.formSubmitValid = false;
          this.isRegistrationInvalid = true;
          this.errorMessage = data.message;
        }
        this.loading = false;
      },
        (error: any) => {
          this.isRegistrationInvalid = true;
          this.alertService.success(error);
          this.loading = false;

          //this.showErrorMessage(error);
        });
  }


  uploadExamImage(files) {
    this.fileValidationError = null;
    let fileInfo = this.fileUploadService.imageFileUpload(files);
    if (fileInfo.validationError != null) {
      this.fileValidationError = fileInfo.validationError;
      return;
    }
    this.fileName = fileInfo.fileName;
    this.uploadedFiles = fileInfo.formData;
    this.fileToUpload = <File>files[0];

    //this.registrationFormData.append('file', fileToUpload, fileToUpload.name);

    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.imagePreviewPath = event.target.result;
    }
    reader.readAsDataURL(files[0]);
  }

  checkPasswords(group: FormGroup) { // here we have the 'passwords' group
    let pass = group.controls.password.value;
    let confirmPass = group.controls.confirmPassword.value;
    return pass === confirmPass ? null : { notSamePassword: true }
  }

  get f() { return this.registrationForm.controls; }

  public resolved(captchaResponse: string): void {
    this.isCapthaChecked = true;
  }

  public onError(errorDetails: RecaptchaErrorParameters): void {
    this.isCapthaChecked = false;
  }
}

