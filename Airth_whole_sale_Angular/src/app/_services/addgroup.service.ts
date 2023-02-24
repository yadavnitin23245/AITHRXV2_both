import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, finalize } from 'rxjs/operators';

import { environment } from '@environments/environment';

import { Account } from '@app/_models';
import { Login } from '../_models/Login';

const baseUrl = `${environment.apiUrl}/Account`;

@Injectable({ providedIn: 'root' })

@Injectable({
  providedIn: 'root'
})
export class AddgroupService {

  private accountSubject: BehaviorSubject<Account>;
  private loginSubject: BehaviorSubject<Login>;
  public account: Observable<Account>;
  public logins: Observable<Login>;
  constructor(
      private router: Router,
      private http: HttpClient
  ) {

      this.accountSubject = new BehaviorSubject<Account>(null);
      this.account = this.accountSubject.asObservable();

      this.loginSubject = new BehaviorSubject<Login>(null);
      this.logins = this.loginSubject.asObservable();

  }


  private baseUrl_saveaddgroup: string = environment.apiUrl + '/Admin/AddGroup';
  private baseUrl_GetAllGroupsList: string = environment.apiUrl + '/Admin/GetAllGroupsList';
  private baseUrl_DeleteGroupsList: string = environment.apiUrl + '/Admin/DeleteGroup';


  //calling function saving method
  Saveaddgroup(formdata) {
    return this.http.post<any[]>(this.baseUrl_saveaddgroup, formdata);
}

//get grouplist

  //calling function saving method
  Getgrouplist() {
    return this.http.get<any[]>(this.baseUrl_GetAllGroupsList);
}

//delete grouplist
 //calling function saving method
 Deletegrouplist(Id) {
  return this.http.get<string>(this.baseUrl_DeleteGroupsList+"/"+Id);
}

}
