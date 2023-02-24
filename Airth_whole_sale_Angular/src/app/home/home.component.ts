import { Component } from '@angular/core';

import { AccountService } from '@app/_services';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
    account = this.accountService.accountValue;

    constructor(private accountService: AccountService) { }


    ngOnInit(): void {
        this.GetUserList();
    }

    // for getting user list from user table
    GetUserList() {
        this.accountService.getUserList()
            .subscribe(
                (data: any) => {
                    console.log(data);
                },
                error => {
                });

    }
}