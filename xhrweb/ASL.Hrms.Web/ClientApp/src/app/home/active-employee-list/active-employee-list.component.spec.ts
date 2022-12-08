import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ActiveEmployeeListComponent } from './active-employee-list.component';

describe('ActiveEmployeeListComponent', () => {
  let component: ActiveEmployeeListComponent;
  let fixture: ComponentFixture<ActiveEmployeeListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ActiveEmployeeListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ActiveEmployeeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
