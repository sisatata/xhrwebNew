import { TestBed } from '@angular/core/testing';

import { CustomConfigurationService } from './custom-configuration.service';

describe('CustomConfigurationService', () => {
  let service: CustomConfigurationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CustomConfigurationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
