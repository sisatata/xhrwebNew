export class CompanyAddress {

  id: any;
  companyId: any;
  addressLine1: any;
  addressLine2: any;
  village: any;
  postOffice: any;
  thanaId: any;
  districtId: any;
  countryId: any;
  latitude: any;
  longitude: any;
  addressTypeId: any;
  isDeleted: any;
  addressTypeName: any;
  thanaName: any;
  districtName: any;
  countryName: any;

  public constructor(init?: Partial<CompanyAddress>) {
    Object.assign(this, init);
  }
}



