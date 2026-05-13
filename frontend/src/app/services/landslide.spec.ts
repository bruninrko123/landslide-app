import { TestBed } from '@angular/core/testing';

import { Landslide } from './landslide';

describe('Landslide', () => {
  let service: Landslide;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Landslide);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
