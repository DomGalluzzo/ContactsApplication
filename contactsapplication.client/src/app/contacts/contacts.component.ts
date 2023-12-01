import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { catchError, EMPTY } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

import { ContactsService } from './contacts.service';
import { Contact } from '../core/models/contact.model';
import { BaseError } from '../core/models/base-error.model';
import { NotificationService } from '../core/services/notification-service.service';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrl: './contacts.component.css'
})
export class ContactsComponent implements OnInit {
  contacts: Contact[];

  constructor(
    private contactsService: ContactsService,
    private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.getContacts();
  }

  private getContacts(): void {
    this.contactsService.getContacts().pipe(catchError((errorResponse: HttpErrorResponse) => {
      const error = errorResponse.error as BaseError;
      const message = !error.message ?
        'An error was encountered while retrieving contacts data' : error.message;

        this.notificationService.CreateError(message, error.title);

        return EMPTY
    }))
    .subscribe((data: Contact[]) => this.contacts = data);
  }
}
