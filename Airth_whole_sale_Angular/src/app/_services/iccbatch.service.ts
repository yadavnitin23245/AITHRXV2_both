import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '@environments/environment';
import { PricingInfo } from '../_models/PricingInfo';
import { VehicleListOfAvailableOffer } from '../_models/VehicleListOfAvailableOffer';
import { VehiclePremiumSubOptions } from '../_models/VehiclePremiumSubOptions';

import { JDPVehicleInfo } from '../_models/JDPVehicleInfo';
import { JDPExtendedDescriptions } from '../_models/JDPExtendedDescriptions';
import { JDPListOfAppliedOffers } from '../_models/JDPListOfAppliedOffers';
import { JDPVehicleComments } from '../_models/JDPVehicleComments';
import { JDPListOfPhotos } from '@app/_models/JDPListOfPhotos';
import { JDPDealerInfo } from '@app/_models/JDPDealerInfo';
import { JDPPremiumOptions } from '@app/_models/JDPPremiumOptions';
import { JDPSubOptions } from '@app/_models/JDPSubOptions';
import { JDPAPICallHistory } from '@app/_models/JDPAPICallHistory';
import { JDPPricing } from '@app/_models/JDPPricing';
import { MapDetail } from '@app/_models/MapDetail';

import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IccbatchService {

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) { }

  // Base URL for API call which comes from environment ts file.
  baseUrl = environment.apiUrl;

  // URL For Calling API from Services.
  private baseUrlGetVehiclesDetails: string = environment.apiUrl + '/ICCBatchApi/GetVehiclesDetails';
  private baseUrlGetAllVehiclesList: string = environment.apiUrl + '/ICCBatchApi/GetAllVehiclesList';
  private baseUrlGetAllVehicleInformations: string = environment.apiUrl + '/ICCBatchApi/GetAllVehicleInformations';
  private baseUrlGetVehicleCommentsListByVehicleId: string = environment.apiUrl + '/ICCBatchApi/GetVehicleCommentsListByVehicleId';
  private baseUrlGetVehiclePhotosListByVehicleId: string = environment.apiUrl + '/ICCBatchApi/GetVehiclePhotosListByVehicleId';
  private baseUrlGetVehicleExtendedDescriptionListByVehicleId: string = environment.apiUrl + '/ICCBatchApi/GetVehicleExtendedDescriptionListByVehicleId';
  private baseUrlGetVehiclePricingInfoListByVehicleId: string = environment.apiUrl + '/ICCBatchApi/GetVehiclePricingInfoListByVehicleId';
  private baseUrlGetVehiclePremiumOptionsByVehicleId: string = environment.apiUrl + '/ICCBatchApi/GetVehiclePremiumOptionsListByVehicleId';
  private baseUrlGetVehiclePremiumSubOptionsByVehicleId: string = environment.apiUrl + '/ICCBatchApi/GetVehiclePremiumSubOptionsListByVehicleId';
  private baseUrlGetVehicleListOfAvailableOfferListByVehicleId: string = environment.apiUrl + '/ICCBatchApi/GetVehicleListOfAvailableOfferListByVehicleId';
  private baseUrlGetDealerInformations: string = environment.apiUrl + '/ICCBatchApi/GetDealerInformations';
  private baseUrlGetDealerInfoForSearchs: string = environment.apiUrl + '/ICCBatchApi/GetDealerInfoForSearchs';
  private baseUrlGetVehicleMakeForSearchs: string = environment.apiUrl + '/ICCBatchApi/GetVehicleMakeForSearchs';
  private baseUrlGetJDPAPICallHistoryList: string = environment.apiUrl + '/ICCBatchApi/GetJDPAPICallHistoryList';
  private baseUrlGetJDPVehicleInfoByVehicleId: string = environment.apiUrl + '/ICCBatchApi/GetJDPVehicleInfoByVehicleId';
  private baseUrlGetVehicleSubOpitonsByVehicleId: string = environment.apiUrl + '/ICCBatchApi/GetVehicleSubOpitonsByVehicleId';
  private baseUrlGetVehicleAppliedOffersByVehicleId: string = environment.apiUrl + '/ICCBatchApi/GetVehicleAppliedOffersByVehicleId';
  private baseUrlGetVehiclePricingByVehicleId: string = environment.apiUrl + '/ICCBatchApi/GetVehiclePricingByVehicleId';
  private baseUrlGetVehicleCountByDealerIds: string = environment.apiUrl + '/ICCBatchApi/GetVehicleCountByDealerIds';
  private baseUrlGetVehiclesByDealerNames: string = environment.apiUrl + '/ICCBatchApi/GetVehiclesByDealerNames';
  
  // call API for Dealer Name
  getVehiclesByDealerNames(formdata) {
    return this.http.post<JDPVehicleInfo[]>(this.baseUrlGetVehiclesByDealerNames, formdata);
  }

  // for getting VehicleCount Dealer
  getVehicleCountByDealerIds(): Observable<MapDetail[]> {
    return this.http.get<MapDetail[]>(this.baseUrlGetVehicleCountByDealerIds);
  }

  // for getting Vehicle Pricing informations
  getVehiclePricingByVehicleId(formdata) {
    return this.http.post<JDPPricing[]>(this.baseUrlGetVehiclePricingByVehicleId, formdata);
  }

  // for getting Vehicle Applied Offers
  getVehicleAppliedOffersByVehicleId(formdata) {
    return this.http.post<JDPListOfAppliedOffers[]>(this.baseUrlGetVehicleAppliedOffersByVehicleId, formdata);
  }

  // for getting Vehicle Sub Options
  getVehicleSubOpitonsByVehicleId(formdata) {
    return this.http.post<JDPSubOptions[]>(this.baseUrlGetVehicleSubOpitonsByVehicleId, formdata);
  }

  // for getting Vehicle info by id
  getJDPVehicleInfoByVehicleId(formdata) {
    return this.http.post<JDPVehicleInfo[]>(this.baseUrlGetJDPVehicleInfoByVehicleId, formdata);
  }

  // get list of API call history
  getJDPAPICallHistoryList(): Observable<JDPAPICallHistory[]> {
    return this.http.get<JDPAPICallHistory[]>(this.baseUrlGetJDPAPICallHistoryList);
  }

  // get list of dealers
  getVehicleMakeListForSearchs(): Observable<JDPVehicleInfo[]> {
    return this.http.get<JDPVehicleInfo[]>(this.baseUrlGetVehicleMakeForSearchs);
  }

  // get list of dealers
  getDealerListForSearchs(): Observable<JDPDealerInfo[]> {
    return this.http.get<JDPDealerInfo[]>(this.baseUrlGetDealerInfoForSearchs);
  }

  // for getting Dealer informations
  getDealerInformations(): Observable<JDPDealerInfo[]> {
    return this.http.get<JDPDealerInfo[]>(this.baseUrlGetDealerInformations);
  }

  // for getting Vehicle List Of Available Offer
  getVehicleListOfAvailableOfferListByVehicleId(formdata) {
    return this.http.post<VehicleListOfAvailableOffer[]>(this.baseUrlGetVehicleListOfAvailableOfferListByVehicleId, formdata);
  }

  // for getting Vehicle Premium Sub Options
  getVehiclePremiumSubOptionsByVehicleId(formdata) {
    return this.http.post<JDPSubOptions[]>(this.baseUrlGetVehiclePremiumSubOptionsByVehicleId, formdata);
  }

  // for getting Vehicle Premium Options
  getVehiclePremiumOptionsByVehicleId(formdata) {
    return this.http.post<JDPPremiumOptions[]>(this.baseUrlGetVehiclePremiumOptionsByVehicleId, formdata);
  }

  // for getting Vehicle Pricing Info
  getVehiclePricingInfoListByVehicleId(formdata) {
    return this.http.post<PricingInfo[]>(this.baseUrlGetVehiclePricingInfoListByVehicleId, formdata);
  }

  // for getting Vehicle Extended DescriptionInfo
  getVehicleExtendedDescriptionListByVehicleId(formdata) {
    return this.http.post<JDPExtendedDescriptions[]>(this.baseUrlGetVehicleExtendedDescriptionListByVehicleId, formdata);
  }

  // for getting List  Of Photos
  getVehiclePhotosListByVehicleId(formdata) {
    return this.http.post<JDPListOfPhotos[]>(this.baseUrlGetVehiclePhotosListByVehicleId, formdata);
  }

  // for getting Vehicle Comments
  getVehicleCommentsListByVehicleId(formdata) {
    return this.http.post<JDPVehicleComments[]>(this.baseUrlGetVehicleCommentsListByVehicleId, formdata);
  }


  // for getting Vehicle details
  getAllVehicleInformations(formdata) {
    return this.http.post<JDPVehicleInfo[]>(this.baseUrlGetAllVehicleInformations, formdata);
  }

  getVehiclesDetails(formdata) {
    return this.http.post(this.baseUrlGetVehiclesDetails, formdata);
  }

  getAllVehiclesList(formdata) {
    return this.http.post(this.baseUrlGetAllVehiclesList, formdata);
  }
}
