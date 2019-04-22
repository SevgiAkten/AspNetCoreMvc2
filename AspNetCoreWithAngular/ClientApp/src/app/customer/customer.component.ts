import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-customre',
  templateUrl: './customer.component.html'
})
export class CustomerComponent {
  public customers: Customer[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Customer[]>(baseUrl + 'api/Customers').subscribe(result => {
      this.customers = result;
    }, error => console.error(error));
  }
}

class Customer {
  id: number;
  firstName: string;
  lastName: string;
}
