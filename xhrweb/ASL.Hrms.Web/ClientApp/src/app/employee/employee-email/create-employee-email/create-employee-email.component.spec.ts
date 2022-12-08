import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEmployeeEmailComponent } from './create-employee-email.component';

describe('CreateEmployeeEmailComponent', () => {
  let component: CreateEmployeeEmailComponent;
  let fixture: ComponentFixture<CreateEmployeeEmailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateEmployeeEmailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEmployeeEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
