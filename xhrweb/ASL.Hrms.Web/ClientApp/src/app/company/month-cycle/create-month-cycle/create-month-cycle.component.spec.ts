import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateMonthCycleComponent } from './create-month-cycle.component';

describe('CreateMonthCycleComponent', () => {
  let component: CreateMonthCycleComponent;
  let fixture: ComponentFixture<CreateMonthCycleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateMonthCycleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateMonthCycleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
