import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FullWidthLayoutComponent } from './full-width-layout.component';

describe('FullWidthLayoutComponent', () => {
  let component: FullWidthLayoutComponent;
  let fixture: ComponentFixture<FullWidthLayoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FullWidthLayoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FullWidthLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
