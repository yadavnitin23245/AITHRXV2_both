import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { JDPExtendedDescriptions } from '@app/_models/JDPExtendedDescriptions';
import { JDPListOfAppliedOffers } from '@app/_models/JDPListOfAppliedOffers';
import { JDPListOfPhotos } from '@app/_models/JDPListOfPhotos';
import { JDPPremiumOptions } from '@app/_models/JDPPremiumOptions';
import { JDPPricing } from '@app/_models/JDPPricing';
import { JDPSubOptions } from '@app/_models/JDPSubOptions';
import { JDPVehicleComments } from '@app/_models/JDPVehicleComments';
import { JDPVehicleInfo } from '@app/_models/JDPVehicleInfo';
import { IccbatchService } from '@app/_services/iccbatch.service';
@Component({
  selector: 'app-vehicle-details',
  templateUrl: './vehicle-details.component.html',
  styleUrls: ['./vehicle-details.component.less']
})
export class VehicleDetailsComponent implements OnInit {
  errorshow: any;
  errorMessage: any;
  vehicleObject = [];
  VehicleIdValue: any;
  vehicleComments: JDPVehicleComments[] = [];
  premiumOptions: JDPPremiumOptions[] = [];
  vehicleInformation: JDPVehicleInfo[] = [];
  vehiclePhotosList: JDPListOfPhotos[] = [];
  extendedDescriptionInfo: JDPExtendedDescriptions[] = [];
  subOptionsList: JDPSubOptions[] = [];
  listOfAppliedOffers: JDPListOfAppliedOffers[] = [];
  pricingInfo: JDPPricing[] = [];
  showpricingInfo: any;
  showlistOfAppliedOffers: any;
  vehicleDetailForm: FormGroup;
  VinValue: any;
  MakeValue: any;
  DealerNameValue: any;
  ModelValue: any;
  showextendeddetail: any;
  showcommentsdetail: any;
  premiumshow: any;
  pricingshow: any;
  SubOtpionshow: any;

  // Variables for Pricing information
  CostValue: any;
  ExtraPrice1Value: any;
  ExtraPrice2Value: any;
  ExtraPrice3Value: any;
  ListValue: any;
  SpecialValue: any;
  constructor(private fb: FormBuilder,
    private iccbatchService: IccbatchService, private route: ActivatedRoute,
    private router: Router,) { }

  ngOnInit(): void {

    this.Createform();
    // for getting value of VehicleId from qurey string
    this.VehicleIdValue = this.route.snapshot.paramMap.get('vehicleId');

    // call all the functions related to VehicleInfo by VehicleId
    this.GetJDPVehicleInfoByVehicleId(this.VehicleIdValue);
    this.GetVehiclePhotosListByVehicleId(this.VehicleIdValue);
    this.GetVehicleCommentsList(this.VehicleIdValue);
    this.GetVehicleExtendedDescriptionListByVehicleId(this.VehicleIdValue);
    this.GetVehiclePremiumOptionsByVehicleId(this.VehicleIdValue);
    this.GetVehicleSubOpitonsByVehicleId(this.VehicleIdValue);
    this.GetVehicleAppliedOffersByVehicleId(this.VehicleIdValue);
    this.GetVehiclePricingByVehicleId(this.VehicleIdValue)
  }

  Createform() {
    this.vehicleDetailForm = this.fb.group({

    }, {

    });
  }
 // function for getting VehicleInfo by id
  GetJDPVehicleInfoByVehicleId(VehicleID: string) {
    var model = {
      "VehicleID": VehicleID
    }
    this.iccbatchService.getJDPVehicleInfoByVehicleId(model)
      .subscribe(
        (data: any) => {
          this.VinValue = data[0].vin;
          this.MakeValue = data[0].make;
          this.DealerNameValue = data[0].dealerName;
          this.ModelValue = data[0].model;
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
          if (data.length > 0) {
            this.premiumshow = 'true';
            this.premiumOptions = data;
          } else {
            this.premiumshow = 'false';
          }
        },
        error => {
        });
  }
  // for getting extended details
  GetVehicleExtendedDescriptionListByVehicleId(VehicleID: string) {
    var model = {
      "VehicleID": VehicleID
    }
    this.iccbatchService.getVehicleExtendedDescriptionListByVehicleId(model)
      .subscribe(
        (data: any) => {
          if (data.length > 0) {
            this.showextendeddetail = 'true';
            this.extendedDescriptionInfo = data;
          } else {
            this.showextendeddetail = 'false';
          }

        },
        error => {
        });
  }
  // for getting comments.
  GetVehicleCommentsList(VehicleID: string) {
    var model = {
      "VehicleID": VehicleID
    }
    this.iccbatchService.getVehicleCommentsListByVehicleId(model)
      .subscribe(
        (data: any) => {
          if (data.length > 0) {
            this.showcommentsdetail = 'true';
            this.vehicleComments = data;
          } else {
            this.showcommentsdetail = 'false';
            this.vehicleComments = [];
          }
        },
        error => {
        });
  }
  // for gettiing photos list by id
  GetVehiclePhotosListByVehicleId(VehicleID: string) {
    var model = {
      "VehicleID": VehicleID
    }
    this.iccbatchService.getVehiclePhotosListByVehicleId(model)
      .subscribe(
        (data: any) => {
          if (data.length > 0) {
            debugger;
            this.errorshow = 'false';
            // add images of Vehicle into array object for binding in slider
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

  // for getting Vehicle sub options
  GetVehicleSubOpitonsByVehicleId(VehicleID: string) {
    var model = {
      "VehicleID": VehicleID
    }
    this.iccbatchService.getVehicleSubOpitonsByVehicleId(model)
      .subscribe(
        (data: any) => {
          if (data.length > 0) {
            this.SubOtpionshow = 'true';
            this.subOptionsList = data;
          } else {
            this.SubOtpionshow = 'false';
            this.subOptionsList = [];
          }

        },
        error => {
        });
  }
  // for getting Vehicle Applied Offers
  GetVehicleAppliedOffersByVehicleId(VehicleID: string) {
    var model = {
      "VehicleID": VehicleID
    }
    this.iccbatchService.getVehicleAppliedOffersByVehicleId(model)
      .subscribe(
        (data: any) => {
          if (data.length > 0) {
            this.showlistOfAppliedOffers = 'true';
            this.listOfAppliedOffers = data;
          } else {
            this.showlistOfAppliedOffers = 'false';
            this.listOfAppliedOffers = [];
          }

        },
        error => {
        });
  }

  // for getting Vehicle Pricing
  GetVehiclePricingByVehicleId(VehicleID: string) {
    var model = {
      "VehicleID": VehicleID
    }
    this.iccbatchService.getVehiclePricingByVehicleId(model)
      .subscribe(
        (data: any) => {
          if (data.length > 0) {
            this.showpricingInfo = 'true';
            this.pricingInfo = data;
            // setting price value to variables
            this.CostValue = data[0].cost;
            this.ExtraPrice1Value = data[0].extraPrice1;
            this.ExtraPrice2Value = data[0].extraPrice2;
            this.ExtraPrice3Value = data[0].extraPrice3;
            this.ListValue = data[0].list;
            this.SpecialValue = data[0].special;

          } else {
            this.showpricingInfo = 'false';
            this.pricingInfo = [];
          }

        },
        error => {
        });
  }
}
