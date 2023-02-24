import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VhrpdfreportViewComponent } from './vhrpdfreport-view.component';

describe('VhrpdfreportViewComponent', () => {
  let component: VhrpdfreportViewComponent;
  let fixture: ComponentFixture<VhrpdfreportViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VhrpdfreportViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VhrpdfreportViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
