import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefiniteofficeComponent } from './definiteoffice.component';

describe('DefiniteofficeComponent', () => {
  let component: DefiniteofficeComponent;
  let fixture: ComponentFixture<DefiniteofficeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DefiniteofficeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DefiniteofficeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
