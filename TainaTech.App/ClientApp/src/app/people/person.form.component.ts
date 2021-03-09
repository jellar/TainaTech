import {Component, Inject, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {Person} from "../models/person.model";
import {HttpClient, HttpHeaders} from "@angular/common/http";
// import {NgForm} from "@angular/forms";
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import {Observable, throwError} from "rxjs";
import {catchError} from "rxjs/operators";

@Component({
  selector: 'person-form',
  templateUrl: 'person.form.component.html'
  })
export class PersonFormComponent implements OnInit{
  form!: FormGroup;
  id!: string;
  isAddMode!: boolean;
  loading = false;
  submitted = false;
  person: Person = new Person();

  constructor(private http: HttpClient,
              @Inject('BASE_URL') private baseUrl: string,
              private formBuilder: FormBuilder,
              private route: ActivatedRoute,
              private router: Router) {

    // activeRoute.params.subscribe(params => {
    //   this.editing = params["mode"] == "edit";
    //   let id = params["id"];
    //   if (id != null) {
    //     http.get<Person>(baseUrl + 'api/person/'+ id).subscribe(result => {
    //       this.person = result;
    //     }, error => console.error(error));
    //     //Object.assign(this.product, model.getProduct(id) || new Product());
    //     //Object.assign(this.originalProduct, this.product);
    //   }
    // })
  }

  genderList: any = ["Female", "Male","Unknown"]
  dateOfBirth: Date;
  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = this.route.snapshot.params["mode"] == "create";

    const formOptions: AbstractControlOptions = {  };
    this.form = this.formBuilder.group({
      firstname: ['', Validators.required],
      surname: ['', Validators.required],
      gender: ['', Validators.required],
      emailAddress: ['', [Validators.required, Validators.email]],
      phoneNumber: [''],
      dateOfBirth: ['', Validators.required]
    }, formOptions);

    if (!this.isAddMode) {
      this.http.get<Person>(this.baseUrl + 'api/person/'+ this.id).subscribe(result => {
        // this.person = result;
        this.form.patchValue(result);
        this.dateOfBirth = result.dateOfBirth;
      }, error => console.error(error));

    }
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }


  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    if (this.isAddMode) {
      this.createUser();
    } else {
      this.updateUser();
    }
  }

  private createUser() {
    this.sendRequest("POST", this.baseUrl + 'api/person/', this.form.value)
      .subscribe(()=>{
        this.router.navigateByUrl("/")
      }).add(()=> this.loading = false)
  }

  private updateUser() {
    let gender: number = this.form.value.gender;
    let person = {...this.form.value, personId: this.route.snapshot.params['id'], gender}
    this.sendRequest("PUT", this.baseUrl + 'api/person/', person)
      .subscribe(()=>{
        this.router.navigateByUrl("/")
      }).add(()=> this.loading = false);
  }








  // submitForm(form: NgForm) {
  //   if (form.valid) {
  //     let verb = this.editing ? 'PUT' : 'POST';
  //     this.sendRequest(verb, this.baseUrl + 'api/person/', this.person)
  //       .subscribe(p=> this.router.navigateByUrl("/"))
  //
  //     //this.model.saveProduct(this.product);
  //     //this.originalProduct = this.product;
  //     //this.router.navigateByUrl("/");
  //   }
  // }

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
