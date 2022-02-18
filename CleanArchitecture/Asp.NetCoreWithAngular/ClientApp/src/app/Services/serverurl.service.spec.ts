import { TestBed } from '@angular/core/testing';

import { ServerurlService } from './serverurl.service';

describe('ServerurlService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ServerurlService = TestBed.get(ServerurlService);
    expect(service).toBeTruthy();
  });
});
