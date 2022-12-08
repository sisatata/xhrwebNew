import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FestivalBonusComponent } from './festival-bonus.component';

describe('FestivalBonusComponent', () => {
  let component: FestivalBonusComponent;
  let fixture: ComponentFixture<FestivalBonusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FestivalBonusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FestivalBonusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
