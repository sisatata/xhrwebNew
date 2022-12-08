import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEarnLeaveEncashmentComponent } from './create-earn-leave-encashment.component';

describe('CreateEarnLeaveEncashmentComponent', () => {
  let component: CreateEarnLeaveEncashmentComponent;
  let fixture: ComponentFixture<CreateEarnLeaveEncashmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateEarnLeaveEncashmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEarnLeaveEncashmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
