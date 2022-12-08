import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeEnrollmentComponent } from './employee-enrollment.component';

describe('EmployeeEnrollmentComponent', () => {
  let component: EmployeeEnrollmentComponent;
  let fixture: ComponentFixture<EmployeeEnrollmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeEnrollmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeEnrollmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
