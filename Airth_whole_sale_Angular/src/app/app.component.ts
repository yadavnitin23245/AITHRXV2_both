import { Component, EventEmitter, Output } from '@angular/core';

import { AccountService } from './_services';
import { Account, Role } from './_models';
import { Router } from '@angular/router';
import { HeaderComponent } from './header/header.component';

@Component({ selector: 'app', templateUrl: 'app.component.html' })
export class AppComponent {
    menuOpened: boolean = true;
    Role = Role;
    account: Account;
    lgbtn:any;
    userName:any;
    constructor(private accountService: AccountService,  private router: Router,) {
       debugger;
        this.accountService.account.subscribe(x => this.account = x);
    }
    ngOnInit(): void {
        
        var Userdetails = localStorage.getItem('userdetails');
        this.account= JSON.parse(Userdetails);
        this.accountService.logout();
        const returnUrl = '/';
        this.router.navigateByUrl(returnUrl);

      }
    logout() {
        this.accountService.logout();
    }
   
}
