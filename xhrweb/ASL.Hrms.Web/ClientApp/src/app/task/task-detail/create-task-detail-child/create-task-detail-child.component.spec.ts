import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateTaskDetailChildComponent } from './create-task-detail-child.component';

describe('CreateTaskDetailChildComponent', () => {
  let component: CreateTaskDetailChildComponent;
  let fixture: ComponentFixture<CreateTaskDetailChildComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateTaskDetailChildComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateTaskDetailChildComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
