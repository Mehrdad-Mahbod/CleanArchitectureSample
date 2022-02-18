import { TestBed } from '@angular/core/testing';

import { PublichttpService } from './publichttp.service';

describe('PublichttpService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PublichttpService = TestBed.get(PublichttpService);
    expect(service).toBeTruthy();
  });
});
