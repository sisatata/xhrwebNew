import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OtSummaryReportComponent } from './ot-summary-report.component';

describe('OtSummaryReportComponent', () => {
  let component: OtSummaryReportComponent;
  let fixture: ComponentFixture<OtSummaryReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OtSummaryReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OtSummaryReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
