import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEmployeeEducationComponent } from './create-employee-education.component';

describe('CreateEmployeeEducationComponent', () => {
  let component: CreateEmployeeEducationComponent;
  let fixture: ComponentFixture<CreateEmployeeEducationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateEmployeeEducationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEmployeeEducationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
