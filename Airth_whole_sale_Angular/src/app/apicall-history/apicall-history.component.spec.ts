import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApicallHistoryComponent } from './apicall-history.component';

describe('ApicallHistoryComponent', () => {
  let component: ApicallHistoryComponent;
  let fixture: ComponentFixture<ApicallHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApicallHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApicallHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
