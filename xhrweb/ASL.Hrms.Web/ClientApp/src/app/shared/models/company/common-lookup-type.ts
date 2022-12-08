import { Guid } from "..";

export class CommonLookUpType {
  id: any;
  companyId: any = Guid.empty;
  parentId?: any;
  shortCode: string = "";
  lookUpTypeName: string = "";
  lookUpTypeNameLC: string = "";
  remarks: string = "";
  sortOrder: number = 1 ;
  public constructor(init?: Partial<CommonLookUpType>) {
    Object.assign(this, init);
  }

}
