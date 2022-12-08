import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveChartComponent } from './leave-chart.component';

describe('LeaveChartComponent', () => {
  let component: LeaveChartComponent;
  let fixture: ComponentFixture<LeaveChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeaveChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaveChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
