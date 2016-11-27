import {TeamAnswer} from "./teamanswer";
export class Team {
    id: number;
    name: string;
    eventID: number;
    userCount: number;
    answers: TeamAnswer[];
}
