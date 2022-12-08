import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginModel } from '../models/Login';

import { AuthService } from 'src/app/auth/services/auth.service';
import { EventEmitterService } from 'src/app/auth/services/event-emitter.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})


export class LoginComponent implements OnInit {
  form: FormGroup;
  public loginInvalid: boolean;
  private formSubmitAttempt: boolean;
  private returnUrl: string;
  loginModel: LoginModel;
  loading: boolean = false;
  hidePassword: boolean = true;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {
  }

  async ngOnInit() {
   // this.returnUrl = this.route.snapshot.queryParams.returnUrl;
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
 
  
    this.form = this.fb.group({
      email: ['', Validators.email],
      password: ['', Validators.required]
    });

    if (await this.authService.checkAuthenticated()) {
      if (this.returnUrl)
        await this.router.navigate([this.returnUrl]);     
    }
  }

  async onSubmit() {
    var email = this.form.get('email').value;
    var password = this.form.get('password').value;

    this.loginModel = new LoginModel(email, password);
    if (this.form.valid) {
      try {
       this.loading = true;
       debugger
        this.authService.login(this.loginModel)
    
          .subscribe((data: any) => {
            localStorage.setItem('userToken', data.bearerToken);
            localStorage.setItem("userId", data.userId);
            localStorage.setItem("loginId", data.loginId);
            localStorage.setItem("displayName", data.displayName);
            localStorage.setItem("email", data.email);
            localStorage.setItem("phone", data.phone);
            localStorage.setItem("companyId", data.companyId);
            localStorage.setItem("companyName", data.companyName);
            localStorage.setItem("phone", data.phone);
            localStorage.setItem("employeeId", data.id);
            localStorage.setItem("userRoles", data.userRoles);
            localStorage.setItem("profilePictureUri", data.employeeImageUri);

            localStorage.setItem('currentUser', JSON.stringify(data));
      
            this.authService.loginCallback();
         
            this.router.navigateByUrl(this.returnUrl, { skipLocationChange: false }).then(() => {
            
              this.loading = false;
              
            });
          },
            (error: any) => {
              this.loginInvalid = true;
              this.loading = false;
            })
            
      } catch (err) {
        this.loginInvalid = true;
        this.loading = false;
      }
    } else {
      this.formSubmitAttempt = true;
      this.loading = false;
    }

  }
}
