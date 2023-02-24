
﻿import { NgModule, APP_INITIALIZER } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule,FormsModule  } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

// used to create fake backend
import { fakeBackendProvider } from './_helpers';

import { AppRoutingModule } from './app-routing.module';
import { JwtInterceptor, ErrorInterceptor, appInitializer } from './_helpers';
import { AccountService } from './_services';
import { AppComponent } from './app.component';
import { AlertComponent } from './_components';
import { HomeComponent } from './home';;
import { CarfexDashboardComponent } from './carfex-dashboard/carfex-dashboard.component'
;
import { HeaderComponent } from './header/header.component'
;
import { FooterComponent } from './footer/footer.component'
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { Ng2OrderModule } from 'ng2-order-pipe';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component'
;
import { UserListComponent } from './user-list/user-list.component'
;
import { CarfaxapiListComponent } from './carfaxapi-list/carfaxapi-list.component';
import { VhrReportComponent } from './vhr-report/vhr-report.component';;
import { NgbModule } from '@ng-bootstrap/ng-bootstrap'
import { NgxLoaderModule } from '@tusharghoshbd/ngx-loader';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { ModalModule, BsModalService } from 'ngx-bootstrap/modal';
import { VhrpdfreportViewComponent } from './vhrpdfreport-view/vhrpdfreport-view.component';;
import { SignUpComponent } from './sign-up/sign-up.component'
;
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component'

import { HashLocationStrategy, LocationStrategy } from '@angular/common';;
import { ResetpasswordaftermailLinkComponent } from './resetpasswordaftermail-link/resetpasswordaftermail-link.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { JwtModule } from "@auth0/angular-jwt";;
import { IicBatchComponent } from './iic-batch/iic-batch.component'
import { NgImageSliderModule } from 'ng-image-slider';
import { JwPaginationModule } from 'jw-angular-pagination';
import { AgmCoreModule } from '@agm/core';
import { AgmOverlays } from 'agm-overlays';;
import { DealerLocationComponent } from './dealer-location/dealer-location.component';
import { MasterDashboardComponent } from './master-dashboard/master-dashboard.component';
import { ChartsModule } from 'ng2-charts';
import { ApicallHistoryComponent } from './apicall-history/apicall-history.component';
import { NgxSpinnerModule } from 'ngx-spinner';
//function is use to get jwt token from local storage
export function tokenGetter() {
  return localStorage.getItem("jwt");
}
import { FilterPipe} from '../app/filters/filter.pipe';
import { VehicleDetailsComponent } from './vehicle-details/vehicle-details.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { SideNavComponent } from "./side-nav/side-nav.component";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';;
import { AddGroupComponent } from './add-group/add-group.component';
import { AddUserrolesComponent } from './add-userroles/add-userroles.component';;
import { ListPostionsComponent } from './list-postions/list-postions.component'

import { ListWholesalersComponent } from './list-wholesalers/list-wholesalers.component';
import { AddRepsComponent } from './add-reps/add-reps.component'
@NgModule({
    declarations: [
        AppComponent,
        AlertComponent,
        HomeComponent,
        CarfexDashboardComponent,
        FooterComponent,
        VehicleListComponent,
        UserListComponent,
        CarfaxapiListComponent,
        VhrReportComponent,
        VhrpdfreportViewComponent,
        SignUpComponent,
        ForgotPasswordComponent,
        ResetpasswordaftermailLinkComponent,
        UserProfileComponent,
        IicBatchComponent,
        DealerLocationComponent,
        MasterDashboardComponent,
        FilterPipe,
        ApicallHistoryComponent,
        VehicleDetailsComponent,
        SideNavComponent,
        HeaderComponent
,
        AddGroupComponent
,
        AddUserrolesComponent ,
        ListPostionsComponent ,
        ListWholesalersComponent ,
        AddRepsComponent    
    ],
    providers: [
        // { provide: APP_INITIALIZER, useFactory: appInitializer, multi: true, deps: [AccountService] },
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
        { provide: LocationStrategy, useClass: HashLocationStrategy },
        // provider used to create fake backend
        fakeBackendProvider,
        BsModalService
    ],
    bootstrap: [AppComponent],
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        FormsModule,
        NgxSpinnerModule,
        HttpClientModule,
        AppRoutingModule,
        Ng2SearchPipeModule,
        JwPaginationModule,
        ModalModule.forRoot(),
        Ng2OrderModule,
        ChartsModule,
        NgxLoaderModule,
        PdfViewerModule,
        NgSelectModule,
        NgImageSliderModule,
        NgxPaginationModule,
        NgbModule,
        AgmOverlays,
        BrowserAnimationsModule,
        AgmCoreModule.forRoot({
            // please get your own API key here:
            // https://developers.google.com/maps/documentation/javascript/get-api-key?hl=en
            apiKey: 'AIzaSyAjb57aQQNvA0qmT4zAt4mopp-zeEZyNKc',
            language: "en",
            libraries: ['visualization', 'places', 'drawing', 'geometry']
        }),
      
    ]
})
export class AppModule { }

