import { Injectable } from "@angular/core";
import { Person } from "../models/person.model";
import { Observable } from "rxjs";
import { RestDataSource } from "./rest.datasource";

@Injectable()
export class PersonRepository {
  private products: Person[] = new Array<Person>();
  private locator = (p: Person, id: number) => p.personId == id;

  constructor(private dataSource: RestDataSource ) {

  }

  getPersons(url): Person[] {
    this.dataSource.getData(url).subscribe(data => this.products = data);
    return this.products;
  }

  getPerson(id: number): Person {
    return this.products.find(p => this.locator(p, id));
  }

  savePerson(product: Person) {
    if (product.personId == 0 || product.personId == null) {
      this.dataSource.saveProduct(product)
        .subscribe(p => this.products.push(p));
    } else {
      this.dataSource.updateProduct(product).subscribe(p => {
        let index = this.products
          .findIndex(item => this.locator(item, p.personId));
        this.products.splice(index, 1, p);
      });
    }
  }

}
