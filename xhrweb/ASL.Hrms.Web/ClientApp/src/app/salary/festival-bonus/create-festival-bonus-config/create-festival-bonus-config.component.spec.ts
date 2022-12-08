import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateFestivalBonusConfigComponent } from './create-festival-bonus-config.component';

describe('CreateFestivalBonusConfigComponent', () => {
  let component: CreateFestivalBonusConfigComponent;
  let fixture: ComponentFixture<CreateFestivalBonusConfigComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateFestivalBonusConfigComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateFestivalBonusConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
