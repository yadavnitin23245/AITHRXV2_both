import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home';
import { AuthGuard } from './_helpers';
import { Role } from './_models';
import { CarfexDashboardComponent } from './carfex-dashboard/carfex-dashboard.component';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { AppComponent } from './app.component';
import { UserListComponent } from './user-list/user-list.component';
import { CarfaxapiListComponent } from './carfaxapi-list/carfaxapi-list.component';
import { VhrReportComponent } from './vhr-report/vhr-report.component';
import { LoginComponent } from './account/login.component';
import { VhrpdfreportViewComponent } from './vhrpdfreport-view/vhrpdfreport-view.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetpasswordaftermailLinkComponent } from './resetpasswordaftermail-link/resetpasswordaftermail-link.component'
import { UserProfileComponent } from './user-profile/user-profile.component';
import { IicBatchComponent } from './iic-batch/iic-batch.component';
import { DealerLocationComponent } from './dealer-location/dealer-location.component';
import { MasterDashboardComponent } from './master-dashboard/master-dashboard.component';
import { ApicallHistoryComponent } from './apicall-history/apicall-history.component';
import { VehicleDetailsComponent } from './vehicle-details/vehicle-details.component';
import { AddGroupComponent } from './add-group/add-group.component';
import { AddUserrolesComponent } from './add-userroles/add-userroles.component';
import { ListPostionsComponent } from './list-postions/list-postions.component';
import { ListWholesalersComponent } from './list-wholesalers/list-wholesalers.component';
import { AddRepsComponent } from './add-reps/add-reps.component'
const accountModule = () => import('./account/account.module').then(x => x.AccountModule);
const adminModule = () => import('./admin/admin.module').then(x => x.AdminModule);
const profileModule = () => import('./profile/profile.module').then(x => x.ProfileModule);

const routes: Routes = [
    { path: 'ResetpasswordMail/:token/:email', component: ResetpasswordaftermailLinkComponent},
    { path: '', component: LoginComponent },
    { path: 'Home', component: HomeComponent },
    { path: 'account', loadChildren: accountModule },
    { path: 'profile', loadChildren: profileModule },
    { path: 'admin', loadChildren: adminModule, canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
    { path: 'admin', loadChildren: adminModule, canActivate: [AuthGuard] },
    { path: 'Carfex-portal', component: CarfexDashboardComponent, data: { title: 'Carfex - Portal' } },
    { path: 'main', component: AppComponent, data: { title: 'Home' } },
    { path: 'Vehicle-list', component: VehicleListComponent,data: { title: 'Vehicle-list' } },
    { path: 'user-profile/:userName', component: UserProfileComponent, data: { title: 'Aithr - Edit-profile' } },
    { path: 'List', component: UserListComponent,canActivate: [AuthGuard],data: { title: 'user-list' } },
    { path: 'Carfax-API', component: CarfaxapiListComponent, data: { title: 'Carfax - API' } },
    { path: 'Vhr-Report', component: VhrReportComponent, data: { title: 'VHR - Report' } },
    { path: 'pdf-Report', component: VhrpdfreportViewComponent, data: { title: 'VHR - Report' } },
    { path: 'register', component: SignUpComponent, data: { title: 'register' } },
    { path: 'Forgot-Password', component: ForgotPasswordComponent, data: { title: 'Forgot-Password' } },
    { path: 'icc-batch', component: IicBatchComponent, data: { title: 'icc-batch' } },
    { path: 'DealerLocation', component: DealerLocationComponent, data: { title: 'dealer locations' } },
    { path: 'master-dashboard', component: MasterDashboardComponent, data: { title: 'master dashboard' } },
    { path: 'api-history', component: ApicallHistoryComponent, data: { title: 'api-history' } },
    { path: 'vehicle-details/:vehicleId', component: VehicleDetailsComponent, data: { title: 'api-history' } },
    { path: 'add-groups', component: AddGroupComponent, data: { title: 'add -groups' } },
    { path: 'add-roles', component: AddUserrolesComponent, data: { title: 'add -roles' } },
    { path: 'add-postions', component: ListPostionsComponent, data: { title: 'add-postions' } },
    { path: 'add-Wholesalers', component: ListWholesalersComponent, data: { title: 'add-Wholesalers' } },
    { path: 'add-Reps', component: AddRepsComponent, data: { title: 'add-Reps' } },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
