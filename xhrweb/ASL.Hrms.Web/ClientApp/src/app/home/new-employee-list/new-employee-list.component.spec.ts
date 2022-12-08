import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewEmployeeListComponent } from './new-employee-list.component';

describe('NewEmployeeListComponent', () => {
  let component: NewEmployeeListComponent;
  let fixture: ComponentFixture<NewEmployeeListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewEmployeeListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewEmployeeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
