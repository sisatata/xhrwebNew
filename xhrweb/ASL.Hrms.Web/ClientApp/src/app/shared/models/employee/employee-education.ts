export class EmployeeEducation{
  id: any;
  employeeId: any;
  companyId: any;
  educationalDegreeId: any;
  educationalInstituteId: any;
  session: string = "";
  passingYear: string = "";
  boardorUniversity: string = "";
  result :string = "";
  resultType: string = "";
  outOf?: number;

  public constructor(init?: Partial<EmployeeEducation>) {
    Object.assign(this, init);
  }
}
