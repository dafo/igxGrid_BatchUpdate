import { Component, OnInit, ViewChild } from '@angular/core';

import { IgxDialogComponent, IgxGridComponent, Transaction } from 'igniteui-angular';
import { City } from './city';
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

  public data: City [];
  public transactionsData: Transaction[] = [];
  public errors: any[];

  public get transactions() {
    return this.grid.transactions;
  }

  constructor(private _cityService: CityService) { }

  public ngOnInit(): void {
    this._cityService.cities.subscribe(data => {
      this.data = data;
    });
    this.transactionsData = this.transactions.getAggregatedChanges(true);
    this.transactions.onStateUpdate.subscribe(() => {
        this.transactionsData = this.transactions.getAggregatedChanges(true);
    });
  }

  public addRow() {
    this.grid.addRow({
      CityID: this.generateID(),
      CityName: 'Provide city name!',
      HolidayDate: new Date(2019, 6, 15),
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
    this._cityService.commitCities(this.grid.transactions.getAggregatedChanges(true))
    .subscribe(res => {
      this.grid.transactions.commit(this.data);
    }, err => this.errors = err);
    this.dialog.close();
  }

  public cancel() {
    this.dialog.close();
  }

  public discard() {
    this.grid.transactions.clear();
    this.dialog.close();
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

  public generateID() {
    let number;
    number = 'temp' + Math.random();
    return number;
  }
}
