import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeBankAccountComponent } from './employee-bank-account.component';

describe('EmployeeBankAccountComponent', () => {
  let component: EmployeeBankAccountComponent;
  let fixture: ComponentFixture<EmployeeBankAccountComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [EmployeeBankAccountComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeBankAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

