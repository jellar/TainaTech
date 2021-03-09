import {Inject, Injectable} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {getBaseUrl} from "../../main";
import {Person} from "../models/person.model";

//const baseUrl = getBaseUrl() + 'api/person';

@Injectable({ providedIn: 'root' })
export class PersonService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getAll() {
    return this.http.get<Person[]>(this.baseUrl+ 'api/person/all');
  }

  getById(id: number) {
    return this.http.get<Person>(`${this.baseUrl}api/person/${id}`);
  }
  getPagedPersons(name: string, page: number, size: number) {
   return this.http.get(this.baseUrl + `api/person/getpagedpeople?page=${page}&size=${size}&firstname=${name}`)
  }
  create(params: any) {
    return this.http.post(this.baseUrl + 'api/person', params);
  }

  update(params: any) {
    return this.http.put(this.baseUrl + 'api/person', params);
  }
}
