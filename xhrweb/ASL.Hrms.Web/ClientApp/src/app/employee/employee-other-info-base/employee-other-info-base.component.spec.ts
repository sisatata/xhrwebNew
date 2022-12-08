import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeOtherInfoBaseComponent } from './employee-other-info-base.component';

describe('EmployeeOtherInfoBaseComponent', () => {
  let component: EmployeeOtherInfoBaseComponent;
  let fixture: ComponentFixture<EmployeeOtherInfoBaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeOtherInfoBaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeOtherInfoBaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
