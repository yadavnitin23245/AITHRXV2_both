import { Component, ElementRef, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AccountService, AlertService } from '@app/_services';
import { Account } from '@app/_models';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of, from } from 'rxjs';
import { map, every, pluck, filter, mergeMap, toArray, concatMap } from 'rxjs/operators';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.less']
})
export class UserListComponent implements OnInit {
  UserListForm: FormGroup;
  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private confirmModalService: BsModalService,
    private alertService: AlertService,
    private route: ActivatedRoute,
    private router: Router,) { }
  deleting = false;
  modalRef: BsModalRef;
  // for getting confirm box from HTML by Id
  @ViewChild('ConfirmBox', { static: false }) ConfirmBoxclick: ElementRef;

  // sorting
  key = 'userName'; // set default
  reverse = false;
  sortedUsers: any;
  DeleteUserid: any;
  accounts: Account[] = [];
  isDesc: boolean = false;
  column: string = 'userName';

  //paging
  name = 'Angular';
  page = 1;
  pageSize = 10;


  // function for column sorting
  sort(property) {
    this.isDesc = !this.isDesc; //change the direction
    this.column = property;
    let direction = this.isDesc ? 1 : -1;

    this.accounts.sort(function (a, b) {
      if (a[property] < b[property]) {
        return -1 * direction;
      }
      else if (a[property] > b[property]) {
        return 1 * direction;
      }
      else {
        return 0;
      }
    });
  };
  searchText;
  ngOnInit(): void {
    this.Createform();
    // For Getting User list
    this.GetUserList();
  }

  // functiono for creating from
  Createform() {
    this.UserListForm = this.fb.group({
    }, {

    });
  }

  // function for getting user list
  GetUserList() {
    this.accountService.getUserList()
      .subscribe(
        (data: any) => {
          // set data to model for show in list.
          this.accounts = data;
        },
        error => {
        });
  }
  // function for delete
  onDelete(UserId: any) {
    this.DeleteUserid = UserId;
    let el: HTMLElement = this.ConfirmBoxclick.nativeElement;
    el.click();
  }
  // function for open model pop up
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.confirmModalService.show(template, { class: 'modal-dialog-centered' });
  }

  // function for confirm 
  confirm(): void {
    this.modalRef.hide();
    this.deleteUser(this.DeleteUserid);
  }
  decline(): void {
    this.modalRef.hide();
  }

  // function for delete users
  deleteUser(email: any) {
    this.accountService.deleteUsers(email)
      .subscribe(
        (data: any) => {
          debugger;
          this.alertService.success("User deleted Successfully");
          this.GetUserList();
          return this.router.navigateByUrl("/List");
        },
        (error: any) => {
          this.alertService.error("error");
        }
      );
  }

}
