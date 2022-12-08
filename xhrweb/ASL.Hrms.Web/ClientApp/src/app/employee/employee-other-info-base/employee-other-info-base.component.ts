import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';

@Component({
  selector: 'app-employee-other-info-base',
  templateUrl: './employee-other-info-base.component.html',
  styleUrls: ['./employee-other-info-base.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class EmployeeOtherInfoBaseComponent implements OnInit {
  @Input() employeeId: any;
  @Input() employeeFullName: any;
  @BlockUI('employee-other-info-section') blockUI: NgBlockUI;
  constructor() { }

  ngOnInit() {

  }

}
