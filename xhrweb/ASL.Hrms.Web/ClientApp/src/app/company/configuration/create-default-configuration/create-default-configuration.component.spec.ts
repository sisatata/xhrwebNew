import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateDefaultConfigurationComponent } from './create-default-configuration.component';

describe('CreateDefaultConfigurationComponent', () => {
  let component: CreateDefaultConfigurationComponent;
  let fixture: ComponentFixture<CreateDefaultConfigurationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateDefaultConfigurationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateDefaultConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
