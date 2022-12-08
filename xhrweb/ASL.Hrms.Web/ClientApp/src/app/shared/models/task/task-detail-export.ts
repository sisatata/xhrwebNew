import { TaskDetailService } from "../../services";

export class TaskDetailExport {
  startDate: any;
  endDate: any;
  employeeId: any;
  userId: any;

  public constructor(init?: Partial<TaskDetailExport>) {
    Object.assign(this, init);
  }
}
