import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { JDPAPICallHistory } from '@app/_models/JDPAPICallHistory';
import { JDPDealerInfo } from '@app/_models/JDPDealerInfo';
import { IccbatchService } from '@app/_services/iccbatch.service';

@Component({
  selector: 'app-apicall-history',
  templateUrl: './apicall-history.component.html',
  styleUrls: ['./apicall-history.component.less']
})
export class ApicallHistoryComponent implements OnInit {
  callhistory: JDPAPICallHistory[] = [];
  APICallHistoryForm: FormGroup;
  isDesc: boolean = false;
  column: string = 'userName';
   //paging
   name = 'Angular';
   page = 1;
   pageSize =10;
   searchText;
   dealerList: JDPDealerInfo[] = [];
  constructor(private iccbatchService: IccbatchService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.Createform();
    this.GetJDPAPICallHistoryList();
    this.GetDealerListForSearchs();
  }

  // getting list of dealers
  GetDealerListForSearchs() {
    this.iccbatchService.getDealerListForSearchs()
      .subscribe(
        (data: any) => {
          this.dealerList = data;
        },
        error => {
        });
  }

  GetJDPAPICallHistoryList() {
    this.iccbatchService.getJDPAPICallHistoryList()
      .subscribe(
        (data: any) => {
          this.callhistory = data;
        },
        error => {
        });
  }
  CallAPIbyDealerName(DealerID: any) {
    this.CallAPIbyDealerNames(DealerID);
  }

  // function for Calling API
  CallAPIbyDealerNames(DealerID: string) {
    var model = {
      "Dealers": DealerID
    }
    this.iccbatchService.getVehiclesByDealerNames(model)
      .subscribe(
        (data: any) => {
          alert("API execute successfully.")
        },
        error => {
        });
  }
  sort(property) {
    debugger;
    this.isDesc = !this.isDesc; //change the direction
    this.column = property;
    let direction = this.isDesc ? 1 : -1;

    this.callhistory.sort(function (a, b) {
      if (a[property] < b[property]) {
        return -1 * direction;
      }
      else if (a[property] > b[property]) {
        return 1 * direction;
      }
      else {
        return 0;
      }
    });
  };

  Createform() {
    this.APICallHistoryForm = this.fb.group({
      DealerName: [''],
    }, {

    });
  }
}
