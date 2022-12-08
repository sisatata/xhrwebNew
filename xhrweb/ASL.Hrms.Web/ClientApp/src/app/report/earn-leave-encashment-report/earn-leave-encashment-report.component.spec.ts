import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EarnLeaveEncashmentReportComponent } from './earn-leave-encashment-report.component';

describe('EarnLeaveEncashmentReportComponent', () => {
  let component: EarnLeaveEncashmentReportComponent;
  let fixture: ComponentFixture<EarnLeaveEncashmentReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EarnLeaveEncashmentReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EarnLeaveEncashmentReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
