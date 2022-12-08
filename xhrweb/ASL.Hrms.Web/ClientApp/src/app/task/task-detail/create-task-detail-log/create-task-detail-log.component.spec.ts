import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateTaskDetailLogComponent } from './create-task-detail-log.component';

describe('CreateTaskDetailLogComponent', () => {
  let component: CreateTaskDetailLogComponent;
  let fixture: ComponentFixture<CreateTaskDetailLogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateTaskDetailLogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateTaskDetailLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
