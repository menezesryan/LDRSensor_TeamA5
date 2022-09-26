import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { BasicComponent } from './basic/basic.component';
import { HomeComponent } from './home/home.component';
import { ContentComponent } from './content/content.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { AppRoutingModule } from './app-routing.module';
import { SetThresholdComponent } from './set-threshold/set-threshold.component';
import { ManualModeComponent } from './manual-mode/manual-mode.component';

@NgModule({
  declarations: [
    AppComponent,
    BasicComponent,
    HomeComponent,
    ContentComponent,
    NavBarComponent,
    SetThresholdComponent,
    ManualModeComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
     
    ]),
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
