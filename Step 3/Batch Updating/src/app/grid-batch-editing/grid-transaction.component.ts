import { Component } from '@angular/core';
import { IgxGridTransaction, IgxTransactionService } from 'igniteui-angular';

@Component({
  providers: [{ provide: IgxGridTransaction, useClass: IgxTransactionService }],
  selector: 'app-grid-batch-editing-with-transactions',
  template: '<ng-content></ng-content>'
})
export class GridBatchEditingWithTransactionsComponent { }
