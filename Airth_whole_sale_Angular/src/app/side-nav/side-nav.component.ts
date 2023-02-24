import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.less']
})
export class SideNavComponent implements OnInit {
  list =[
    {
      nunber:'1',
      name:'Profile',
      icon:'fa fa-user',
      link:'/user-profile/{{ account.email }}',
    },
    {
      nunber:'2',
      name:'Admin',
      icon:'fa fa-user',
      link:'/List',
    },
    {
      nunber:'3',
      name:'VHR Report',
      icon:'fa fa-file-text',
      link:'/Vhr-Report',
    },
    {
      nunber:'4',
      name:'Carfax API',
      icon:'fa fa-cog fa-solid fa-box',
      link:'/Carfax-API',
    },
    {
      nunber:'5',
      name:'Inventory',
      icon:'fa fa-sticky-note',
      link:'/Vhr-Report',
    },
    {
      nunber:'6',
      name:'Dashboard',
      icon:'fa fa-tachometer',
      link:'/master-dashboard',
    },
    {
      nunber:'7',
      name:'API Call History',
      icon:'fa fa-history',
      link:'/api-history',
    },
    {
      nunber:'8',
      name:'Dealer location',
      icon:'fa fa-map',
      link:'/DealerLocation',
    },

  ];
  constructor() { }

  ngOnInit(): void {
  }

}
