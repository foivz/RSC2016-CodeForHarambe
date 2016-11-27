import { Component, OnInit } from '@angular/core';
import { Event } from './event';
import { EventService } from './event.service';
import './rxjs-operators';
import {Router} from "@angular/router";

@Component({
    moduleId: module.id,
    selector: 'my-events',
    templateUrl: 'events.component.html',
    styleUrls: ['css/main.css', 'css/dashboard.css', 'css/theme.css'],
    providers: [EventService]
})

export class EventsComponent implements OnInit {
    title = 'Events';
    events: Event[];
    errorMessage: any;
    selectedEvent: Event;
    constructor(
        private eventService: EventService,
        private router: Router
    ) { }

    getEvents(): void {
        this.eventService.getEvents().subscribe(
            events => {
                this.events = events;

                for (let event of this.events) {
                    this.eventService.getEventUsersCount(event.id).subscribe(
                        count => event.usersCount = +count[0].count
                    );
                }
            },
            error =>  this.errorMessage = <any>error
        );
    }
    ngOnInit(): void {
        this.getEvents();
    }

    onSelect(event: Event): void {
        this.selectedEvent = event;
    }

    gotoDashboard(id: number): void {
        this.router.navigate(['/dashboard/'+id]);
    }

    gotoEvents(): void {
        this.router.navigate(['/events']);
    }

    gotoDetail(id: number): void {
        this.router.navigate(['/events/detail/', id]);
    }

    gotoCreate(): void {
        this.router.navigate(['/events/create']);
    }

    notify(id: number): void {
        this.eventService.notify(id);
    }

    remove(id: number): void {
        this.eventService.remove(id)
            .then(() => this.router.navigate(['/events']));
    }

    deleteEvent(id: number): void {
        this.eventService.remove(id).then(() => {
            this.events.forEach((event, index) => {
                if(event.id==id) {
                    this.events.splice(index, 1);
                }
            });
        });
    }

}
