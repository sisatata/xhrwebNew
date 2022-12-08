import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecTestComponent } from './rec-test.component';

describe('RecTestComponent', () => {
  let component: RecTestComponent;
  let fixture: ComponentFixture<RecTestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecTestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
