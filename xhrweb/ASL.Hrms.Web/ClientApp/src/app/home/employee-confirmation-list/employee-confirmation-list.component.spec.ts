import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeConfirmationListComponent } from './employee-confirmation-list.component';

describe('EmployeeConfirmationListComponent', () => {
  let component: EmployeeConfirmationListComponent;
  let fixture: ComponentFixture<EmployeeConfirmationListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeConfirmationListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeConfirmationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
