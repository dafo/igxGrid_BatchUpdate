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
  @ViewChild('gridRowEditTransaction', { read: IgxGridComponent, static: true }) public grid: IgxGridComponent;
  @ViewChild(IgxDialogComponent, { static: true }) public dialog: IgxDialogComponent;
  @ViewChild('dialogGrid', { read: IgxGridComponent, static: true }) public dialogGrid: IgxGridComponent;

  public data: City [];
  public transactionsData: Transaction[] = [];
  public errors: any[];

  public get transactions() {
    return this.grid.transactions;
  }

  constructor(private cityService: CityService) { }

  public ngOnInit(): void {
    this.cityService.cities.subscribe(data => {
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
    let addResult: { [id: number]: City};

    this.cityService.commitCities(this.grid.transactions.getAggregatedChanges(true))
      .subscribe(res => {
          if (res) {
            addResult = res;
          }
        },
        err => this.errors = err,
        () => {
          // all done, commit transactions
          this.grid.transactions.commit(this.data);
          if (!addResult) {
            return;
          }
          // update added records IDs with ones generated from backend
          for (const id of Object.keys(addResult)) {
            const item = this.data.find(x => x.CityID === parseInt(id, 10));
            item.CityID = addResult[id].CityID;
          }
          this.data = [...this.data];
        }
      );
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
    return parseInt(new Date().getTime().toString().substring(4), 10);
  }

  cellExitEditMode(event) {
    console.log('Exit edit mode!');
  }
}
