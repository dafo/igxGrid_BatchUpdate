import { Injectable } from '@angular/core';
import { map, shareReplay } from 'rxjs/operators';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { City } from './city';
import { Observable, Observer, merge } from 'rxjs';
import { Transaction } from 'igniteui-angular';

@Injectable()
export class CityService {

    private _getURL = 'http://localhost:36830/api/cities/';
    private _deleteURL = 'http://localhost:36830/api/cities1';
    private _addURL = 'http://localhost:36830/api/cities1';
    private _updateURL = 'http://localhost:36830/api/cities1';
    private _cities: Observable<City []>;

    constructor(private http: HttpClient) { }

    public get cities() {
        if (!this._cities) {
            this._cities = this.getCities().pipe(
                shareReplay(1)
            );
        }

        return this._cities;
    }

    getCities(): Observable<City[]> {
        return this.http.get<City[]>(this._getURL).pipe(map(response => response));
    }

    // Comment out the following if you want to use separate end-points
    // and to process the transactions on the client - side

    // commitCities(transactions): Observable<any> {
    //     const httpOptions = {
    //         headers: new HttpHeaders({
    //           'Content-Type':  'application/json'
    //         })
    //       };

    //     return Observable.create((observer: Observer<any>) => {
    //      this.http.post(this._getURL, transactions, httpOptions)
    //         .subscribe(res => {
    //             console.log('success');
    //             observer.next(res);
    //             observer.complete();
    //         }, err => observer.error(err));
    //     });
    // }

    // Uncomment the following if you want to use separate end-points
    // and to process the transactions on the client - side

    commitCities(transactions: Transaction[]): Observable<any> {
        const httpOptions = {
            headers: new HttpHeaders({
              'Content-Type':  'application/json'
            })
          };

        const requests: Observable<Object>[] = [];

        const updates = transactions.filter(x => x.type === 'update');
        const adds = transactions.filter(x => x.type === 'add');
        const deletes = transactions.filter(x => x.type === 'delete');

        if (deletes.length) {
            requests.push(this.http.delete(this._deleteURL, {
                params: {
                    ids: deletes.map(t => t.id)
                }
            }));
        }
        if (adds.length) {
            requests.push(this.http.post(this._addURL, adds.map(t => t.newValue), httpOptions));
        }
        if (updates.length) {
            requests.push(this.http.put(this._updateURL, updates.map(t => t.newValue), httpOptions));
        }

        // Thsi should be mergeMap probably
        return merge(...requests);
    }
}
