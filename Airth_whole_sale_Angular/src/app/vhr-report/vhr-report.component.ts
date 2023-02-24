import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IccbatchService } from '@app/_services/iccbatch.service';
import { PricingInfo } from '@app/_models/PricingInfo';
import { JDPSubOptions } from '@app/_models/JDPSubOptions';
import { VehicleListOfAvailableOffer } from '@app/_models/VehicleListOfAvailableOffer';
import { JDPVehicleInfo } from '@app/_models/JDPVehicleInfo';
import { JDPExtendedDescriptions } from '@app/_models/JDPExtendedDescriptions';
import { JDPListOfAppliedOffers } from '@app/_models/JDPListOfAppliedOffers';
import { JDPVehicleComments } from '@app/_models/JDPVehicleComments';
import { JDPListOfPhotos } from '@app/_models/JDPListOfPhotos';
import { JDPPremiumOptions } from '@app/_models/JDPPremiumOptions';
import { JDPDealerInfo } from '@app/_models/JDPDealerInfo';
import { NgxSpinnerService } from 'ngx-spinner';
import { SideNavComponent } from '@app/side-nav/side-nav.component';
@Component({
  selector: 'app-vhr-report',
  templateUrl: './vhr-report.component.html',
  styleUrls: ['./vhr-report.component.less']
})
export class VhrReportComponent implements OnInit {
  @ViewChild('closebutton') closebutton;
  modalRef: BsModalRef;
  modalcloseOpen: any;
  show = false;
  fullScreen = true;
  template = ``
  Generateddate: any;
  submitted = false;
  userType: any;
  VinNumberControlValue: any;
  show_i_o_error = false;

  isDesc: boolean = false;
  column: string = 'VIN';

  pdfSource = '/assets/MobileReport.pdf'
  searchText;


  pricingInfo: PricingInfo[] = [];
  vehiclePremiumSubOptions: JDPSubOptions[] = [];
  vehicleListOfAvailableOffer: VehicleListOfAvailableOffer[] = [];
  premiumOptions: JDPPremiumOptions[] = [];

  vehicleInformation: JDPVehicleInfo[] = [];
  vehiclePhotosList: JDPListOfPhotos[] = [];
  extendedDescriptionInfo: JDPExtendedDescriptions[] = [];
  jDPListOfAppliedOffers: JDPListOfAppliedOffers[] = [];
  vehicleComments: JDPVehicleComments[] = [];
  dealerList: JDPDealerInfo[] = [];
  makeList: JDPVehicleInfo[] = [];
  errorMessage: string;
  errorshow: any;
  vehicleObject = [];
  vhrReportForm: FormGroup;

  vehcileinfogrid: vehicleinfoforgrid[] = [];


  // For Show Vehicle Details
  ShowVinNumber: any;
  ShowMake: any;
  GetDealerId: any;
  GetMakeValue: any;
  GetOpcodeValue: any;

  // Setting for Pages no and records
  page = 1;
  count = 500;
  VehicleTotalcount:any;
  pageSize = 20;
  pageSizes = [10, 20, 30, 40, 50, 60, 70];


  constructor(private fb: FormBuilder, private modalServiceng: BsModalService,
    private modalService: NgbModal,
    private iccbatchService: IccbatchService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.Createform();
    this.userType = localStorage.getItem('UserType');

    this.GetDealerId = "";
    this.GetMakeValue = "";
    this.GetOpcodeValue = "1";
    this.GetAllVehicleDetails();
    this.GetDealerListForSearchs();
    this.GetVehicleMakeListForSearchs();
  }

  pageChangeEvent(event: number) {
    this.page = event;
    this.GetAllVehicleDetails();
    // this.getUsers();
  }

  handlePageSizeChange(event) {
    this.pageSize = event.target.value;
    this.page = 1;
    this.GetAllVehicleDetails();
  }

  GetAllVehicleDetails() {
    this.GetAllVehicleInformations();
  }

  // for Open model pop up for details
  openExtendedDescriptionModelPopUp(targetModal, VehicleID, VIN, Make) {
    this.ShowVinNumber = VIN;
    this.ShowMake = Make;
    this.GetVehicleInformations(VehicleID);
    this.GetVehicleCommentsList(VehicleID);
    this.modalcloseOpen = this.modalService.open(targetModal, {
      centered: true,
      backdrop: 'static',
      size: 'lg'
    });
  }
  // open model popup for images of Vehicles
  openImagesModelPopUp(targetModal, VehicleID) {
    this.GetVehiclePhotosListByVehicleId(VehicleID);
    this.modalcloseOpen = this.modalService.open(targetModal, {
      centered: true,
      backdrop: 'static',
      size: 'lg'
    });
  }

  Mouseoutpopclose() {
    this.closebutton.nativeElement.click();
  }


  // for getting Vehicles informations
  GetVehicleInformations(VehicleID: string) {
    this.GetVehicleExtendedDescriptionListByVehicleId(VehicleID);
    // this.GetVehiclePricingInfoListByVehicleId(VehicleID);
    this.GetVehiclePremiumOptionsByVehicleId(VehicleID);
    this.GetVehiclePremiumSubOptionsByVehicleId(VehicleID);
    // this.GetVehicleListOfAvailableOfferListByVehicleId(VehicleID);
  }
  GetVehicleImages(VehicleID: string) {
    this.GetVehiclePhotosListByVehicleId(VehicleID);
  }

  // for getting Vehicle List Of Available Offer
  GetVehicleListOfAvailableOfferListByVehicleId(VehicleID: string) {
    var model = {
      "VehicleID": VehicleID
    }
    this.iccbatchService.getVehicleListOfAvailableOfferListByVehicleId(model)
      .subscribe(
        (data: any) => {
          console.log(data);
          this.vehicleListOfAvailableOffer = data;
        },
        error => {
        });
  }

  // get Vehicle Premium Options
  GetVehiclePremiumOptionsByVehicleId(VehicleID: string) {
    var model = {
      "VehicleID": VehicleID
    }
    this.iccbatchService.getVehiclePremiumOptionsByVehicleId(model)
      .subscribe(
        (data: any) => {
          debugger;
          console.log(data);
          this.premiumOptions = data;
        },
        error => {
        });
  }

  // getting make list
  GetVehicleMakeListForSearchs() {
    this.iccbatchService.getVehicleMakeListForSearchs()
      .subscribe(
        (data: any) => {
          this.makeList = data;
        },
        error => {
        });
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
  // get Vehicle Premium Sub Options
  GetVehiclePremiumSubOptionsByVehicleId(VehicleID: string) {
    var model = {
      "VehicleID": VehicleID
    }
    this.iccbatchService.getVehiclePremiumSubOptionsByVehicleId(model)
      .subscribe(
        (data: any) => {
          this.vehiclePremiumSubOptions = data;
        },
        error => {
        });
  }

  GetVehiclePricingInfoListByVehicleId(VehicleID: string) {
    var model = {
      "VehicleID": VehicleID
    }
    this.iccbatchService.getVehiclePricingInfoListByVehicleId(model)
      .subscribe(
        (data: any) => {
          console.log(data);
          this.pricingInfo = data;
        },
        error => {
        });
  }

  GetVehicleExtendedDescriptionListByVehicleId(VehicleID: string) {
    var model = {
      "VehicleID": VehicleID
    }
    this.iccbatchService.getVehicleExtendedDescriptionListByVehicleId(model)
      .subscribe(
        (data: any) => {
          console.log(data);
          this.extendedDescriptionInfo = data;
        },
        error => {
        });
  }

  GetVehiclePhotosListByVehicleId(VehicleID: string) {
    var model = {
      "VehicleID": VehicleID
    }
    this.iccbatchService.getVehiclePhotosListByVehicleId(model)
      .subscribe(
        (data: any) => {
          if (data.length > 0) {
            this.errorshow = 'false';
            for (var i = 0; i < data.length; i++) {
              this.vehicleObject.push({ image: data[i].photoUrl, thumbImage: data[i].photoUrl, title: '' })
            }
          } else {
            this.errorshow = 'true';
            this.errorMessage = "No images for this Vehicle.";
          }

        },
        error => {
        });
  }
  // for refresh images.
  RefreshImageList() {
    this.vehicleObject = [];
  }
  GetVehicleCommentsList(VehicleID: string) {
    var model = {
      "VehicleID": VehicleID
    }
    this.iccbatchService.getVehicleCommentsListByVehicleId(model)
      .subscribe(
        (data: any) => {
          console.log(data);
          this.vehicleComments = data;
        },
        error => {
        });
  }

  GetVehcileListByDealerid(dealerid: any) {
    this.spinner.show();
    this.vehcileinfogrid = [];
    this.GetDealerId = dealerid;
    this.GetMakeValue = "";
    this.GetOpcodeValue = "2";
    this.GetAllVehicleInformations();
  }

  GetVehcileListByMake(Make: any) {
    this.spinner.show();
    this.vehcileinfogrid = [];
    this.GetDealerId = "";
    this.GetMakeValue = Make;
    this.GetOpcodeValue = "3";
    this.GetAllVehicleInformations();
  }

  // for reset filters for Vehicles
  ResetFilter() {
    // this.spinner.show();
    this.vehcileinfogrid = [];
    this.GetDealerId = "";
    this.GetMakeValue = "";
    this.GetOpcodeValue = "1";
    const MakeNameControl = this.vhrReportForm.get('MakeName');
    MakeNameControl.setValue("");
    const DealerNameControl = this.vhrReportForm.get('DealerName');
    DealerNameControl.setValue("");
    const SerachNameControl = this.vhrReportForm.get('SerachName');
    SerachNameControl.setValue("");

    this.GetAllVehicleInformations();
  }

  // for gettting Vehicle Informations
  GetAllVehicleInformations() {
    this.spinner.show();
    var model = {
      "DealerId": this.GetDealerId,
      "Make": this.GetMakeValue,
      "Opcode": this.GetOpcodeValue,
      "PageNumber": this.page,
      "RowsOfPage": this.pageSize,
    }
    this.iccbatchService.getAllVehicleInformations(model)
      .subscribe(
        (data: any) => {
          if (data.length > 0) {
            this.vehcileinfogrid = [];
            for (var i = 0; i < data.length; i++) {

              this.count = data[0].totalcount;
              this.VehicleTotalcount = data[0].vehicleTotalcount;
              this.vehcileinfogrid.push(
                {
                  inStockDate: data[i].inStockDate,
                  stockNumber: data[i].stockNumber,
                  vehicleID: data[i].vehicleID,
                  vehicleStatus: data[i].vehicleStatus,
                  dealerName: data[i].dealerName,
                  vin: data[i].vin,
                  year: data[i].year,
                  make: data[i].make,
                  hwyMPG: data[i].hwyMPG,
                  model: data[i].model,
                  bodyType: data[i].model
                }
              )
            }
        }
          this.spinner.hide();
          // this.show = false
        },
        error => {
        });
  }

  sort(property) {
    this.isDesc = !this.isDesc; //change the direction
    this.column = property;
    let direction = this.isDesc ? 1 : -1;

    this.vehcileinfogrid.sort(function (a, b) {
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
  openpop(targetModal) {
    // for missed cleans

    this.modalRef = this.modalServiceng.show(targetModal);
  }



  openModelPopUp(targetModal, user) {
    debugger;
    this.modalcloseOpen = this.modalService.open(targetModal, {
      centered: true,
      backdrop: 'static',
      size: 'lg'
    });
  }
  get f() { return this.vhrReportForm.controls; }
  onKeyUp(x) { // appending the updated value to the variable
    var value = x.target.value;
    const Icharchter = "i";
    const ocharchter = "o";
    var checki = value.indexOf(Icharchter);
    var checko = value.indexOf(ocharchter);

    if (checki != -1) {
      this.show_i_o_error = true;
      return;
    }
    else if (checko != -1) {
      this.show_i_o_error = true;
      return;
    }
    else {
      this.show_i_o_error = false;
    }
  }
  ShowLoader(modaltarget) {
    debugger;
    this.submitted = true;
    if (this.show_i_o_error == true) {
      return;
    }
    // stop here if form is invalid
    if (this.vhrReportForm.invalid) {
      return;
    }

    const VinNumberControl = this.vhrReportForm.get('VinNumberControl');
    this.VinNumberControlValue = VinNumberControl.value;

    this.Generateddate = new Date();
    //  let latest_date =this.datepipe.transform(this.Generateddate, 'yyyy-MM-dd');

    this.show = true;
    this.fullScreen = true;
    this.template = ``
    setTimeout(() => {
      this.show = false
    }, 2000);

    this.openpopPdf(modaltarget);
  }
  openpopPdf(targetModal) {
    this.modalcloseOpen.dismiss();
    this.Refresh();
    this.modalRef = this.modalServiceng.show(targetModal);
  }

  Refresh() {
    const VinNumberControl = this.vhrReportForm.get('VinNumberControl');
    VinNumberControl.setValue("");
  }

  Createform() {
    this.vhrReportForm = this.fb.group({
      VinNumberControl: ['', [Validators.required, Validators.maxLength(17)]],
      MakeName: [''],
      DealerName: [''],
      SerachName: ['']
    }, {

    });
  }

}

// just an interface for type safety.
interface vehicleinfoforgrid {
  inStockDate: string | null;
  stockNumber: string;
  vehicleID: number;
  vehicleStatus: string;
  dealerName: string;
  vin: string;
  year: number | null;
  make: string;
  hwyMPG: number | null;
  model: string;
  bodyType: string;
  //totalcount: number | null;
}

 