import {Component, Inject, OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {PersonRepository} from "../Data/person.repository";
import {Person} from "../models/person.model";

@Component({
  selector: 'app-people-data',
  templateUrl: './people.component.html'
})
export class PeopleComponent implements OnInit{
  persons: Person[]
  page = 1;
  count = 0;
  pageSize = 10;
  name: '';
  pageSizes = [10, 15];
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

    // http.get<Person[]>(baseUrl + 'api/person/all').subscribe(result => {
    //   this.people = result;
    // }, error => console.error(error));

  }

  ngOnInit(): void {
    this.retrievePersons()
  }

  retrievePersons() {
    let name = this.name == undefined ? '' : this.name;
    this.http.get(this.baseUrl + `getpagedpeople?page=${this.page}&size=${this.pageSize}&firstname=${name}`)
      .subscribe(
        response => {
          // @ts-ignore
          const { persons, count } = response;
          this.persons = persons;
          console.log(this.persons);
          this.count = count;
        },
        error => console.error(error));
  }

  handlePageChange(event) {
    this.page = event;
    this.retrievePersons();
  }

  handlePageSizeChange(event) {
    this.pageSize = event.target.value;
    this.page = 1;
    this.retrievePersons();
  }



}


