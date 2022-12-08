import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PaySlipReportComponent } from './pay-slip-report.component';

describe('PaySlipReportComponent', () => {
  let component: PaySlipReportComponent;
  let fixture: ComponentFixture<PaySlipReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PaySlipReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PaySlipReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
