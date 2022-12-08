import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { ForgotPasswordModel } from '../models/forgot-password';

import { AccountService } from 'src/app/auth/services/account.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})

export class ForgotPasswordComponent implements OnInit {
  form: FormGroup;
  public formSubmitValid: boolean;
  private formSubmitAttempt: boolean;
  private returnUrl: string;
  forgotPasswordModel: ForgotPasswordModel;
  hidePassword: boolean = true;
  errorMessage: string = '';
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService
  ) {
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      email: ['', Validators.email]
    });

  }

  onSubmit() {
    var email = this.form.get('email').value;

    this.forgotPasswordModel = new ForgotPasswordModel(email);

    if (this.form.valid) {
      try {
        this.loading = true;
        this.accountService.forgotPassword(this.forgotPasswordModel)
          .subscribe((data: any) => {
            
            if (data.success == true) {
              this.formSubmitValid = true;
              this.isFormValid = true;

            }
            else {
              this.formSubmitValid = false;
              this.errorMessage = data.message;
              this.isFormValid = false;
              
            }
            this.loading = false;

          },
            (error: any) => {
              this.formSubmitValid = false;
              this.loading = false;
            })
      } catch (err) {
        this.formSubmitValid = false;
        this.loading = false;

      }
    } else {
      this.formSubmitAttempt = true;
      this.loading = false;

    }

  }

}

