
import { Component, OnInit, ViewChild, ElementRef, QueryList, ViewChildren } from '@angular/core';

import { Router } from '@angular/router';
import { FormGroup, FormBuilder, FormArray, Validators, FormControl,AbstractControl } from '@angular/forms';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { AccountService, AlertService } from '@app/_services';
import { ConfirmPasswordValidator } from "@app/utils/confirm-password.validator"
import { PasswordStrengthValidator } from "@app/utils/password-strength.validators"

@Component({
  selector: 'app-resetpasswordaftermail-link',
  templateUrl: './resetpasswordaftermail-link.component.html',
  styleUrls: ['./resetpasswordaftermail-link.component.less']
})
export class ResetpasswordaftermailLinkComponent implements OnInit {
  Resetform: FormGroup;
  submitted = false;
  token:any;
  email:any;
  loading = false;
  Resetdone=false;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private accountService: AccountService,
    private alertService: AlertService

  ) { }

  ngOnInit(): void {
debugger;
      this.token = this.route.snapshot.paramMap.get('token');
      this.email = this.route.snapshot.paramMap.get('email');
      this.Resetformgroup();
  }


  Resetformgroup()
  {
    this.Resetform = this.fb.group(
      {

        password: [null, Validators.compose([
          Validators.required, Validators.minLength(8), PasswordStrengthValidator])],
        confirmPassword: ['', Validators.required],

      },
      {
        validator: ConfirmPasswordValidator("password", "confirmPassword")
      }
    );
  }


  get f(): { [key: string]: AbstractControl } {
    return this.Resetform.controls;
  }

  onSubmit(): void {
    debugger;
    this.submitted = true;

    if (this.Resetform.invalid) {
      return;
    }

    this.resetpasswordCallApi(this.Resetform.value);
    // console.log(JSON.stringify(this.Resetform.value, null, 2));
  }


  resetpasswordCallApi(data)
  {
    var model={
      "Password": data.password,
      "ConfirmPassword": data.confirmPassword,
      "Email": this.email ,
      "Token": this.token ,

    }

    this.accountService.ResetPassword(model)
    .subscribe(
    (data: any) => {

debugger
this.Resetdone=true;

    },
    error => {

    });
  }

}
