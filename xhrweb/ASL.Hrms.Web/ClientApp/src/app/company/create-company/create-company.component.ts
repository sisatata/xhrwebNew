import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, FormGroupDirective, NgForm } from '@angular/forms';
import { ReplaySubject } from 'rxjs';
import { Company } from '../../shared/models';
import { CompanyService } from '../../shared/services';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ErrorStateMatcher } from '@angular/material/core';
import { AlertService } from 'src/app/shared/services/alert.service';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const invalidCtrl = !!(control && control.invalid && control.parent.dirty);
    const invalidParent = !!(control && control.parent && control.parent.invalid && control.parent.dirty);

    return (invalidCtrl || invalidParent);
    //return control.dirty && form.invalid;
  }
}

@Component({
  selector: 'app-create-company',
  templateUrl: './create-company.component.html',
  styleUrls: ['./create-company.component.css']
})


export class CreateCompanyComponent implements OnInit {
  isEditMode: boolean;
  // onCreate event
  onCompanyCreateEvent: EventEmitter<number> = new EventEmitter();
  // onEdit event;
  onCompanyEditEvent: EventEmitter<number> = new EventEmitter();

  companyForm: FormGroup
  submitted = false;
  company: Company;
  companyId: number;
  cities: any[];
  areas: any[];

  cityFilterCtrl: FormControl = new FormControl();
  filteredCities: ReplaySubject<any[]> = new ReplaySubject<any[]>(1);
  areaFilterCtrl: FormControl = new FormControl();
  filteredAreas: ReplaySubject<any[]> = new ReplaySubject<any[]>(1);

  hidePassword: boolean = true;
  hideConfirmPassword: boolean = true;
  matcher = new MyErrorStateMatcher();

  constructor(private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<CreateCompanyComponent>,
    private companyService: CompanyService,
    private alertService: AlertService,
    @Inject(MAT_DIALOG_DATA) data) {

    this.company = new Company();

    if (isNaN(data)) {
      this.company = new Company(data);
    }
    else { // coming from merchant page
      this.company = data;
      this.company.id = this.companyId;
     

    }
    if (this.company.id === undefined) {
      this.isEditMode = false;
    }
    else {
      this.isEditMode = true;
    }

    this.buildForm();
    this.addValidators();
  }

  ngOnInit() {
  }

  buildForm() {
    this.companyForm = this.formBuilder.group({
      companyName: [this.company.companyName, [Validators.required, Validators.maxLength(250)]],
      companyLocalizedName: [this.company.companyLocalizedName, [Validators.maxLength(150)]],
      shortName: [this.company.shortName, [Validators.required, Validators.maxLength(50)]],
      companySlogan: [this.company.companySlogan, [Validators.maxLength(50)]],
      sortOrder: [this.company.sortOrder, [Validators.maxLength(50)]],
      licenseKey: [this.company.licenseKey, [Validators.maxLength(250)]],
      isActive: [this.company.isActive],
      userId: [this.company.userId],
      password: [this.company.password],
      confirmPassword: [''],
    }, { validator: this.checkPasswords });
  }

  addValidators() {
    if (this.company.id === undefined) {
      this.companyForm.get('password').setValidators([Validators.required, Validators.minLength(6)]);
      this.companyForm.get('userId').setValidators([Validators.required]);

    } else {
      this.companyForm.get('password').setValidators(null);
      this.companyForm.get('userId').setValidators(null);
    }
    this.companyForm.updateValueAndValidity();
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.companyForm.invalid) {
      return;
    }
    if (this.company.id === undefined) {
      this.createCompany();
    }
    else {
      this.editCompany();
    }
    this.dialogRef.close();
  }

  createCompany() {
    this.company = new Company(this.companyForm.value);
    if (this.company !== null) {
      this.company.id = this.companyId;
    }
    this.companyService.createCompany(this.company)
      .subscribe((data: any) => {
        this.onCompanyCreateEvent.emit(this.company.id);
        this.alertService.success("Company added successfully. Please use credential to login again and get new company");
      },
        (error: any) => {
          this.showErrorMessage(error);
        });
  }

  editCompany() {
    let newData = new Company(this.companyForm.value);
    if (this.company !== null) {
      this.company.companyName = newData.companyName;
      this.company.companyLocalizedName = newData.companyLocalizedName;
      this.company.shortName = newData.shortName;
      this.company.companySlogan = newData.companySlogan;
      this.company.sortOrder = newData.sortOrder;
      this.company.licenseKey = newData.licenseKey;
      //this.company.address = newData.address;
      this.company.isActive = newData.isActive;
      this.company.companyWebsite = newData.companyWebsite;
      this.company.facebookLink = newData.facebookLink;
      this.company.id = newData.id;
      //  console.log(this.company);
      this.companyService.editCompany(this.company)
        .subscribe((data: any) => {
          this.onCompanyEditEvent.emit(this.company.id);
          //this.alertService.success("Company edited successfully");
        },
          (error: any) => {
            this.showErrorMessage(error);
          });

    }
  }


  close() {
    this.dialogRef.close();
  }

  showErrorMessage(error: any) {
    console.log(error);
    //this.alertService.error("Internal server error happen");
  }
  checkPasswords(group: FormGroup) { // here we have the 'passwords' group
    let pass = group.controls.password.value;
    let confirmPass = group.controls.confirmPassword.value;

    return pass === confirmPass ? null : { notSamePassword: true }
  }

  // convenience getter for easy access to form fields
  get f() { return this.companyForm.controls; }

}

