import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Person } from '../interfaces/person';
import { PersonService } from '../services/person.service';

@Component({
  selector: 'app-person',
  templateUrl: './person.component.html',
  styleUrls: ['./person.component.scss']
})
export class PersonComponent implements OnInit {
  people$: Observable<Person[]> = new Observable<Person[]>();
  people: Person[] = [];
  
  formPerson: FormGroup = this.formBuilder.group({
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]]
  });
  errorMessage: any;
  firstName: string = '';
  lastName: string = '';

  constructor(
    private personService: PersonService,
    private formBuilder: FormBuilder,
    private avRoute: ActivatedRoute,
    private router: Router
  ){  }

  ngOnInit(): void {
    this.getPeople();
    this.firstName = '';
    this.lastName = '';
    this.formPerson = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]]
    });
  }

  getPeople(): void {
    this.people$ = this.personService.getPeople();
    this.people$.subscribe(ppl => this.people = ppl);
  }

  savePerson() {
    if(!this.formPerson.valid) {
      return;
    }

    let person: any = {
      firstName: this.formPerson.get("firstName")?.value,
      lastName: this.formPerson.get("lastName")?.value
    }

    this.personService.savePerson(person)
    .subscribe((data) => {
      console.log(data);

      this.formPerson.reset();
      //Fetch results after saved
      this.getPeople();
    });
  }
}
