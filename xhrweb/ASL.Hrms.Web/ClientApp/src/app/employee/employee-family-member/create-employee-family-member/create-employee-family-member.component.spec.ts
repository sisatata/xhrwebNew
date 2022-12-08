import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEmployeeFamilyMemberComponent } from './create-employee-family-member.component';

describe('CreateEmployeeFamilyMemberComponent', () => {
  let component: CreateEmployeeFamilyMemberComponent;
  let fixture: ComponentFixture<CreateEmployeeFamilyMemberComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateEmployeeFamilyMemberComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEmployeeFamilyMemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
