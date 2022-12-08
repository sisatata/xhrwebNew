import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskDetailBaseComponent } from './task-detail-base.component';

describe('TaskDetailBaseComponent', () => {
  let component: TaskDetailBaseComponent;
  let fixture: ComponentFixture<TaskDetailBaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TaskDetailBaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskDetailBaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
