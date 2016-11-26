"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var http_1 = require('@angular/http');
var Observable_1 = require('rxjs/Observable');
var EventService = (function () {
    function EventService(http) {
        this.http = http;
        this.eventsUrl = 'http://rsc-harambe.azurewebsites.net/api'; // URL to web API
        this.headers = new http_1.Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
    }
    EventService.prototype.getEvents = function () {
        var params = new http_1.URLSearchParams();
        params.set('format', 'json');
        params.set('callback', 'JSONP_CALLBACK');
        return this.http.get(this.eventsUrl + '/users', { headers: this.headers })
            .map(this.extractData)
            .catch(this.handleError);
    };
    EventService.prototype.getEvent = function (id) {
        var params = new http_1.URLSearchParams();
        params.set('format', 'json');
        params.set('callback', 'JSONP_CALLBACK');
        return this.http.get(this.eventsUrl + '/users/' + id, { headers: this.headers })
            .map(this.extractData)
            .catch(this.handleError);
    };
    EventService.prototype.create = function (name, location, date) {
        var url = this.eventsUrl + '/users';
        return this.http
            .post(url, JSON.stringify({ action: 'create', 'data': [{ name: name, location: location, date: date }] }), { headers: this.headers })
            .toPromise()
            .catch(this.handleError);
    };
    EventService.prototype.remove = function (id) {
        var url = this.eventsUrl + '/users/' + id;
        return this.http
            .delete(url, { headers: this.headers })
            .toPromise()
            .catch(this.handleError);
    };
    EventService.prototype.update = function (event) {
        var url = this.eventsUrl + '/users';
        return this.http
            .post(url, JSON.stringify({ action: 'update', 'data': [event] }), { headers: this.headers })
            .toPromise()
            .then(function () { return event; })
            .catch(this.handleError);
    };
    EventService.prototype.extractData = function (res) {
        var body = res.json();
        return body || {};
    };
    EventService.prototype.handleError = function (error) {
        // In a real world app, we might use a remote logging infrastructure
        var errMsg;
        if (error instanceof http_1.Response) {
            var body = error.json() || '';
            var err = body.error || JSON.stringify(body);
            errMsg = error.status + " - " + (error.statusText || '') + " " + err;
        }
        else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Observable_1.Observable.throw(errMsg);
    };
    EventService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], EventService);
    return EventService;
}());
exports.EventService = EventService;
//# sourceMappingURL=user.service.js.map