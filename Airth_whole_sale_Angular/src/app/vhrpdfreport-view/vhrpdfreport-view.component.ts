import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vhrpdfreport-view',
  templateUrl: './vhrpdfreport-view.component.html',
  styleUrls: ['./vhrpdfreport-view.component.less']
})
export class VhrpdfreportViewComponent implements OnInit {
  pdfSource =  "https://vadimdez.github.io/ng2-pdf-viewer/assets/pdf-test.pdf";
  constructor() { }

  ngOnInit(): void {
  }

}
