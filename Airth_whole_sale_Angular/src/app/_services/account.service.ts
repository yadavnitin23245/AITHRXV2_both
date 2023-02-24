import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, finalize } from 'rxjs/operators';

import { environment } from '@environments/environment';

import { Account } from '@app/_models';
import { Login } from '../_models/Login';
import { country } from '../_models/country';
import { state } from '../_models/state';
import { city } from '../_models/city';
const baseUrl = `${environment.apiUrl}/Account`;

@Injectable({ providedIn: 'root' })
export class AccountService {

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
   // Define Variables for API call
    private baseUrlGetUserList: string = environment.apiUrl + '/Common/Getuserlist';
    private baseUrlRegister: string = environment.apiUrl + '/Account/RegisterWithoutmailconfirmation';
    private baseUrlGetCountryList: string = environment.apiUrl + '/Common/GetCountries';
    private baseUrlGetStateList: string = environment.apiUrl + '/Common/GetStateListByCountryIds';
    private baseUrlGetCityList: string = environment.apiUrl + '/Common/GetCityListByCountryIds';
    private baseUrlGetUserDetailById: string = environment.apiUrl + '/Common/GetUserDetailById';
    private baseUrl_Forgotpassword: string = environment.apiUrl + '/Account/ForgotPassword';
    private baseUrl_Resetpassword: string = environment.apiUrl + '/Account/ResetPassword';
    private baseUrl_Login: string = environment.apiUrl + '/Account/Login';
    private baseUrlEditUserDetail: string = environment.apiUrl + '/Common/updateUserDetail';
    private baseUrlLoginurl: string = environment.apiUrl + '/Users/authenticate';
    private baseUrlDeleteUser: string = environment.apiUrl + '/Common/DeleteUser';
    private baseUrlDeleteUsers: string = environment.apiUrl + '/Common/DeleteUser/';

    // functions for getting User for edit information
    editUserDetail(formdata) {
        const headers = new HttpHeaders({
            'Content-Type': 'application/json; charset=utf-8',

        });
        return this.http.post(this.baseUrlEditUserDetail, formdata);
    }
    DeleteUser(formdata) {
        const headers = new HttpHeaders({
            'Content-Type': 'application/json; charset=utf-8',

        });
        return this.http.post(this.baseUrlDeleteUser, formdata);
    }
    deleteUsers(email:any){
        var URL = this.baseUrlDeleteUsers + email;
        return this.http.get(URL);
      }

    getUserDetailById(formdata) {
        return this.http.post<Account[]>(this.baseUrlGetUserDetailById, formdata);
    }

    // Working regitser method
    Register(formdata) {
        return this.http.post(this.baseUrlRegister, formdata);
    }

    //regsiter users
    Registerpost(formdata) {
        const headers = new HttpHeaders({
            'Content-Type': 'application/json; charset=utf-8',
            'Accept': 'application/json'
        });
        const requestOptions = { headers: headers };
        return this.http.post(this.baseUrlRegister, formdata);
      }

    //Forgot passowrd
    ForgotPasswordPost(formdata) {
        const headers = new HttpHeaders({
            'Content-Type': 'application/json; charset=utf-8',

        });
        const requestOptions = { headers: headers };
        return this.http.post(this.baseUrl_Forgotpassword, formdata, requestOptions);
    }



    //Reset passowrd
    ResetPassword(formdata) {
        const headers = new HttpHeaders({
            'Content-Type': 'application/json; charset=utf-8',

        });
        const requestOptions = { headers: headers };
        return this.http.post(this.baseUrl_Resetpassword, formdata, requestOptions);
    }

    public get accountValue(): Account {
        return this.accountSubject.value;
    }

    public get loginValue(): Login {
        return this.loginSubject.value;
    }



    login(Email: string, password: string) {
        return this.http.post<any>(this.baseUrlLoginurl, { Email, password })
            .pipe(map(account => {
                this.accountSubject.next(account);
                localStorage.setItem('userdetails', JSON.stringify(account.userdata));
                localStorage.setItem("jwt", account.jwtToken);
                localStorage.setItem('AuthToken', account.jwtToken);
                localStorage.setItem('UserRole', account.role);
                localStorage.setItem('UserName', account.username);

                localStorage.setItem('email', account.email);
                return account;
            }));
    }

    setValueUserAdmin(UserName: string) {
        if (UserName == "Admin") {
            localStorage.setItem('UserType', 'true');
        } else if (UserName == "Dealer") {
            localStorage.setItem('UserType', 'false');
        }

    }

    getUserList(): Observable<Account[]> {
        debugger;
      return this.http.get<Account[]>(this.baseUrlGetUserList);
    }

    // functions for getting City , state,country
    getCountryList() {
        debugger;
        return this.http.get<country[]>(this.baseUrlGetCountryList);
    }

    getStateList(formdata) {
        debugger;
        return this.http.post<state[]>(this.baseUrlGetStateList, formdata);
    }

    getCityList(formdata) {
        debugger;
        return this.http.post<city[]>(this.baseUrlGetCityList, formdata);
    }

    logout() {
        localStorage.removeItem("AuthToken");
        localStorage.removeItem('userdetails')
        // this.stopRefreshTokenTimer();
        this.accountSubject.next(null);
        const returnUrl = '/';
        this.router.navigateByUrl(returnUrl);
        //this.router.navigate(['/account/login']);
    }

    refreshToken() {
        return this.http.post<any>(`${baseUrl}/refresh-token`, {}, { withCredentials: true })
            .pipe(map((account) => {
                this.accountSubject.next(account);
                this.startRefreshTokenTimer();
                return account;
            }));
    }

    register(account: Account) {
        return this.http.post(`${baseUrl}/register`, account);
    }

    verifyEmail(token: string) {
        return this.http.post(`${baseUrl}/verify-email`, { token });
    }



    validateResetToken(token: string) {
        return this.http.post(`${baseUrl}/validate-reset-token`, { token });
    }

    resetPassword(token: string, password: string, confirmPassword: string) {
        return this.http.post(`${baseUrl}/reset-password`, { token, password, confirmPassword });
    }

    getAll() {
        return this.http.get<Account[]>(baseUrl);
    }

    getById(id: string) {
        return this.http.get<Account>(`${baseUrl}/${id}`);
    }

    create(params) {
        return this.http.post(baseUrl, params);
    }

    update(id, params) {
        return this.http.put(`${baseUrl}/${id}`, params)
            .pipe(map((account: any) => {
                // update the current account if it was updated
                if (account.id === this.accountValue.id) {
                    // publish updated account to subscribers
                    account = { ...this.accountValue, ...account };
                    this.accountSubject.next(account);
                }
                return account;
            }));
    }

    delete(id: string) {
        return this.http.delete(`${baseUrl}/${id}`)
            .pipe(finalize(() => {
                // auto logout if the logged in account was deleted
                if (id === this.accountValue.id)
                    this.logout();
            }));
    }

    // helper methods

    private refreshTokenTimeout;

    private startRefreshTokenTimer() {
        // parse json object from base64 encoded jwt token
        // const jwtToken = JSON.parse(atob(this.account.token.split('.')[1]));

        // // set a timeout to refresh the token a minute before it expires
        // const expires = new Date(jwtToken.exp * 1000);
        // const timeout = expires.getTime() - Date.now() - (60 * 1000);
        // this.refreshTokenTimeout = setTimeout(() => this.refreshToken().subscribe(), timeout);
    }

    private stopRefreshTokenTimer() {
        clearTimeout(this.refreshTokenTimeout);
    }
}
