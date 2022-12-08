export class EmployeePromotionTransfer{
    id:any;
    employeeId:any;
    previousCompanyId:any;
    previousBranchId:any;
    previousDepartmentId:any;
    previousPositionId:any;
    previousPositionName: string ='';
    newPositionName:string = '';
    newCompanyId:any;
    newBranchId:any;
    newDepartmentId:any;
    newPositionId:any;
    proposedDate:any;
    startDate:any;
    endDate:any;
    previousGross:number;
    newGross:number;
    previousBasic:number;
    newBasic:number;
    previousHouserent:number;
    newHouserent:number;
    incrementTypeId:number;
    incrementValue:number;
    incrementAmount:number;
    incrementOn:number;
    reason:string = '';
    reference:string = '';
    approveDate:any;
    approveBy:string = '';
    approveNote:string = '';
    approvalStatus:string = ''; 
    incrementValueTypeId:any;
    previousGradeId:any;
    newGradeId:any;
    previousSalaryStructureId:any;
    newSalaryStructureId:any;
   previousPaymentOptionId:number;
   newPaymentOptionId:number;
   previousGradeName:string='';
   previousStructureName:string='';
   previousOptionName:string='';
   newGradeName:string='';
   newStructureName:string='';
   newOptionName:string='';
    public constructor(init?: Partial<EmployeePromotionTransfer>) {
      Object.assign(this, init);
    }
  }
  