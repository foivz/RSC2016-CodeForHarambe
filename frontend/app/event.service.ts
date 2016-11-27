import { Injectable }     from '@angular/core';
import {Http, Response, URLSearchParams, Headers} from '@angular/http';
import { Event }           from './event';
import { Observable }     from 'rxjs/Observable';

@Injectable()
export class EventService {
  private eventsUrl = 'http://rsc-harambe.azurewebsites.net/api';  // URL to web API
  private headers = new Headers({'Content-Type': 'application/x-www-form-urlencoded'});
  constructor (private http: Http) {}

  getEvents (): Observable<Event[]> {
    let params = new URLSearchParams();
    params.set('format', 'json');
    params.set('callback', 'JSONP_CALLBACK');

    return this.http.get(this.eventsUrl+'/qevents', {headers: this.headers})
      .map(this.extractData)
      .catch(this.handleError);
  }

  getEvent (id: number): Observable<Event> {
    let params = new URLSearchParams();
    params.set('format', 'json');
    params.set('callback', 'JSONP_CALLBACK');

    return this.http.get(this.eventsUrl+'/qevents/'+id, {headers: this.headers})
      .map(this.extractData)
      .catch(this.handleError);
  }

  create(name: string, eDesc: string, eDate: string, loc: string, prize: string, teamsize: number, eStatus: number): Observable<Event> {
    const url = this.eventsUrl+'/qevents';

    if(eStatus==null) {
        eStatus = 0;
    }

    return this.http
      .post(url, JSON.stringify({action: 'create', 'data': [{
        name: name,
          eDesc: eDesc,
          eDate: eDate,
          loc: loc,
          prize: prize,
          teamsize: +teamsize,
          eStatus: +eStatus
      }]}), {headers: this.headers
      })
    .map(this.extractData)
      .catch(this.handleError);
  }

  remove(id: number): any {
    const url = this.eventsUrl+'/qevents/'+id;

      return this.http
          .post(url, JSON.stringify({action: 'delete', id: id}), {headers: this.headers})
          .toPromise()
          .catch(this.handleError);
  }

  update(event: Event): any {
    const url = this.eventsUrl+'/qevents';

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
