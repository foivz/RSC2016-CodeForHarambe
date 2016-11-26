import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params }   from '@angular/router';
import { Location }                 from '@angular/common';
import './rxjs-operators';
import { EventService } from './event.service';

@Component({
  moduleId: module.id,
  selector: 'my-event-create',
  templateUrl: 'event-create.component.html',
  styleUrls: ['css/main.css', 'css/dashboard.css', 'css/theme.css']
})
export class EventCreateComponent implements OnInit {
  @Input()
    name: string;
    eDesc: string;
    eDate: string;
    loc: string;
    prize: string;
    teamsize: number;
    eStatus: number;

  constructor(
    private eventService: EventService,
    private route: ActivatedRoute,
    private location: Location
  ) {}

  ngOnInit(): void {
  }

  save(): void {
    this.eventService.create(this.name, this.eDesc, this.eDate, this.loc, this.prize, this.teamsize, this.eStatus)
        .then(() => this.goBack());
  }

  goBack(): void {
    this.location.back();
  }
}
