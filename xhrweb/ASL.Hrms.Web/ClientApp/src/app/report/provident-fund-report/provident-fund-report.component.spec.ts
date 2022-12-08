import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProvidentFundReportComponent } from './provident-fund-report.component';

describe('ProvidentFundReportComponent', () => {
  let component: ProvidentFundReportComponent;
  let fixture: ComponentFixture<ProvidentFundReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProvidentFundReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProvidentFundReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
