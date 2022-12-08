import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeFamilyMemberComponent } from './employee-family-member.component';

describe('EmployeeFamilyMemberComponent', () => {
  let component: EmployeeFamilyMemberComponent;
  let fixture: ComponentFixture<EmployeeFamilyMemberComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeFamilyMemberComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeFamilyMemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
