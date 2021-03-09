import { Injectable, Inject, InjectionToken } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { Person } from "../models/person.model";
import { catchError, delay } from "rxjs/operators";

export const REST_URL = new InjectionToken("rest_url");

@Injectable()
export class RestDataSource   {
  constructor(private http: HttpClient,
              @Inject(REST_URL) private url: string) { }

  getData(url): Observable<Person[]> {
    return this.sendRequest<Person[]>("GET", url);
  }

  saveProduct(product: Person): Observable<Person> {
    return this.sendRequest<Person>("POST", this.url, product);
  }

  updateProduct(product: Person): Observable<Person> {
    return this.sendRequest<Person>("PUT",
      `${this.url}/${product.personId}`, product);
  }

  deleteProduct(id: number): Observable<Person> {
    return this.sendRequest<Person>("DELETE", `${this.url}/${id}`);
  }

  private sendRequest<T>(verb: string, url: string, body?: Person)
    : Observable<T> {

    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set("Access-Key", "<secret>");
    myHeaders = myHeaders.set("Application-Names", ["exampleApp", "proAngular"]);

    return this.http.request<T>(verb, url, {
      body: body,
      headers: myHeaders
    })
      //.pipe(delay(5000))
      .pipe(catchError((error: Response) =>
        throwError(`Network Error: ${error.statusText} (${error.status})`)));
  }
}
