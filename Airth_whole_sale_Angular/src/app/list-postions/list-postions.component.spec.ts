import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListPostionsComponent } from './list-postions.component';

describe('ListPostionsComponent', () => {
  let component: ListPostionsComponent;
  let fixture: ComponentFixture<ListPostionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListPostionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListPostionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
