import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder,  FormControl, FormGroup, FormGroupDirective, NgForm, ValidatorFn, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ChangePassword } from 'src/app/shared/models';
import { AlertService } from 'src/app/shared/services/alert.service';
import { AccountService } from '../services/account.service';

export function MustMatch(controlName: string, matchingControlName: string) {
  return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];
      
      if (matchingControl.errors && !matchingControl.errors.mustMatch) {
          // return if another validator has already found an error on the matchingControl
          return;
      }
 ;
 
      // set error on matchingControl if validation fails
      if (control.value !== matchingControl.value) {
       
          matchingControl.setErrors({ mustMatch: true });
      } else {
          matchingControl.setErrors(null);
      }
  }
}

export function passwordValidator(): ValidatorFn {  
  return (control: AbstractControl): { [key: string]: any } => {  
    if (control.value !== undefined  && control.value.match(/^[0-9a-zA-Z]+$/)) {
     
      return { 'hasNonAlphaNumeric': true };
  }
  return null;
  };  
}  

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const invalidCtrl = !!(control && control.invalid && control.parent.dirty);
    const invalidParent = !!(control && control.parent && control.parent.invalid && control.parent.dirty);

    return (invalidCtrl || invalidParent);
  }
}
@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  changePasswordForm: FormGroup;
  matcher = new MyErrorStateMatcher();
  changePassword:ChangePassword = new ChangePassword();
  submitted = false;
  isFormValid: boolean = true;
  confirmPassword:string='';
  errorMessages: any;
  loading:boolean = false;
  email:string = localStorage.getItem('email');
  constructor(
    private dialogRef: MatDialogRef<ChangePasswordComponent>,
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private alertService:AlertService
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }
  buildForm():void{
    this.changePasswordForm = this.formBuilder.group({
     email:[this.email,[Validators.required]],
     currentPassword:[this.changePassword.currentPassword,[Validators.required]],
     newPassword:[this.changePassword.newPassword,[Validators.required, Validators.maxLength(50),passwordValidator()]],
     confirmPassword:[this.confirmPassword,[Validators.required]]
      
    },{validator:MustMatch('newPassword', 'confirmPassword') });
  }
  checkPasswords(group: FormGroup) { 
    let pass = group.controls.newPassword.value;
    let confirmPass = group.controls.confirmPassword.value;
    return pass === confirmPass ? null : { notSame: true }
  }
  passwordConfirming(c: AbstractControl): { invalid: boolean } {
    if (c.get('newPassword').value !== c.get('confirmPassword').value) {
        return {invalid: true};
    }
}

  onSubmit():void{
    this.submitted = true;
    //console.log(this.changePasswordForm);
    if(this.changePasswordForm.invalid){
      return;
    }
    this.changeCurrentPassword();
  }

  changeCurrentPassword():void{
    this.loading = true;
    this.changePassword = new ChangePassword(this.changePasswordForm.value);
    this.accountService.changePassword(this.changePassword).subscribe(res=>{

                this.loading = false;
                if((res as any).success === true ){
                  this.isFormValid = true;
                  this.dialogRef.close();
                  this.alertService.success('Password Changes Successfully');
                  
                }
                else{
                  this.isFormValid = false;
                  this.errorMessages = (res as any).message;
                  
                }
    },()=>{
      this.loading = false;
    })
  }
  close():void{
    this.dialogRef.close();
  }

  changePasswordEvent(data:any):void{
  // console.log(data);
  }
  
  get f() { return this.changePasswordForm.controls; }
}
