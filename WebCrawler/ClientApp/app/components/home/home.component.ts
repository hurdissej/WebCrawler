import { Component } from '@angular/core';
import {CrawlService} from "../../services/CrawlService";
import {ResultFunc} from "rxjs/observable/GenerateObservable";

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    constructor(private crawlService: CrawlService){}
    queryResult = [
    ];
    timeTaken = 0;
    length = 0;
    startUrl = "";
    limit = 0;
    
    getWebPages()
    {
        this.crawlService.get(this.startUrl, this.limit)
            .subscribe(res =>{ this.queryResult = res; this.timeTaken = res.timeTaken; this.length = res.webPages.length})
    }

    printQuery() 
    {
        console.log(this.queryResult)
    }
}
