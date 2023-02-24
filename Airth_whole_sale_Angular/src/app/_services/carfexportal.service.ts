import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '@environments/environment';

const baseUrl = `${environment.apiUrl}/CarfexAPIValue`;
@Injectable({
  providedIn: 'root'
})
export class CarfexportalService {

  constructor(private fb: FormBuilder,private http: HttpClient, private router: Router) { }
  baseUrl = environment.apiUrl;

  // Define URL for API Call
  private baseUrlPostToWholsaleAPI: string = environment.apiUrl + '/CarfexAPIValue/getcarfaxapitokenbyclientid';

  // function for getting data from API
  CallCrafexApiPost(formdata) {
     return this.http.post(this.baseUrlPostToWholsaleAPI, formdata);
  }

}
