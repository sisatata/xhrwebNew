import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateAttendanceRequestComponent } from './create-attendance-request.component';

describe('CreateAttendanceRequestComponent', () => {
  let component: CreateAttendanceRequestComponent;
  let fixture: ComponentFixture<CreateAttendanceRequestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateAttendanceRequestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateAttendanceRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
