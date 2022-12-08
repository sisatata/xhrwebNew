import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateTaskCategoryComponent } from './create-task-category.component';

describe('CreateTaskCategoryComponent', () => {
  let component: CreateTaskCategoryComponent;
  let fixture: ComponentFixture<CreateTaskCategoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateTaskCategoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateTaskCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
