import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { Contact } from '../core/models/contact.model';

@Injectable()
export class ContactsService {
	private baseUrl = 'https://localhost:7123/api/Contacts';

	constructor(private http: HttpClient) {}

	getContacts(): Observable<Contact[]> {
		return this.http.get<Contact[]>(this.baseUrl);
	}

	createContact(contact: Contact): Observable<Contact> {
		return this.http.post<Contact>(this.baseUrl, contact);
	}

	deleteContact(contactId: number): Observable<number> {
		return this.http.delete<number>([this.baseUrl, contactId].join('/'));
	}

	updateContact(contactId: number, contactRequest: Contact): Observable<Contact> {
		return this.http.put<Contact>([this.baseUrl, contactId].join('/'), contactRequest);
	}
}