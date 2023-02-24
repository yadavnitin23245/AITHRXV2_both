import { Component, ElementRef, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
@Component({
  selector: 'app-carfaxapi-list',
  templateUrl: './carfaxapi-list.component.html',
  styleUrls: ['./carfaxapi-list.component.less']
})
export class CarfaxapiListComponent implements OnInit {
  CarfaxapiForm: FormGroup;
  modalcloseOpen: any;
  modalRef: BsModalRef;
  @ViewChild('PdfMOdelPopUp', { static: false }) elclick: ElementRef;
  show = false;
  fullScreen = true;
  template = ``
  // form: any;
  submitted = false;

  show_i_o_error = false;
  pdfSource = '/assets/MobileReport.pdf'
  constructor(private fb: FormBuilder,
    private modalService: NgbModal,
    private modalServiceng: BsModalService,
    private router: Router

  ) { }

  ngOnInit(): void {
    this.Createform();
  }

  Refresh() {
    const VinNumberControl = this.CarfaxapiForm.get('VinNumberControl');
    VinNumberControl.setValue("");
  }

  onKeyUp(x) { // appending the updated value to the variable
    var value = x.target.value;
    const Icharchter = "i";
    const ocharchter = "o";
    debugger;
    // const regex =  RegEx(/.*[%]{1}.*[-]{1}/);
    // this.formulaType = regex.test(this.selectedSources[formula].Value) ? 'percent' : 'diff';

    var checki = value.indexOf(Icharchter);
    var checko = value.indexOf(ocharchter);

    if (checki != -1) {
      this.show_i_o_error = true;
      return;
    }
    else if (checko != -1) {
      this.show_i_o_error = true;
      return;
    }
    else {
      this.show_i_o_error = false;
    }
  }

  ShowLoader(modaltarget) {
    debugger;
    this.submitted = true;
    if (this.show_i_o_error == true) {
      return;
    }
    // stop here if form is invalid
    if (this.CarfaxapiForm.invalid) {
      return;
    }

    this.show = true;
    this.fullScreen = true;
    this.template = ``
    setTimeout(() => {
      this.show = false
    }, 2000);

    this.openpop(modaltarget);
  }

  openPDfDocumentViewer(targetModal, user) {
    this.modalService.open(targetModal, {
      centered: true,
      backdrop: 'static',
      size: 'xl'
    });
  }
  onClickDefault() {
    this.show = true;
    this.fullScreen = true;
    this.template = ``
    setTimeout(() => {
      this.show = false

      this.gotoReport();
    }, 1000);
  }
  gotoReport() {
    this.router.navigate(['/Vhr-Report']);
  }

  openModelPopUp(targetModal, user) {
    debugger;
    this.modalcloseOpen = this.modalService.open(targetModal, {
      centered: true,
      backdrop: 'static',
      size: 'lg'
    });
  }

  openpop(targetModal) {
    this.modalcloseOpen.dismiss();
    this.Refresh();
    this.modalRef = this.modalServiceng.show(targetModal);
  }


  get f() { return this.CarfaxapiForm.controls; }

  Createform() {
    this.CarfaxapiForm = this.fb.group({
      VinNumberControl: ['', [Validators.required, Validators.maxLength(17)]],

    }, {

    });
  }
}
