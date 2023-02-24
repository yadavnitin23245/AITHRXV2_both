import {
  Component,
  OnInit,
  VERSION,
  NgZone,
  ViewChild,
  ElementRef
} from "@angular/core";
import { MouseEvent,LatLngLiteral } from '@agm/core';
import { IccbatchService } from "@app/_services/iccbatch.service";
declare var google: any;
@Component({
  selector: 'app-dealer-location',
  templateUrl: './dealer-location.component.html',
  styleUrls: ['./dealer-location.component.less']
})
export class DealerLocationComponent implements OnInit {

  constructor(private iccbatchService: IccbatchService) { }

 // google maps zoom level
zoom: number = 4;
dealerName:any;
// initial center position for the map
lat: number = 50.000000;
lng: number = -85.000000;
dealerLoctaions: dealerLoctaion[] = [];

ngOnInit(): void {
this.GetDealerInformations();
}

// for getting Vehicle List Of Available Offer

GetDealerInformations() {
  this.iccbatchService.getDealerInformations()
    .subscribe(
      (data: any) => {
        for (var i = 0; i < data.length; i++) {
          this.dealerLoctaions.push(
            {
              lat: data[i].lattitude,
              lng: data[i].longitude,
              label: data[i].dealerName,
               draggable: true,
               content:"This is testing",
               color:"Red",
               iconUrl:"",
               Make:data[i].make,
               VehicleCount:data[i].vehicleCount,
               Address:data[i].address
              }


            )
          this.dealerName = data[i].dealerName;
        }
      },
      error => {
      });
}


clickedMarker(label: string, index: number) {
  console.log(`clicked the marker: ${label || index}`)
}

// mapClicked($event: MouseEvent) {
//   this.dealerLoctaions.push({
//     lat: $event.coords.lat,
//     lng: $event.coords.lng,
//     draggable: true,
//     content: "This is testing",
//     color: "red",
//     iconUrl: "http://maps.google.com/mapfiles/ms/micons/police.png",
//     Make:'test',
//     VehicleCount:2

//   });
// }

markerDragEnd(m: dealerLoctaion, $event: MouseEvent) {
  console.log('dragEnd', m, $event);
}
}

// just an interface for type safety.
interface dealerLoctaion {
lat: number;
lng: number;
label?: string;
draggable: boolean;
content: string;
color: string;
iconUrl: string;
VehicleCount:number;
Make:string;
Address:string
}
