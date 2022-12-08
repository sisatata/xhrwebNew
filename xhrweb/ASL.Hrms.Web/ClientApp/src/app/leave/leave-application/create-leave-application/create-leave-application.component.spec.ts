import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateLeaveApplicationComponent } from './create-leave-application.component';

describe('CreateLeaveApplicationComponent', () => {
  let component: CreateLeaveApplicationComponent;
  let fixture: ComponentFixture<CreateLeaveApplicationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateLeaveApplicationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateLeaveApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
