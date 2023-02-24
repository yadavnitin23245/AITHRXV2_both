import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUserrolesComponent } from './add-userroles.component';

describe('AddUserrolesComponent', () => {
  let component: AddUserrolesComponent;
  let fixture: ComponentFixture<AddUserrolesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddUserrolesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddUserrolesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
