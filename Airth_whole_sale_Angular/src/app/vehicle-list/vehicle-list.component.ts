import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.less']
})
export class VehicleListComponent implements OnInit {

  VehicleListForm: FormGroup;
  constructor( private fb: FormBuilder,) { }

  ngOnInit(): void {
    this.Createform();
  }
  Createform() {
    this.VehicleListForm = this.fb.group({
    }, {

    });
  }
}
