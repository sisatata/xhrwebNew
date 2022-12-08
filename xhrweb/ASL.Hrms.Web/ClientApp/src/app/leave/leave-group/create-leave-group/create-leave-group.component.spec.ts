import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateLeaveGroupComponent } from './create-leave-group.component';

describe('CreateLeaveGroupComponent', () => {
  let component: CreateLeaveGroupComponent;
  let fixture: ComponentFixture<CreateLeaveGroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateLeaveGroupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateLeaveGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
