import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService, AlertService } from '@app/_services';

import { map } from 'rxjs/operators';
import { MustMatch } from '@app/_helpers';
import { User } from '../_models/user';
import { country } from '../_models/country';
import { state } from '../_models/state';
import { city } from '../_models/city';
import { StringMapWithRename } from '@angular/compiler/src/compiler_facade_interface';
import { ConfirmPasswordValidator } from "@app/utils/confirm-password.validator"
import { PasswordStrengthValidator } from "@app/utils/password-strength.validators"
@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.less']
})
export class SignUpComponent implements OnInit {


  form: FormGroup;
  loading = false;
  submitted = false;
  isSubmitted = false;
  UserModel: User;
  countrylist: country[] = [];
  statelist: state[] = [];
  citylist: city[] = [];
  SelectedCountryID: any;
  showRemoveButton: any;


  PaymentTypelist = [
    { id: 'Stripe', name: 'Stripe', image: './assets/images/stripe_img.png ' },
    { id: 'BOM', name: 'BOM', image: './assets/images/bom_img.png ' },
    { id: 'Interac', name: 'Interac', image: './assets/images/interac_img.png ' }

  ];



  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private alertService: AlertService) { }

  RegisterForm: FormGroup;
  RegisterUsers: FormArray;


  GetStates(CountryId: any) {
    this.GetStatesList(CountryId.toString());
    this.SelectedCountryID = CountryId.toString();
  }

  GetCityList(stateid: any) {
    this.GetCities(stateid.toString());
  }

  GetCities(stateid: any) {
    debugger;
    var model = {
      "id": stateid,
      "Countryid": this.SelectedCountryID
    }
    this.accountService.getCityList(model)
      .subscribe(
        (data: any) => {
          debugger;
          this.citylist = data;

        },
        error => {

        });

  }
  GetCountryList() {
    debugger;
    this.accountService.getCountryList()
      .subscribe(
        (data: any) => {
          debugger;
          console.log(data);
          this.countrylist = data;

        },
        error => {
        });
  }

  GetStatesList(CountryId: any) {

    var model = {
      "id": CountryId,
      "Name": ""
    }
    this.accountService.getStateList(model)
      .subscribe(
        (data: any) => {
          debugger;
          this.statelist = data;
          const cityName = this.RegisterForm.get('cityName');
          cityName.setValue("");
        },
        error => {

        });

  }

  ngOnInit() {
    this.CreateFrom();
    this.showRemoveButton = 'false';

    this.GetCountryList();
  }
  CreateFrom() {
    this.RegisterForm = new FormGroup({
      businessname: this.fb.control('', Validators.required),
      gstNumber: this.fb.control('', Validators.required),
      EFTinfo: this.fb.control('', Validators.required),
      paymenttype: this.fb.control('', Validators.required),
      addresslineone: this.fb.control('', Validators.required),
      addresslinetwo: this.fb.control('', Validators.required),
      cityName: this.fb.control('', Validators.required),
      Country: this.fb.control('', Validators.required),
      State: this.fb.control('', Validators.required),
      userlist: new FormArray([
        new FormGroup(
          {
            firstName: new FormControl('', [Validators.required]),
            lastName: new FormControl('', [Validators.required]),
            email: new FormControl('', [Validators.required, Validators.email, Validators.pattern(/^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)]),
            // email: ['', [Validators.required, Validators.email,Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]],
            phone: new FormControl('', [Validators.required]),
            password: new FormControl('', [Validators.required, Validators.minLength(8), PasswordStrengthValidator]),
            confirmPassword: new FormControl('', [Validators.required]),

          }

        ),
      ]),

    });
  }


  userlist(): FormArray {
    return this.RegisterForm.get("userlist") as FormArray
  }

  //RegisterUser array data
  UserFieldsArray() {
    return this.fb.group({
      firstName: this.fb.control('', Validators.required),
      lastName: this.fb.control('', Validators.required),
      email: this.fb.control('', [Validators.required, Validators.email, Validators.pattern(/^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)]),
      phone: this.fb.control('', [Validators.required, Validators.pattern("^[0-9]*$")]),
      password: this.fb.control('', [Validators.required, Validators.minLength(8), PasswordStrengthValidator]),
      confirmPassword: this.fb.control('', Validators.required),
      trackingRegisterId: this.generateUniqueId()
    });
  }


  keyPressNumbers(event) {
    var charCode = (event.which) ? event.which : event.keyCode;
    // Only Numbers 0-9
    if ((charCode < 48 || charCode > 57)) {
      event.preventDefault();
      return false;
    }
    else if (event.target.value.length >= 10) {
      event.preventDefault();
      return false;

    }

    else {
      return true;
    }



  }






  addItem(): void {
    this.showRemoveButton = 'true';
    this.RegisterUsers = this.RegisterForm.get('userlist') as FormArray;
    this.RegisterUsers.push(this.UserFieldsArray());
  }

  //generate unique id
  generateUniqueId() {
    return Math.random().toString(36).substring(2, 15) + Math.random().toString(36).substring(2, 15);
  }


  //Add user
  addGroup() {
    this.fa.push(this.UserFieldsArray());
  }

  //remove user
  removeGroup(i: number) {
    this.RegisterUsers.removeAt(i);
  }


  // convenience getter for easy access to form fields
  get f() { return this.RegisterForm.controls; }

  //forformarray
  get fa() { return this.RegisterForm.get('userlist') as FormArray; }

  get RegisterUsersdata(): FormArray {
    return this.RegisterForm.get('userlist') as FormArray;
  }

  // City Names
  City: any = ['Florida', 'South Dakota', 'Tennessee', 'Michigan']

  // Choose city using select dropdown
  changeCity(e) {
    console.log(e.value)
    this.cityName.setValue(e.target.value, {
      onlySelf: true
    })
  }

  // Getter method to access formcontrols
  get cityName() {
    return this.RegisterForm.get('cityName');
  }



  onSubmit() {
    debugger;
    console.log('value: ', this.RegisterForm.value);
    var collectionData = this.UserModel;
    this.submitted = true;
    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.RegisterForm.invalid) {
      return;
    }

    this.loading = true;

    this.accountService.Registerpost(this.RegisterForm.value)
      .subscribe(
        (data: any) => {
          debugger;
          this.loading = false;
          //this.sentmail=true;
          this.alertService.success("User Registered Successfully ");

        },
        (error: any) => {
          debugger;
          this.alertService.error(error);
          this.loading = false;
        }

      );
  }
}
