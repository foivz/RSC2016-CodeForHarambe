import {Component, OnInit, Input} from '@angular/core';

import { Event } from './event';
import { EventService } from './event.service';
import {Router, ActivatedRoute, Params} from "@angular/router";
import {TeamService} from "./team.service";
import {Team} from "./team";
import {QuestionService} from "./question.service";
import {Question} from "./question";


@Component({
    moduleId: module.id,
    selector: 'my-flow',
    templateUrl: 'flow.component.html',
    styleUrls: ['css/main.css', 'css/carousel.css']
})

export class FlowComponent implements OnInit {
    @Input()
    team: Team;
    questions: Question[] = [];

    constructor(
        private eventService: EventService,
        private questionService: QuestionService,
        private teamService: TeamService,
        private router: Router,
        private route: ActivatedRoute
    ) { }

    ngOnInit(): void {
        this.route.params.switchMap((params: Params) => this.teamService.getTeam(+params['id']))
            .subscribe(team => {
                this.team = team[0];

                this.questionService.getEventQuestions(this.team.eventID)
                    .subscribe(questions => this.questions = questions);
            });
    }

    nextQuestion(eid: number, qid: number): void {
        this.teamService.nextQuestion(eid, qid).then(
            () => {
                if(qid==0) {
                    this.router.navigate(['/events']);
                }
            });
    }
}

