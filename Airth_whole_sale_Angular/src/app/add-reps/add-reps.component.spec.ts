import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRepsComponent } from './add-reps.component';

describe('AddRepsComponent', () => {
  let component: AddRepsComponent;
  let fixture: ComponentFixture<AddRepsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddRepsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddRepsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
