import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmationReportComponent } from './confirmation-report.component';

describe('ConfirmationReportComponent', () => {
  let component: ConfirmationReportComponent;
  let fixture: ComponentFixture<ConfirmationReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfirmationReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmationReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
