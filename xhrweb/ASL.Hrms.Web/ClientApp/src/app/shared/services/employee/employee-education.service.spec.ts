import { TestBed } from '@angular/core/testing';

import { EmployeeEducationService } from './employee-education.service';

describe('EmployeeEducationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmployeeEducationService = TestBed.get(EmployeeEducationService);
    expect(service).toBeTruthy();
  });
});
