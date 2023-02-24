import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DealerLocationComponent } from './dealer-location.component';

describe('DealerLocationComponent', () => {
  let component: DealerLocationComponent;
  let fixture: ComponentFixture<DealerLocationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DealerLocationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DealerLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
