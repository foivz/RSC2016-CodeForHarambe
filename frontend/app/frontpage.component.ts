import { Component, OnInit } from '@angular/core';

import { Event } from './event';


@Component({
    moduleId: module.id,
    selector: 'frontpage',
    templateUrl: 'frontpage.component.html',
    styleUrls: ['css/main.css', 'css/cover.css']
})

export class FrontpageComponent implements OnInit {
    constructor() { }

    ngOnInit(): void {
    }
}

