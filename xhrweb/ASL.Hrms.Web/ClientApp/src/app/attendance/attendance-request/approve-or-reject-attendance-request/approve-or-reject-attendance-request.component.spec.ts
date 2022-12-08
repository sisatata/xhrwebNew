import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveOrRejectAttendanceRequestComponent } from './approve-or-reject-attendance-request.component';

describe('ApproveOrRejectAttendanceRequestComponent', () => {
  let component: ApproveOrRejectAttendanceRequestComponent;
  let fixture: ComponentFixture<ApproveOrRejectAttendanceRequestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApproveOrRejectAttendanceRequestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApproveOrRejectAttendanceRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
