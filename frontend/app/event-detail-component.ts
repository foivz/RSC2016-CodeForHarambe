import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params }   from '@angular/router';
import { Location }                 from '@angular/common';
import './rxjs-operators';
import { EventService } from './event.service';
import { QuestionService } from './question.service';
import {AnswerService} from './answer.service';
import {Question} from "./question";
import {Event} from "./event"
import {Answer} from "./answer";

@Component({
  moduleId: module.id,
  selector: 'my-event-detail',
  templateUrl: 'event-detail.component.html',
  styleUrls: ['css/main.css', 'css/theme.css']
})
export class EventDetailComponent implements OnInit {
  @Input()
  event: Event;
  questions: Question[];
  questionAnswers: Answer[];

  constructor(
    private eventService: EventService,
    private questionService: QuestionService,
    private answerService: AnswerService,
    private route: ActivatedRoute,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.route.params
      .switchMap((params: Params) => this.eventService.getEvent(+params['id']))
      .subscribe(event => {
          this.event = event[0];
          this.questionAnswers = [];

          this.route.params
              .switchMap((params: Params) => this.questionService.getEventQuestions(this.event.id))
              .subscribe(questions => {
                  this.questions = questions;

                  let index = 0;
                  for (let question of questions) {
                      question.answers = [];

                      this.route.params.switchMap((params: Params) => this.answerService.getQuestionAnswers(question.id))
                          .subscribe(answers => question.answers = answers);
                      index++;
                  }
                });
      });
  }

  save(): void {
    this.eventService.update(this.event);

    for (let question of this.questions) {
        this.questionService.update(question);

        for (let answer of question.answers) {
            this.answerService.update(answer);
        }
    }
  }

  addQuestion(): void {
      this.route.params
          .switchMap((params: Params) => this.questionService.create(1, '', 10, this.event.id))
          .subscribe(question => {
              question[0].answers = [];
              this.questions.push(question[0])
          });
  }

    addAnswer(id: number): void {
        this.route.params
            .switchMap((params: Params) => this.answerService.create(id, '', 0))
            .subscribe(answer => {
                for (let question of this.questions) {
                    if(question.id==id) {
                        question.answers.push(answer[0]);
                    }
                }
            });
    }

    deleteQuestion(id: number): void {
      this.questionService.remove(id).then(() => {
          this.questions.forEach((question, index) => {
              if(question.id==id) {
                  this.questions.splice(index, 1);
              }
          });
      });
    }

    deleteAnswer(id: number, qid: number): void {
        this.answerService.remove(id).then(() => {
            this.questions.forEach((question) => {
                if(question.id==qid) {
                    question.answers.forEach((answer, i) => {
                        if(answer.id==id) {
                            question.answers.splice(i, 1);
                        }
                    });
                }
            });
        });
    }

  goBack(): void {
    this.location.back();
  }
}
