import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public persons: Person[] = [];
  public submitButtonText: string = 'Create';

  private http: HttpClient;
  
  private isCreate: boolean = true;

  bufPersonForUpdateID: number = 0;
   

  showModal: boolean = false;

  firstNameFilter: string = '';
  lastNameFilter: string = '';
  phoneNumberFilter: string ='';

  constructor(http: HttpClient) {
    this.http = http;
  }

  get filteredPersons(): Person[] {
    return this.persons.filter(person =>
      person.firstName.toLowerCase().includes(this.firstNameFilter.toLowerCase()) &&
      person.lastName.toLowerCase().includes(this.lastNameFilter.toLowerCase()) &&
      person.phoneNumbers.map(phone => phone.phoneNumberValue).
        join().toLowerCase().includes(this.phoneNumberFilter.toLowerCase())
    );
  }

  openModal(): void {
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
    this.isCreate = true;
    this.submitButtonText = 'Create';
    this.resetForm();
    this.fetchPersons();
  }



  person = {
    firstName: '',
    lastName: '',
    phoneNumbers: [] as string[],
  }

  addPhoneNumber() {
    this.person.phoneNumbers.push('');
  }

  removePhoneNumber(index: number) {
    this.person.phoneNumbers.splice(index, 1);
  }

  ngOnInit(): void {
    this.fetchPersons()    
  }

  getPhoneNumbersAsString(phoneNumbers: PhoneNumber[]): string {
    return phoneNumbers.map(phone => phone.phoneNumberValue).join(', ');
  }

  fetchPersons(): void {
    this.http.get<Person[]>('https://localhost:5001/api/persons/').subscribe(result => {
      this.persons = result;
    }, error => console.error(error));
  }

  submitPerson(): void {
    if (this.isCreate) {
      this.createPerson();
    }
    if (!this.isCreate) {
      this.isCreate = true;
      this.submitButtonText = 'Create';

      this.http.put<any>(`https://localhost:5001/api/persons/${this.bufPersonForUpdateID}` , this.person)
        .subscribe(
          (response) => {
            console.log('Персона успешно Обновлена:', response);
            this.closeModal()
          },
          (error) => {
            console.error('Ошибка при обновлении персоны:', error);
          }
        );
    }
  }

  updatePerson(person: Person): void {

    this.openModal();

    this.person = {
      firstName: person.firstName,
      lastName: person.lastName,
      phoneNumbers: person.phoneNumbers.map(phoneNumber => phoneNumber.phoneNumberValue),
    };

    this.isCreate = false;
    this.submitButtonText = 'Update';
    this.bufPersonForUpdateID = person.personId;
  }

  createPerson(): void {
    this.http.post<any>('https://localhost:5001/api/persons/', this.person)
      .subscribe(
        (response) => {
          console.log('Персона успешно создана:', response);
          
          this.closeModal();
        },
        (error) => {
          console.error('Ошибка при создании персоны:', error);
        }
      );
  }

  deletePerson(person: Person): void {
    this.http.delete(`https://localhost:5001/api/persons/${person.personId}`)
      .subscribe(
        () => {
          console.log('Персона успешно удалена');

          this.fetchPersons();
        },
        (error) => {
          console.error('Ошибка при удалении персоны:', error);
        }
      );
  }

  resetForm(): void {
    this.person = {
      firstName: '',
      lastName: '',
      phoneNumbers: []
    };
  }

  

}

interface Person {
  personId: number;
  firstName: string;
  lastName: string;
  phoneNumbers: PhoneNumber[]
}
interface PhoneNumber {
  phoneNumberId: number,
  phoneNumberValue: string,
  person: string;
}
