import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { IgxNavigationDrawerModule, IgxNavbarModule, IgxLayoutModule, IgxRippleModule, IgxGridModule, IgxFocusModule, IgxButtonModule, IgxDialogModule } from 'igniteui-angular';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { GridBatchEditingComponent } from './grid-batch-editing/grid-batch-editing.component';
import { GridBatchEditingWithTransactionsComponent } from './grid-batch-editing/grid-transaction.component';

import { HttpClientModule } from '@angular/common/http';
import { CityService } from './grid-batch-editing/city.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    GridBatchEditingComponent,
    GridBatchEditingWithTransactionsComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    IgxNavigationDrawerModule,
    IgxNavbarModule,
    IgxLayoutModule,
    IgxRippleModule,
    IgxGridModule,
    IgxFocusModule,
    IgxButtonModule,
    IgxDialogModule,
    HttpClientModule
  ],
  providers: [CityService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
