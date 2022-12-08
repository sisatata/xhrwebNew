import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEmployeeExperienceComponent } from './create-employee-experience.component';

describe('CreateEmployeeExperienceComponent', () => {
  let component: CreateEmployeeExperienceComponent;
  let fixture: ComponentFixture<CreateEmployeeExperienceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateEmployeeExperienceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEmployeeExperienceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
