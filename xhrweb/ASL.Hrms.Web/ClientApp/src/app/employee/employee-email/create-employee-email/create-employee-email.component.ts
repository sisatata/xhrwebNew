import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeEmail } from 'src/app/shared/models';
import { EmployeeEmailService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';

@Component({
  selector: 'app-create-employee-email',
  templateUrl: './create-employee-email.component.html',
  styleUrls: ['./create-employee-email.component.css']
})

export class CreateEmployeeEmailComponent implements OnInit {
  onEmployeeEmailCreateEvent: EventEmitter<any> = new EventEmitter();
  onEmployeeEmailEditEvent: EventEmitter<any> = new EventEmitter();

  employeeEmailCreateForm: FormGroup
  submitted = false;
  employeeId: any;
  companies: any = [];
  thanas: any = [];
  districts: any = [];
  countries: any = [];
  addressTypes: any = [];
  employeeEmail: EmployeeEmail;
  employeeEmailId: number;
  isEditMode = false;
  isFormValid: boolean;
  errorMessages: any; // errorMessages: any = ['error 1', 'error 2', 'error 3'];
  constructor(
    private dialogRef: MatDialogRef<CreateEmployeeEmailComponent>,
    private formBuilder: FormBuilder,
    private employeeEmailservice: EmployeeEmailService,
    private alertService: AlertService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.employeeEmail = new EmployeeEmail();
    if (isNaN(data)) {
      this.employeeEmail = new EmployeeEmail(data);
      this.employeeId = this.employeeEmail.employeeId;

    }

    if (this.employeeEmail.id) {
      this.isEditMode = true;
    }
    else {
      this.isEditMode = false;
    }
    this.buildForm();
  }

  ngOnInit() {
  }


  buildForm() {
    this.employeeEmailCreateForm = this.formBuilder.group({
      emailAddress: [this.employeeEmail.emailAddress, [Validators.required, Validators.maxLength(150), Validators.email]],
      isPrimary: [this.employeeEmail.isPrimary]
    });
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.employeeEmailCreateForm.invalid) {
      return;
    }
    if (this.employeeEmail.id === undefined) {
      this.createEmployeeEmail();
    }
    else {
      this.editEmployeeEmail();
    }
    this.dialogRef.close();
  }

  createEmployeeEmail() {
    this.employeeEmail = new EmployeeEmail(this.employeeEmailCreateForm.value);

    this.employeeEmail.employeeId = this.employeeId;

    this.employeeEmailservice.createEmployeeEmail(this.employeeEmail).subscribe((data: any) => {
      this.onEmployeeEmailCreateEvent.emit(this.employeeEmail.employeeId);
       if(data.status === true){
        
        this.alertService.success("Employee Email added successfully");
       }
       else{
        this.isFormValid = false;
        this.errorMessages = data.message;
       }
     
    }, (error: any) => {
      this.alertService.error(error);

      this.showErrorMessage(error);
    });
  }


  editEmployeeEmail() {
    let newData = new EmployeeEmail(this.employeeEmailCreateForm.value);
    if (this.employeeEmail !== null) {
      this.employeeEmail.emailAddress = newData.emailAddress;
      this.employeeEmail.isPrimary = newData.isPrimary;

      this.employeeEmailservice.editEmployeeEmail(this.employeeEmail).subscribe((data: any) => {
        this.onEmployeeEmailEditEvent.emit(this.employeeEmail.id)
        if(data.status === true){
        
          this.alertService.success("Employee Email updated successfully");
         }
         else{
          this.isFormValid = false;
          this.errorMessages = data.message;
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
  get f() { return this.employeeEmailCreateForm.controls; }
}
