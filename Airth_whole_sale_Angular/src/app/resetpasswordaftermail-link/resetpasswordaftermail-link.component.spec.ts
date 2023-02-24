import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ResetpasswordaftermailLinkComponent } from './resetpasswordaftermail-link.component';

describe('ResetpasswordaftermailLinkComponent', () => {
  let component: ResetpasswordaftermailLinkComponent;
  let fixture: ComponentFixture<ResetpasswordaftermailLinkComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ResetpasswordaftermailLinkComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ResetpasswordaftermailLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
