import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CarfaxapiListComponent } from './carfaxapi-list.component';

describe('CarfaxapiListComponent', () => {
  let component: CarfaxapiListComponent;
  let fixture: ComponentFixture<CarfaxapiListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CarfaxapiListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CarfaxapiListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
