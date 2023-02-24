import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Account, Role } from '@app/_models';
import { AccountService } from '@app/_services/account.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.less']
})
export class HeaderComponent implements OnInit {

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
