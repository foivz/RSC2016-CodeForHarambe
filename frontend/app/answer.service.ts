import { Injectable }     from '@angular/core';
import {Http, Response, URLSearchParams, Headers} from '@angular/http';
import { Answer }           from './answer';
import { Observable }     from 'rxjs/Observable';

@Injectable()
export class AnswerService {
    private eventsUrl = 'http://rsc-harambe.azurewebsites.net/api';  // URL to web API
    private headers = new Headers({'Content-Type': 'application/x-www-form-urlencoded'});
    constructor (private http: Http) {}

    getAnswers (): Observable<Answer[]> {
        let params = new URLSearchParams();
        params.set('format', 'json');
        params.set('callback', 'JSONP_CALLBACK');

        return this.http.get(this.eventsUrl+'/answers', {headers: this.headers})
            .map(this.extractData)
            .catch(this.handleError);
    }

    getAnswer (id: number): Observable<Answer> {
        let params = new URLSearchParams();
        params.set('format', 'json');
        params.set('callback', 'JSONP_CALLBACK');

        return this.http.get(this.eventsUrl+'/answers/'+id, {headers: this.headers})
            .map(this.extractData)
            .catch(this.handleError);
    }

    getQuestionAnswers (id: number): Observable<Answer[]> {
        let params = new URLSearchParams();
        params.set('format', 'json');
        params.set('callback', 'JSONP_CALLBACK');

        return this.http.get(this.eventsUrl+'/answersquery/'+id, {headers: this.headers})
            .map(this.extractData)
            .catch(this.handleError);
    }

    create(questionID: number, aText: string, isCorrect: number): Observable<Answer> {
        const url = this.eventsUrl+'/answers';

        return this.http
            .post(url, JSON.stringify({action: 'create', 'data': [{questionID: +questionID, aText: aText, isCorrect: +isCorrect}]}), {headers: this.headers})
            .map(this.extractData)
            .catch(this.handleError);
    }

    remove(id: number): any {
        const url = this.eventsUrl+'/answers/'+id;

        return this.http
            .post(url, JSON.stringify({action: 'delete', id: id}), {headers: this.headers})
            .toPromise()
            .catch(this.handleError);
    }

    update(answer: Answer): any {
        const url = this.eventsUrl+'/answers';

        return this.http
            .post(url, JSON.stringify({action: 'update', 'data': [answer]}), {headers: this.headers})
            .toPromise()
            .then(() => answer)
            .catch(this.handleError);
    }

    private extractData(res: Response) {
        let body = res.json();
        return body || { };
    }

    private handleError (error: Response | any) {
        // In a real world app, we might use a remote logging infrastructure
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Observable.throw(errMsg);
    }
}
