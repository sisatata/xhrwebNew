import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthCycleComponent } from './month-cycle.component';

describe('MonthCycleComponent', () => {
  let component: MonthCycleComponent;
  let fixture: ComponentFixture<MonthCycleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MonthCycleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MonthCycleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
