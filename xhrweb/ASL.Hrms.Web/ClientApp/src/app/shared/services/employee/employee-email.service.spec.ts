import { TestBed } from '@angular/core/testing';

import { EmployeeEmailService } from './employee-email.service';

describe('EmployeeEmailService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmployeeEmailService = TestBed.get(EmployeeEmailService);
    expect(service).toBeTruthy();
  });
});
