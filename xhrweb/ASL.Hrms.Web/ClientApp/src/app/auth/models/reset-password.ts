export class ResetPasswordModel {
    email: string;
    password: string;
    token: string;
  
    public constructor(email: string, password: string,token:string) {
      this.email = email;
      this.password = password;
      this.token = token;
    }
  }
  