import { Component, OnInit } from '@angular/core';
import { IccbatchService } from '../_services/iccbatch.service';
@Component({
  selector: 'app-iic-batch',
  templateUrl: './iic-batch.component.html',
  styleUrls: ['./iic-batch.component.less']
})
export class IicBatchComponent implements OnInit {

  constructor(private iccbatchService: IccbatchService) { }

  ngOnInit(): void {

    // call of function on Page laod and if we need to send parameter for ICC batch from angular
    // we can pass from here also
    this.GetAllVehiclesList("", "", "", "", "");
    this.GetAllVehiclesDetails("", "", "", "", "");
  }

  // function for getting Vehicles List
  GetAllVehiclesList(APIBaseURL: string, Apikey: string, Dealers: string, Pageno: string, Format: string) {
    var model = {
      "ApiBaseUrl": APIBaseURL,
      "Apikey": Apikey,
      "Dealers": Dealers,
      "Pageno": Pageno,
      "Format": Format
    }
    this.iccbatchService.getAllVehiclesList(model)
      .subscribe(
        (data: any) => {
        },
        error => {
        });

  }

  GetAllVehiclesDetails(APIBaseURL: string, Apikey: string, Dealers: string, Pageno: string, Format: string) {
    var model = {
      "ApiBaseUrl": APIBaseURL,
      "Apikey": Apikey,
      "Dealers": Dealers,
      "Pageno": Pageno,
      "Format": Format
    }
    this.iccbatchService.getVehiclesDetails(model)
      .subscribe(
        (data: any) => {
        },
        error => {

        });

  }
}
