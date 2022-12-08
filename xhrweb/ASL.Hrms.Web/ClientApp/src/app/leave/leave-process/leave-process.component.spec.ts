import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveProcessComponent } from './leave-process.component';

describe('LeaveProcessComponent', () => {
  let component: LeaveProcessComponent;
  let fixture: ComponentFixture<LeaveProcessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeaveProcessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaveProcessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
