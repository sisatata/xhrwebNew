import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-company-other-info-base',
  templateUrl: './company-other-info-base.component.html',
  styleUrls: ['./company-other-info-base.component.css']
})
export class CompanyOtherInfoBaseComponent implements OnInit {
  @Input() companyName: any;
  @Input() companyId: any;
  constructor() { }

  ngOnInit(): void {
  }

}
