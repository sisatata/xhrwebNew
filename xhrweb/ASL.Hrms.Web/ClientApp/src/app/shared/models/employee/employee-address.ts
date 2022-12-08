export class EmployeeAddress {
  id: any;
  employeeAddressId: any; // Addded for edit api
  employeeId: any;
  addressLine1: string = "";
  addressLine2: string = "";
  village: string = "";
  postOffice :string = "";
  thanaId: any;
  thanaName: string = "";
  districtId: any;
  districtName: string = "";
  countryId: any;
  countryName: string = "";
  latitude?: number;
  longitude?: number;
  addressTypeId: any;
  addressTypeName: string="";
  public constructor(init?: Partial<EmployeeAddress>) {
    Object.assign(this, init);
  }
}
