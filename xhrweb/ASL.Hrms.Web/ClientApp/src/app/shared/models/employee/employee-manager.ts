export class EmployeeManager {
  id?: any;
  employeeId: any;
  managerId?: any;
  managerName: string = "";
  isPrimaryManager: boolean=false;
  assignedBy?: any;
  assignDate?: any ;
  unAssignedBy?: any;
  unAssignDate?: any;
  isDeleted: boolean;
  companyId?: any ;
  managerTypeId?: any;
  managerTypeName: string = "";
  public constructor(init?: Partial<EmployeeManager>){
    Object.assign(this, init);
  }
}
