import { Component, ElementRef, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Account } from '@app/_models';
import { AccountService, AlertService } from '@app/_services';

import { AddgroupService } from '../_services/addgroup.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-add-group',
  templateUrl: './add-group.component.html',
  styleUrls: ['./add-group.component.less']
})
export class AddGroupComponent implements OnInit {
  AddGroupForm: FormGroup;

  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private modalService: NgbModal,
    private confirmModalService: BsModalService,
    private alertService: AlertService,
    private route: ActivatedRoute,

    private addgroupService:AddgroupService,
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
  isValid:boolean = true;
  column: string = 'userName';
  modalcloseOpen: any;
  //paging
  name = 'Angular';
  page = 1;
  pageSize = 10;
  loading = false;
  submitted = false;

  Grouplist=[];
deleteitem:any=0;
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
    this.CreateGroupFrom();
    this.Getgrouplist();
  }

  // function for creating from
CreateGroupFrom() {
  this.AddGroupForm = new FormGroup({
    groupName: this.fb.control('', Validators.required)
  });
}

// function for open model pop up
openModal(template: TemplateRef<any>,itemid:any) {
  debugger;
  this.deleteitem=itemid;
  this.modalRef = this.confirmModalService.show(template, { class: 'modal-dialog-centered' });
}

openAddGroupModel(targetModal) {
  this.modalcloseOpen = this.modalService.open(targetModal, {
    centered: true,
    backdrop: 'static',
    size: 'md'
  });
}

ValidTextBox(event: KeyboardEvent) {
    this.isValid = true;
  }
  onSubmit() {
    if (this.AddGroupForm.invalid) {
      this.isValid = false;
      return;
    } else {
      var modelGroup = {
        "GroupName": this.AddGroupForm.controls.groupName.value
      }
      this.addgroupService.Saveaddgroup(modelGroup)
      .subscribe(
        (data: any) => {
          this.Getgrouplist();
          alert("data saved")
        // this.Grouplist = data;
        },
        error => {
        });
      this.loading = true;
      this.isValid = true;
    }


  }


// function for confirm  delete
confirm(): void {
  debugger;
this.deleteitem;
this.addgroupService.Deletegrouplist(this.deleteitem)
  .subscribe(
    (data) => {
      debugger;
      this.Getgrouplist();
    },
    error => {
    });

  this.modalRef.hide();
  
}
decline(): void {
  this.modalRef.hide();
}

displayStyle = "none";
  
openPopup() {
  this.displayStyle = "block";
}
closePopup() {
  this.displayStyle = "none";
}


//Calling get list of group on page load

Getgrouplist()
{  debugger;
  this.addgroupService.Getgrouplist()
  .subscribe(
    (data: any) => {
      debugger;
     this.Grouplist = data;
    },
    error => {
    });
}

}