import { TestBed } from '@angular/core/testing';

import { LDRService } from './ldr.service';

describe('LDRService', () => {
  let service: LDRService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LDRService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
