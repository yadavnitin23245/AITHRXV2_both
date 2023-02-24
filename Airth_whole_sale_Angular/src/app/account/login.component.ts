import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AccountService, AlertService } from '@app/_services';

@Component({ templateUrl: 'login.component.html' })
export class LoginComponent implements OnInit {
    form: FormGroup;
    loading = false;
    submitted = false;
    lginEmailValue: any;
    show_userAccount = false;
    errorMessage:any;
    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private alertService: AlertService
    ) { }

    ngOnInit() {
        this.form = this.formBuilder.group({
            email: ['', [Validators.required, Validators.email,Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]],
            password: ['', Validators.required]
        });
    }

    // convenience getter for easy access to form fields
    get f() { return this.form.controls; }


    onSubmit() {
        debugger;
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();
        const emailValue = this.form.get('email');
        this.lginEmailValue = emailValue.value;
        // stop here if form is invalid
        if (this.form.invalid) {
            return;
        }

        this.loading = true;
        this.accountService.login(this.f.email.value, this.f.password.value)
            .pipe(first())
            .subscribe({
                next: () => {
                    // Set Landing page according to user role.
                    if (localStorage.getItem('UserRole') == "Admin") {
                        this.accountService.setValueUserAdmin("Admin");
                       return this.router.navigateByUrl("/master-dashboard");
                        
                    } else if (localStorage.getItem('UserRole') == "Dealer") {
                        this.accountService.setValueUserAdmin("Dealer");
                       return this.router.navigateByUrl("/master-dashboard");

                    }

                },
                error: error => {
                    this.errorMessage="Please check your Login Credentials";
                    this.alertService.error(this.errorMessage);
                    this.loading = false;
                }
            });
    }
}