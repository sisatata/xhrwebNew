import { Guid } from "../guid";

export class EmployeeFamilyMember {
  id: any;
  employeeId: any;
  companyId: any;
  familiyMemberName: string = "";
  dateOfBirth: any;
  educationClass: string = "";
  educationalInstitute: string = "";
  educationYear :string = "";
  relationTypeId: any ;
  relationTypeName: string = "";
  isDependant: boolean = false;
  isEligibleForMedical: boolean = false;
  isEligibleForEducation: boolean = false;

  public constructor(init?: Partial<EmployeeFamilyMember>) {
    Object.assign(this, init);
  }
}
