import { TestBed } from '@angular/core/testing';

import { EmployeeFamilyMemberService } from './employee-family-member.service';

describe('EmployeeFamilyMemberService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmployeeFamilyMemberService = TestBed.get(EmployeeFamilyMemberService);
    expect(service).toBeTruthy();
  });
});
