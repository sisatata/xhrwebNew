import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateCustomConfigurationComponent } from './create-custom-configuration.component';

describe('CreateCustomConfigurationComponent', () => {
  let component: CreateCustomConfigurationComponent;
  let fixture: ComponentFixture<CreateCustomConfigurationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateCustomConfigurationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateCustomConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
