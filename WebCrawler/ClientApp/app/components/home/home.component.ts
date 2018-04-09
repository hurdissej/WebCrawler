import { Component } from '@angular/core';
import {CrawlService} from "../../services/CrawlService";
import {ResultFunc} from "rxjs/observable/GenerateObservable";

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    constructor(private crawlService: CrawlService){}
    queryResult = [];
    startUrl = "";
    limit = 0;
    
    getWebPages()
    {
        this.crawlService.get(this.startUrl, this.limit)
            .subscribe(res => this.queryResult = res)
    }

    printQuery() 
    {
        console.log(this.queryResult)
    }
}
