import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeEnrollReportComponent } from './employee-enroll-report.component';

describe('EmployeeEnrollReportComponent', () => {
  let component: EmployeeEnrollReportComponent;
  let fixture: ComponentFixture<EmployeeEnrollReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeEnrollReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeEnrollReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
