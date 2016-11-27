import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard.component';
import {FrontpageComponent} from './frontpage.component';
import {EventsComponent} from './events.component';
import {EventCreateComponent} from './event-create.component'
import {EventDetailComponent} from './event-detail-component'
import {FlowComponent} from './flow.component'

const routes: Routes = [
    //{ path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: '',  component: FrontpageComponent },
    { path: 'dashboard/:id',  component: DashboardComponent },
    { path: 'dashboard/flow/:id',  component: FlowComponent },
    { path: 'events',  component: EventsComponent },
    { path: 'events/create', component: EventCreateComponent },
    { path: 'events/detail/:id', component: EventDetailComponent },
];
@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
