import { Injectable }     from '@angular/core';
import {Http, Response, URLSearchParams, Headers} from '@angular/http';
import { Team }           from './team';
import { Observable }     from 'rxjs/Observable';

@Injectable()
export class TeamService {
    private eventsUrl = 'http://rsc-harambe.azurewebsites.net/api';  // URL to web API
    private headers = new Headers({'Content-Type': 'application/x-www-form-urlencoded'});
    constructor (private http: Http) {}

    getTeams (): Observable<Team[]> {
        let params = new URLSearchParams();
        params.set('format', 'json');
        params.set('callback', 'JSONP_CALLBACK');

        return this.http.get(this.eventsUrl+'/teams', {headers: this.headers})
            .map(this.extractData)
            .catch(this.handleError);
    }

    getEventTeams(id: number): Observable<Team[]> {
        let params = new URLSearchParams();
        params.set('format', 'json');
        params.set('callback', 'JSONP_CALLBACK');

        return this.http.get(this.eventsUrl+'/eventteams/'+id, {headers: this.headers})
            .map(this.extractData)
            .catch(this.handleError);
    }

    getTeamUsersCount (id: number): Observable<any> {
        let params = new URLSearchParams();
        params.set('format', 'json');
        params.set('callback', 'JSONP_CALLBACK');

        return this.http.get(this.eventsUrl+'/membercount/'+id, {headers: this.headers})
            .map(this.extractData)
            .catch(this.handleError);
    }

    getTeam (id: number): Observable<Team> {
        let params = new URLSearchParams();
        params.set('format', 'json');
        params.set('callback', 'JSONP_CALLBACK');

        return this.http.get(this.eventsUrl+'/teams/'+id, {headers: this.headers})
            .map(this.extractData)
            .catch(this.handleError);
    }

    create(name: string, location: string, date: string): any {
        const url = this.eventsUrl+'/teams';

        return this.http
            .post(url, JSON.stringify({action: 'create', 'data': [{name: name, location: location, date: date}]}), {headers: this.headers})
            .toPromise()
            .catch(this.handleError);
    }

    remove(id: number): any {
        const url = this.eventsUrl+'/teams/'+id;

        return this.http
            .delete(url, {headers: this.headers})
            .toPromise()
            .catch(this.handleError);
    }

    update(event: Team): any {
        const url = this.eventsUrl+'/teams';

        return this.http
            .post(url, JSON.stringify({action: 'update', 'data': [event]}), {headers: this.headers})
            .toPromise()
            .then(() => event)
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
