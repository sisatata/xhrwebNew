import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateTaskDetailComponent } from './create-task-detail.component';

describe('CreateTaskDetailComponent', () => {
  let component: CreateTaskDetailComponent;
  let fixture: ComponentFixture<CreateTaskDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateTaskDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateTaskDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
