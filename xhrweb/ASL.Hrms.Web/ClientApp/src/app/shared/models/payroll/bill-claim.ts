export class BillClaim {
  id: any;
  benefitDeductionId?: any;
  employeeId: any;
  managerId:any
  billDate: string;
  claimDate: string;
  allocatedAmount: number;
  claimAmount: number;
  remarks: string;
  companyId: any;
  status: string;
  benefitBillClaimId: number;
  billAttachment: any;
  billAttachmentUri: string;
  branchIds: any;
  departmentIds: any;
  designationIds: any;
  positionName: string = "";
  companyEmployeeId: string = "";
  searchText: string = "";
  branchId: any;
  departmentId: any;
  monthCycleId:any;
  fullName:any;
  startTime:any;
  endTime:any;
  public constructor(init?: Partial<BillClaim>) {
    Object.assign(this, init);
  }
}
