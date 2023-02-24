import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CarfexDashboardComponent } from './carfex-dashboard.component';

describe('CarfexDashboardComponent', () => {
  let component: CarfexDashboardComponent;
  let fixture: ComponentFixture<CarfexDashboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CarfexDashboardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CarfexDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
