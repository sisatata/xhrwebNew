import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseAuthorizedComponent } from './base-authorized.component';

describe('BaseAuthorizedComponent', () => {
  let component: BaseAuthorizedComponent;
  let fixture: ComponentFixture<BaseAuthorizedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BaseAuthorizedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BaseAuthorizedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
