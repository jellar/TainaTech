import {Component, OnInit} from '@angular/core';
import {Person} from "../models/person.model";
import {PersonService} from "../_services/person.service";

@Component({
  selector: 'app-people-data',
  templateUrl: './people.component.html'
})
export class PeopleComponent implements OnInit {
  persons: Person[]
  page = 1;
  count = 0;
  pageSize = 10;
  name: '';
  pageSizes = [10, 15];

  constructor(private personService: PersonService) {
  }

  ngOnInit(): void {
    this.retrievePersons()
  }

  retrievePersons() {
    let name = this.name == undefined ? '' : this.name;
    this.personService.getPagedPersons(name, this.page, this.pageSize)
      .subscribe(
        response => {
          // @ts-ignore
          const {persons, count} = response;
          this.persons = persons;
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


