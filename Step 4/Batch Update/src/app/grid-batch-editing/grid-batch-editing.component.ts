import { Component, OnInit, ViewChild } from '@angular/core';

import { IgxDialogComponent, IgxGridComponent, Transaction } from 'igniteui-angular';
import { ICity } from './city';
import { Observable } from 'rxjs';
import { CityService } from './city.service';

@Component({
  selector: 'app-grid-batch-editing',
  styleUrls: ['./grid-batch-editing.component.scss'],
  templateUrl: './grid-batch-editing.component.html'
})
export class GridBatchEditingComponent implements OnInit {
  @ViewChild('gridRowEditTransaction', { read: IgxGridComponent }) public grid: IgxGridComponent;
  @ViewChild(IgxDialogComponent) public dialog: IgxDialogComponent;
  @ViewChild('dialogGrid', { read: IgxGridComponent }) public dialogGrid: IgxGridComponent;

  public data = [];
  public response = [];
  public transactionsData: Transaction[] = [];
  private addCityId: number;

  public get transactions() {
    return this.grid.transactions;
  }

  constructor(private _cityService: CityService) { }

  public ngOnInit(): void {
    this._cityService.getCities().subscribe(resp => {
      resp.body.forEach((elem) =>
        this.response.push({
          'CityID': elem.CityID,
          'CityName': elem.CityName,
          'Population': elem.Population,
          'HolidayDate': new Date(elem.HolidayDate),
          'TrainStation': elem.TrainStation,
          'Description': elem.Description
        })
      );

      this.data = this.response;
	    this.addCityId = this.data.length + 1;
    });
    this.transactionsData = this.transactions.getAggregatedChanges(true);
    this.transactions.onStateUpdate.subscribe(() => {
        this.transactionsData = this.transactions.getAggregatedChanges(true);
    });
  }

  public addRow() {
    this.grid.addRow({
      CityID: this.addCityId++,
      CityName: 'Provide city name!',
      HolidayDate:new Date(2019, 6, 15),
      Population: 0,
      TrainStation: false,
      Description: 'Provide description!'
    });
  }

  public deleteRow(event, rowID) {
    this.grid.deleteRow(rowID);
  }

  public openCommitDialog() {
    this.dialog.open();
    this.dialogGrid.reflow();
  }

  public commit() {
    this._cityService.commitCities(this.grid.transactions.getAggregatedChanges(true));
    this.grid.transactions.commit(this.data);
    this.dialog.close();
  }

  public cancel() {
    this.dialog.close();
  }

  public discard() {
    this.grid.transactions.clear();
    this.dialog.close();
  }

  private getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }

  private classFromType(type: string): string {
    return `transaction--${type.toLowerCase()}`;
  }

  public get hasTransactions(): boolean {
    return this.grid.transactions.getAggregatedChanges(false).length > 0;
  }

  public stateFormatter(value: string) {
    return JSON.stringify(value);
  }

  public typeFormatter(value: string) {
    return value.toUpperCase();
  }
}
