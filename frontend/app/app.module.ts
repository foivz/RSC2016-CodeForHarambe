import { NgModule }       from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { FormsModule }    from '@angular/forms';
import { AppComponent }        from './app.component';
import {HttpModule, JsonpModule} from "@angular/http";
import {DashboardComponent} from './dashboard.component';
import {FrontpageComponent} from './frontpage.component';
import {EventsComponent} from './events.component';
import {EventCreateComponent} from './event-create.component';
import {EventDetailComponent} from './event-detail-component';
import {FlowComponent} from './flow.component'
import {ResultsComponent} from './results.component'

import { AppRoutingModule }     from './app-routing.module';
import {EventService} from "./event.service";
import {TeamService} from "./team.service";
import {QuestionService} from "./question.service";
import {AnswerService} from "./answer.service";
import {TeamAnswerService} from "./teamanswer.service";

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    JsonpModule,
    AppRoutingModule
  ],
  declarations: [
    AppComponent,
    DashboardComponent,
    FrontpageComponent,
      EventsComponent,
      EventCreateComponent,
      EventDetailComponent,
      FlowComponent,
      ResultsComponent,
  ],
  providers: [
      EventService,
      TeamService,
      QuestionService,
      AnswerService,
      TeamAnswerService
  ],
  bootstrap: [ AppComponent ]
})

export class AppModule {
}


