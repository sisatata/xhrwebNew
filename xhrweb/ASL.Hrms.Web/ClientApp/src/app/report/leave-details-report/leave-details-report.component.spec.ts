import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveDetailsReportComponent } from './leave-details-report.component';

describe('LeaveDetailsReportComponent', () => {
  let component: LeaveDetailsReportComponent;
  let fixture: ComponentFixture<LeaveDetailsReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeaveDetailsReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaveDetailsReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
