import { Component, EventEmitter, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CompanyEmail } from 'src/app/shared/models';
import { CompanyEmailService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
  selector: 'app-create-company-email',
  templateUrl: './create-company-email.component.html',
  styleUrls: ['./create-company-email.component.css']
})
export class CreateCompanyEmailComponent implements OnInit {
  onCompanyEmailCreateEvent: EventEmitter<any> = new EventEmitter();
  onCompanyEmailEditEvent: EventEmitter<any> = new EventEmitter();
  companyEmailCreateForm: FormGroup
  submitted = false;
  companyId: any;
  companies: any = [];
  emailAddress:any;
  employeeEmailId: number;
  isEditMode = false;
  companyEmail: CompanyEmail;
  isPrimary: boolean ;
  isFormValid: boolean;
  errorMessages: any ;
  loading:boolean = false;
  constructor(private dialogRef: MatDialogRef<CreateCompanyEmailComponent>,
    private formBuilder: FormBuilder,
    private companyEmailservice: CompanyEmailService,
    private alertService: AlertService,
    @Inject(MAT_DIALOG_DATA) data) { 
      if (isNaN(data)) {
        this.companyEmail = new CompanyEmail(data);
        this.companyId = this.companyEmail.companyId;
       
      
    }
    if (this.companyEmail.id) {
      this.isEditMode = true;
    }
    else {
      this.isEditMode = false;
    }
    this.buildForm();

  }
  ngOnInit(): void {
  }

  buildForm() {
    this.companyEmailCreateForm = this.formBuilder.group({
      emailAddress: [this.companyEmail.emailAddress, [Validators.required, Validators.maxLength(150), Validators.email]],
      isPrimary: [this.companyEmail.isPrimary]
    });
  }
  
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.companyEmailCreateForm.invalid) {
      return;
    }
    if (this.companyEmail.id === undefined) {
      this.createCompanyEmail();
    }
    else {
      this.editCompanyEmail();
    }
   // this.dialogRef.close();
  }
  createCompanyEmail() {
    this.companyEmail = new CompanyEmail(this.companyEmailCreateForm.value);

    this.companyEmail.companyId = this.companyId;
    this.loading = true;
    this.companyEmailservice.createCompanyEmail(this.companyEmail).subscribe((data: any) => {
      this.onCompanyEmailCreateEvent.emit(this.companyEmail.id);
      if(data.status === true){
        this.alertService.success("Employee Email added successfully");
        this.isFormValid = true;
        this.dialogRef.close();
      }else{
        this.isFormValid = false;
        this.errorMessages = data.message;
      }
      this.loading = false;
    }, (error: any) => {
      this.alertService.error(error);
      this.loading = false;
      this.showErrorMessage(error);
    });
  }
  editCompanyEmail() {
    let newData = new CompanyEmail(this.companyEmailCreateForm.value);
    if (this.companyEmail !== null) {
      this.companyEmail.emailAddress = newData.emailAddress;
      this.companyEmail.isPrimary = newData.isPrimary;
       this.loading = true;
      this.companyEmailservice.editCompanyEmail(this.companyEmail).subscribe((data: any) => {
      
        this.onCompanyEmailEditEvent.emit(this.companyEmail.id)
        if(data.status === true){
          this.alertService.success("Employee Email edited successfully");
          this.isFormValid = true;
          this.dialogRef.close();
        }else{
          this.isFormValid = false;
          this.errorMessages = data.message;
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
  get f() { return this.companyEmailCreateForm.controls; }


}
