import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EMPTY, catchError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

import { Contact } from '../../../core/models/contact.model';
import { ContactsService } from '../../contacts.service';
import { BaseError } from '../../../core/models/base-error.model';
import { NotificationService } from '../../../core/services/notification-service.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent {
  @Input() contact: Contact;
  @Output() deleted = new EventEmitter<number>();

  constructor(
    private contactsService: ContactsService,
    private notificationService: NotificationService) {}

  deleteContact(): void {
    this.contactsService.deleteContact(this.contact.id).pipe(catchError((errorResponse: HttpErrorResponse) => {
      const error = errorResponse.error as BaseError;
      const message = !error.message ?
        'An error was encountered while deleting contact' : error.message;

      this.notificationService.createError(message, error.title);

      return EMPTY;
    }))
    .subscribe((data: number) => {
      this.deleted.emit(data);
    })
  }
}
