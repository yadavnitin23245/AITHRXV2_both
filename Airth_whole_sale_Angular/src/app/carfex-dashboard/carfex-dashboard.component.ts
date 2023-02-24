import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';

import { CarfexportalService } from '../_services/carfexportal.service';
import { AccountService } from '../_services/account.service';
@Component({
  selector: 'app-carfex-dashboard',
  templateUrl: './carfex-dashboard.component.html',
  styleUrls: ['./carfex-dashboard.component.less']
})
export class CarfexDashboardComponent implements OnInit {

  APIBaseURL: string = '';
  clientid: string = '';
  clientsecret: string = '';
  audienceValue: string = '';
  granttypeValue: string = '';

  showAccessTokenDiv: string = 'false';
  ShowErrorMessage: string = '';
  access_tokenValues: string = '';
  expires_inValue: string = '';
  scopeValue: string = '';
  token_typeValues: string = '';
  submitted = false;

  // Variables for Default API Values
  DefaultAPIBaseURL: string = 'https://authentication.carfax.ca/oauth/token/';
  Defaultclientid: string = 'BZLiHB1eW56Pc0bCM2IAMKcpGN5QwFf5';
  Defaultclientsecret: string = 'GCtQfGn8p7f1f0X1nuYh-KOAHundtJrKyRUDCpJSzwJZIWC59oZEqXWVC94iKz8s';
  DefaultaudienceValue: string = 'https://api.carfax.ca';
  DefaultgranttypeValue: string = 'client_credentials';

  Sendclientid = 'BZLiHB1eW56Pc0bCM2IAMKcpGN5QwFf5';
  Sendclientsecret: string = 'GCtQfGn8p7f1f0X1nuYh-KOAHundtJrKyRUDCpJSzwJZIWC59oZEqXWVC94iKz8s';
  CarfexPortalForm: FormGroup;
  constructor(
    private fb: FormBuilder,
    private carfexportalService: CarfexportalService,
    private accountService: AccountService,

  ) {

  }

  // Page load function
  ngOnInit(): void {
    this.showAccessTokenDiv = 'false';
    this.Createform();
  }
  // function for creating controls of form
  Createform() {
    this.CarfexPortalForm = this.fb.group({
      APIBaseControl: ['', Validators.required],
      ClientIDControl: ['', Validators.required],
      granttypeValueControl: ['', Validators.required],
      audienceValueControl: ['', Validators.required],
      clientsecretControl: ['', Validators.required]
    }, {

    });
  }

  // Function for converting last four char into Astrik
  CovertLastFourChar(Value: string) {
    Value = Value.replace(/.(?=.{4,}$)/g, '*');
    return Value;
  }
  // function for setting default values in controls
  SetDefaultValues() {
    const APIBaseControl = this.CarfexPortalForm.get('APIBaseControl');
    APIBaseControl.setValue(this.DefaultAPIBaseURL);

    const ClientIDControl = this.CarfexPortalForm.get('ClientIDControl');
    ClientIDControl.setValue("****************************wFf5");

    const clientsecretControl = this.CarfexPortalForm.get('clientsecretControl');
    clientsecretControl.setValue("************************************************************Kz8s");

    const audienceValueControl = this.CarfexPortalForm.get('audienceValueControl');
    audienceValueControl.setValue(this.DefaultaudienceValue);

    const granttypeValueControl = this.CarfexPortalForm.get('granttypeValueControl');
    granttypeValueControl.setValue(this.DefaultgranttypeValue);

  }
  // convenience getter for easy access to form fields
  get f() { return this.CarfexPortalForm.controls; }

  // Function when user click on Test Connection Button
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.CarfexPortalForm.invalid) {
      return;
    }

    // display form values on success
    const wholeSaleControlValues = this.CarfexPortalForm.value;
    this.APIBaseURL = wholeSaleControlValues.APIBaseControl;
    this.clientid = wholeSaleControlValues.ClientIDControl;
    this.clientsecret = wholeSaleControlValues.clientsecretControl;
    this.audienceValue = wholeSaleControlValues.audienceValueControl;
    this.granttypeValue = wholeSaleControlValues.granttypeValueControl;


    // fucntion for getting API acess token values
    this.CallCarfexPortalApi(this.APIBaseURL, this.clientid, this.clientsecret, this.audienceValue, this.granttypeValue)

  }
  logout() {
    this.accountService.logout();
  }
  // function for getting Carfaxx API values
  CallCarfexPortalApi(APIBaseURL: string, clientid: string, clientsecret: string, audienceValue: string, granttypeValue: string) {

    // Setting values in Model for send to API
    var model = {
      "APIBaseURL": APIBaseURL,
      "clientid": clientid,
      "clientsecret": clientsecret,
      "audienceValue": audienceValue,
      "granttypeValue": granttypeValue
    }
    // call of Service function
    this.carfexportalService.CallCrafexApiPost(model)
      .subscribe(
        (data: any) => {
          // setting values of API into variables for show in front end
          this.access_tokenValues = data.access_token;
          this.expires_inValue = data.expires_in;
          this.scopeValue = data.scope;
          this.token_typeValues = data.token_type;
          this.ShowErrorMessage = "Connected Successfully."
          this.showAccessTokenDiv = 'true';
          this.SetDefaultValues();

        },
        error => {
          this.showAccessTokenDiv = 'false';
          this.ShowErrorMessage = error;
        });

  }
}
