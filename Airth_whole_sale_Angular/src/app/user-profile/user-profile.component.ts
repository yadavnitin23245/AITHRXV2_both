import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService, AlertService } from '@app/_services';
import { MustMatch } from '@app/_helpers';
import { Account } from '../_models/account';
@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.less']
})
export class UserProfileComponent implements OnInit {
  //account = this.accountService.accountValue;

  Userdetails = localStorage.getItem('userdetails');

  account = JSON.parse(this.Userdetails);

  form: FormGroup;
  loading = false;
  submitted = false;
  deleting = false;
  accountlist: Account[] = [];
  UserNameValue: any;
  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private alertService: AlertService) { }

  ngOnInit(): void {
    this.Createform();
    // for getting value from qurey string
    this.UserNameValue = this.route.snapshot.paramMap.get('userName');
    this.GetUserDetailById(this.UserNameValue);
  }

  Createform() {
    this.form = this.formBuilder.group({
      firstName: [this.account.firstName, Validators.required],
      lastName: [this.account.lastName, Validators.required],
      email: [this.account.email, [Validators.required, Validators.email]],
    }, {
    });
  }
  get f() { return this.form.controls; }

// function for getting user detail by id
  GetUserDetailById(UserID: any) {
    debugger;
    var model = {
      "userName": this.UserNameValue
    }
    this.accountService.getUserDetailById(model)
      .subscribe(
        (data: any) => {
          this.accountlist = data;
          // bind value to controls
          const firstName = this.form.get('firstName');
          firstName.setValue(data[0].firstName);
          const lastName = this.form.get('lastName');
          lastName.setValue(data[0].lastName);

          const email = this.form.get('email');
          email.setValue(data[0].email);
        },
        error => {
        });
  }

// function for updating user information
  onSubmit() {
    this.submitted = true;
    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }
    this.loading = true;
    this.accountService.editUserDetail(this.form.value)
      .subscribe(
        (data: any) => {
          this.loading = false;
          this.alertService.success("User detail Updated Successfully");
          return this.router.navigateByUrl("/List");
        },
        (error: any) => {
          this.alertService.error("error");
          this.loading = false;
        }
      );
  }

  onDelete() {
    if (confirm('Are you sure?')) {
      this.deleting = true;
    }

  }

}
