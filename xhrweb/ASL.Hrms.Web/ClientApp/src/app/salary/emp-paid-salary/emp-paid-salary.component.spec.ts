import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpPaidSalaryComponent } from './emp-paid-salary.component';

describe('EmpPaidSalaryComponent', () => {
  let component: EmpPaidSalaryComponent;
  let fixture: ComponentFixture<EmpPaidSalaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmpPaidSalaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmpPaidSalaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
