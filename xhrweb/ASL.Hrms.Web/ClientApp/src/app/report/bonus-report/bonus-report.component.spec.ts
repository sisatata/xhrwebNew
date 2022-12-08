import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BonusReportComponent } from './bonus-report.component';

describe('BonusReportComponent', () => {
  let component: BonusReportComponent;
  let fixture: ComponentFixture<BonusReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BonusReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BonusReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
