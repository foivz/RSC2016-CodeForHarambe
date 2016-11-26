import { Component, OnInit } from '@angular/core';

import { Event } from './event';
import { EventService } from './event.service';


@Component({
  moduleId: module.id,
  selector: 'my-dashboard',
  templateUrl: 'dashboard.component.html',
  styleUrls: ['css/main.css', 'css/dashboard.css', 'css/font-awesome.min.css']
})

export class DashboardComponent implements OnInit {

  events: Event[] = [];

  constructor(private eventService: EventService) { }

  ngOnInit(): void {
    //this.heroService.getEvents()
    //  .subscribe(events => this.events = events.slice(1, 5));
  }
}

