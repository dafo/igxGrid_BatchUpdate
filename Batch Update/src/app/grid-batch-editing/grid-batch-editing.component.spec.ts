import { FormsModule } from '@angular/forms';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

import { GridBatchEditingComponent } from './grid-batch-editing.component';
import { GridWithTransactionsComponent } from './grid-transaction.component';
import {
	IgxButtonModule,
	IgxDialogModule,
	IgxFocusModule,
	IgxGridModule,
	IgxRippleModule
} from 'igniteui-angular';

describe('GridBatchEditingComponent', () => {
  let component: GridBatchEditingComponent;
  let fixture: ComponentFixture<GridBatchEditingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GridBatchEditingComponent, GridWithTransactionsComponent ],
      imports: [ FormsModule, NoopAnimationsModule, IgxDialogModule, IgxGridModule, IgxFocusModule, IgxButtonModule, IgxRippleModule ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GridBatchEditingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
