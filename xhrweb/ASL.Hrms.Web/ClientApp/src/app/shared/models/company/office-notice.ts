export class OfficeNotice {
  id: any;
  companyId: any;
  subject: string = "";
  details: string = "";
  isGeneral?: boolean ;
  isSectionSpecific?: boolean;
  applicableSections: string="";
  publishDate: any;
  startDate: any;
  endDate: any;
  isDeleted: boolean;
  isPublished?: boolean;
  public constructor(init?: Partial<OfficeNotice>) {
    Object.assign(this, init);
  }
}
