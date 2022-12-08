import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveOrRejectLeaveRequestComponent } from './approve-or-reject-leave-request.component';

describe('ApproveOrRejectLeaveRequestComponent', () => {
  let component: ApproveOrRejectLeaveRequestComponent;
  let fixture: ComponentFixture<ApproveOrRejectLeaveRequestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApproveOrRejectLeaveRequestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApproveOrRejectLeaveRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
