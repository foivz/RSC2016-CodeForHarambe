import {Answer} from "./answer";

export class Question {
    id: number;
    qType: number;
    qText: string;
    qTime: number;
    eventID: number;
    answers: Answer[];
}
