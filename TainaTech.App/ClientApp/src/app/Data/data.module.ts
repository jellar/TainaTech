import { NgModule } from "@angular/core";
import { HttpClientModule, HttpClientJsonpModule } from "@angular/common/http";
import { RestDataSource, REST_URL } from "./rest.datasource";

import {PersonRepository} from "./person.repository";

@NgModule({
  imports: [HttpClientModule, HttpClientJsonpModule],
  providers: [PersonRepository, RestDataSource,
    { provide: REST_URL, useValue: "http://localhost:3500/products" }]
})
export class ModelModule { }
