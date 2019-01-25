import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { ICity } from './city';
import { Observable } from 'rxjs';

@Injectable()
export class CityService {

    private _getURL = 'http://localhost:36830/api/cities/';
    private _commitURL = 'http://localhost:36830/api/cities/updatecities';

    constructor(private http: HttpClient) { }

    getCities(): Observable<HttpResponse<ICity[]>> {
        return this.http.get<ICity[]>(this._getURL, {observe: 'response' });
    }

    commitCities(transactions) {
        const httpOptions = {
            headers: new HttpHeaders({
              'Content-Type':  'application/json'
            })
          };
        this.http.post(this._commitURL, transactions, httpOptions)
            .subscribe(res => {
                console.log('success');
            });
    }
}
