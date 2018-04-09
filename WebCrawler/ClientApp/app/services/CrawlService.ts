import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';

@Injectable()
export class CrawlService
{
    constructor(private http: Http){}
    
    get(startUrl: string, limit: number)
    {
        return this.http.get('/api/CrawlWebPage?webPage=' + startUrl + "&limit=" + limit).map(res => res.json());
    }
}