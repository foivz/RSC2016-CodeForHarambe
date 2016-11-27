import { Injectable }     from '@angular/core';
import {Http, Response, URLSearchParams, Headers} from '@angular/http';
import { Question }           from './question';
import { Observable }     from 'rxjs/Observable';

@Injectable()
export class QuestionService {
    private eventsUrl = 'http://rsc-harambe.azurewebsites.net/api';  // URL to web API
    private headers = new Headers({'Content-Type': 'application/x-www-form-urlencoded'});
    constructor (private http: Http) {}

    getQuestions (): Observable<Question[]> {
        let params = new URLSearchParams();
        params.set('format', 'json');
        params.set('callback', 'JSONP_CALLBACK');

        return this.http.get(this.eventsUrl+'/questions', {headers: this.headers})
            .map(this.extractData)
            .catch(this.handleError);
    }

    getQuestion (id: number): Observable<Question> {
        let params = new URLSearchParams();
        params.set('format', 'json');
        params.set('callback', 'JSONP_CALLBACK');

        return this.http.get(this.eventsUrl+'/questions/'+id, {headers: this.headers})
            .map(this.extractData)
            .catch(this.handleError);
    }

    getEventQuestions (id: number): Observable<Question[]> {
        let params = new URLSearchParams();
        params.set('format', 'json');
        params.set('callback', 'JSONP_CALLBACK');

        return this.http.get(this.eventsUrl+'/questionsquery/'+id, {headers: this.headers})
            .map(this.extractData)
            .catch(this.handleError);
    }

    create(qType: number, qText: string, qTime: number, eventID: number): Observable<Question> {
        const url = this.eventsUrl+'/questions';

        return this.http
            .post(url, JSON.stringify({action: 'create', 'data': [{qType: +qType, qText: qText, qTime: +qTime, eventID: +eventID}]}), {headers: this.headers})
            .map(this.extractData)
            .catch(this.handleError);
    }

    remove(id: number): any {
        const url = this.eventsUrl+'/questions';

        return this.http
            .post(url, JSON.stringify({action: 'delete', id: id}), {headers: this.headers})
            .toPromise()
            .catch(this.handleError);
    }

    update(question: Question): any {
        const url = this.eventsUrl+'/questions';

        return this.http
            .post(url, JSON.stringify({action: 'update', 'data': [question]}), {headers: this.headers})
            .toPromise()
            .then(() => question)
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
