import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IicBatchComponent } from './iic-batch.component';

describe('IicBatchComponent', () => {
  let component: IicBatchComponent;
  let fixture: ComponentFixture<IicBatchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IicBatchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IicBatchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
