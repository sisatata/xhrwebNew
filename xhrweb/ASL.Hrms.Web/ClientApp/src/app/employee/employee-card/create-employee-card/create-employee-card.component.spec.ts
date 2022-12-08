import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEmployeeCardComponent } from './create-employee-card.component';

describe('CreateEmployeeCardComponent', () => {
  let component: CreateEmployeeCardComponent;
  let fixture: ComponentFixture<CreateEmployeeCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateEmployeeCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEmployeeCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
