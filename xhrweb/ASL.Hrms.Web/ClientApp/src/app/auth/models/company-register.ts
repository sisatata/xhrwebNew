// export class CompanyRegisterModel {
//     email: string;
  
//     public constructor(email: string) {
//       this.email = email;
//     }
//   }
  
  export class CompanyRegisterModel {
    id: any;
    companyName: number;
    shortName: number;
    companyLocalizedName: any;
    companySlogan: any;
    licenseKey: any;
    sortOrder: any = 1;
    isActive: boolean=false;
    userId: string = "";
    password: string = "";
    public constructor(init?: Partial<CompanyRegisterModel>) {
      Object.assign(this, init);
    }
  }
  