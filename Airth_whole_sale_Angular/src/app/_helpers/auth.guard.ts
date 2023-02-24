import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AccountService } from '@app/_services';
import { environment } from '@environments/environment';
@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

  private baseUrlLoginurl: string = environment.apiUrl + '/Users/authenticate';
    constructor(
        private router: Router,
        private accountService: AccountService
    ) { }



    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        debugger;
       // const account = this.accountService.accountValue;

        var Userdetails = localStorage.getItem('userdetails');

        const account= JSON.parse(Userdetails);

        if (account) {
            debugger;
            // check if route is restricted by role
            if (route.data.roles && !route.data.roles.includes(account.role)) {
                // role not authorized so redirect to home page
                this.router.navigate(['/']);
                return false;
            }

            // authorized so return true
            return true;
        }

        // not logged in so redirect to login page with the return url
        this.router.navigate([this.baseUrlLoginurl], { queryParams: { returnUrl: state.url }});
        return false;
    }
}
