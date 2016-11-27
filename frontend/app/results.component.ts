import { Component, Input, OnInit } from '@angular/core';
import {ActivatedRoute, Params, Router}   from '@angular/router';
import { Location }                 from '@angular/common';
import './rxjs-operators';
import { EventService } from './event.service';
import { TeamService } from './team.service';
import { AnswerService } from './answer.service';
import { TeamAnswerService } from './teamanswer.service';
import { QuestionService } from './question.service';
import {Team} from "./team";

@Component({
    moduleId: module.id,
    selector: 'my-results',
    templateUrl: 'results.component.html',
    styleUrls: ['css/main.css', 'css/dashboard.css', 'css/theme.css']
})
export class ResultsComponent implements OnInit {
    @Input()
    team: Team;

    constructor(
        private eventService: EventService,
        private teamService: TeamService,
        private taService: TeamAnswerService,
        private questionService: QuestionService,
        private answerService: AnswerService,
        private route: ActivatedRoute,
        private router: Router,
        private location: Location
    ) {}

    ngOnInit(): void {
        this.route.params
            .switchMap((params: Params) => this.teamService.getTeam(+params['id']))
            .subscribe(team => {
                this.team = team[0];

                this.taService.getTeamAnswers(this.team.id).
                    subscribe(answers => this.team.answers = answers);
            });
    }

    gotoEvents(): void {
        this.router.navigate(['/events']);
    }

    save(): void {
        /*this.eventService.create(this.name, this.eDesc, this.eDate, this.loc, this.prize, this.teamsize, this.eStatus)
            .subscribe(answer => this.router.navigate(['/events/detail/'+answer[0].id]));*/
    }

    goBack(): void {
        this.location.back();
    }
}
