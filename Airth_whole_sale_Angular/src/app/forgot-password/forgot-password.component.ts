import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';
import { AccountService, AlertService } from '@app/_services';
@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.less']
})

export class ForgotPasswordComponent implements OnInit {


 form: FormGroup;
  submitted = false;
  loading=false;
  sentmail=false;
  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,


    )
   {

   }

   ngOnInit(): void {
    this.form = this.formBuilder.group(
      {

        email: ['', [Validators.required, Validators.email]]
      }
    );
  }

  // convenience getter for easy access to form fields
  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  onSubmit(): void {
    debugger;
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }
    this.loading=true;

this.ForgetPassowrdFunction(this.form.value.email)

    console.log(JSON.stringify(this.form.value, null, 2));
  }

  //calling forget password api
  ForgetPassowrdFunction( email:string)
  {
   var model={


      "email": email


    }

    this.accountService.ForgotPasswordPost(model)
    .subscribe(
    (data: any) => {
debugger;
this.loading=false;
this.sentmail=true;

    },
    error => {

    });

  }

}
