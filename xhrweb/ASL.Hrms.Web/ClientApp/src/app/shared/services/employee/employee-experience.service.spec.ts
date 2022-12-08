import { TestBed } from '@angular/core/testing';

import { EmployeeExperienceService } from './employee-experience.service';

describe('EmployeeExperienceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmployeeExperienceService = TestBed.get(EmployeeExperienceService);
    expect(service).toBeTruthy();
  });
});
