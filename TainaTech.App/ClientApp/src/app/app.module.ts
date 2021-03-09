import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import {PeopleComponent} from "./people/people.component";
import {PersonFormComponent} from "./people/person.form.component";
import {NgxPaginationModule} from "ngx-pagination";
import {PersonService} from "./_services/person.service";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    PeopleComponent,
    PersonFormComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: PeopleComponent, pathMatch: 'full'},
      {path: "person/:mode/:id", component: PersonFormComponent},
      {path: "person/:mode", component: PersonFormComponent}
    ]),
    ReactiveFormsModule,
    NgxPaginationModule
  ],
  providers: [PersonService],
  bootstrap: [AppComponent]
})
export class AppModule { }
