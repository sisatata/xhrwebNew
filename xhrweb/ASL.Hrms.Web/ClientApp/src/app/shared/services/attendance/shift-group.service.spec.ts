import { TestBed } from '@angular/core/testing';

import { ShiftGroupService } from './shift-group.service';

describe('ShiftGroupService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ShiftGroupService = TestBed.get(ShiftGroupService);
    expect(service).toBeTruthy();
  });
});
