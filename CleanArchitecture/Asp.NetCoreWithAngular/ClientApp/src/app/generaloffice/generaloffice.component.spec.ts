import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GeneralofficeComponent } from './generaloffice.component';

describe('GeneralofficeComponent', () => {
  let component: GeneralofficeComponent;
  let fixture: ComponentFixture<GeneralofficeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GeneralofficeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GeneralofficeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
