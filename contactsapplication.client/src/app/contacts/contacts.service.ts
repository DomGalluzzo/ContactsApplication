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
}