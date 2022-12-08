import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeSalaryCompComponent } from './employee-salary-comp.component';

describe('EmployeeSalaryComponentComponent', () => {
  let component: EmployeeSalaryCompComponent;
  let fixture: ComponentFixture<EmployeeSalaryCompComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [EmployeeSalaryCompComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeSalaryCompComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

