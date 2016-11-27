import {Component, OnInit, Input} from '@angular/core';

import { Event } from './event';
import { EventService } from './event.service';
import {Router, ActivatedRoute, Params} from "@angular/router";
import {TeamService} from "./team.service";
import {Team} from "./team";


@Component({
  moduleId: module.id,
  selector: 'my-dashboard',
  templateUrl: 'dashboard.component.html',
  styleUrls: ['css/main.css', 'css/dashboard.css']
})

export class DashboardComponent implements OnInit {
    @Input()
    event: Event;
    teams: Team[] = [];

  constructor(
      private eventService: EventService,
      private teamService: TeamService,
      private router: Router,
      private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
      this.route.params.switchMap((params: Params) => this.eventService.getEvent(+params['id']))
            .subscribe(event => {
                this.event = event[0];

                this.teamService.getEventTeams(this.event.id)
                    .subscribe(teams => {
                        this.teams = teams;

                        for (let team of this.teams) {
                            this.teamService.getTeamUsersCount(team.id)
                                .subscribe(count => team.userCount = count[0].count)
                        }
                    });
            });
  }

    gotoEvents(): void {
        this.router.navigate(['/events']);
    }

    gotoFlow(id: number): void {
        this.router.navigate(['/dashboard/flow/'+id]);
    }
}

