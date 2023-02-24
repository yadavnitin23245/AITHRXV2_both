import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VhrReportComponent } from './vhr-report.component';

describe('VhrReportComponent', () => {
  let component: VhrReportComponent;
  let fixture: ComponentFixture<VhrReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VhrReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VhrReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
