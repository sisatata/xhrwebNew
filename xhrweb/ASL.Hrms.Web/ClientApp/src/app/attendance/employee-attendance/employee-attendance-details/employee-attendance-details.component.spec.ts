import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeAttendanceDetailsComponent } from './employee-attendance-details.component';

describe('EmployeeAttendanceDetailsComponent', () => {
  let component: EmployeeAttendanceDetailsComponent;
  let fixture: ComponentFixture<EmployeeAttendanceDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeAttendanceDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeAttendanceDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
