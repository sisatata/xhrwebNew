import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, FormGroupDirective, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ErrorStateMatcher } from '@angular/material/core';
import { ResetPasswordModel } from '../models/reset-password';
import { AccountService } from '../services/account.service';


export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const invalidCtrl = !!(control && control.invalid && control.parent.dirty);
    const invalidParent = !!(control && control.parent && control.parent.invalid && control.parent.dirty);

    return (invalidCtrl || invalidParent);
    //return control.dirty && form.invalid;
  }
}


@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})

export class ResetPasswordComponent implements OnInit {
  form: FormGroup;
  public formSubmitValid: boolean;
  private formSubmitAttempt: boolean;
  private returnUrl: string;
  resetPasswordModel: ResetPasswordModel;
  hidePassword: boolean = true;
  hideConfirmPassword: boolean = true;
  errorMessage: string = '';
  matcher = new MyErrorStateMatcher();
  email: any;
  token: any;
  submitted = false;
  errorMessages: any[] = [];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService
  ) {
  }

  ngOnInit(): void {

    this.route.queryParams.subscribe(params => {
      this.email = params['email'];
      this.token = params['token'];
    });

    //this.email = this.route.snapshot.params.email;
    //this.token = this.route.snapshot.params.token;

    this.form = this.fb.group({
      email: [this.email, [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]]
    }, { validator: this.checkPasswords });

  }

  checkPasswords(group: FormGroup) { // here we have the 'passwords' group
    let pass = group.controls.password.value;
    let confirmPass = group.controls.confirmPassword.value;

    return pass === confirmPass ? null : { notSamePassword: true }
  }


  onSubmit() {
    this.submitted = true;
    var password = this.form.get('password').value;
    this.resetPasswordModel = new ResetPasswordModel(this.email, password, this.token);

    if (this.form.valid) {
      try {

        this.accountService.resetPassword(this.resetPasswordModel)
          .subscribe((data: any) => {
            if (data.succeeded == true) {
              this.formSubmitValid = true;
            }
            else {
              this.formSubmitValid = false;
              this.errorMessages = data.messages;
            }
          },
            (error: any) => {
              this.formSubmitValid = false;
            })
      } catch (err) {
        this.formSubmitValid = false;
      }
    } else {
      this.formSubmitAttempt = true;
    }
  }
}
