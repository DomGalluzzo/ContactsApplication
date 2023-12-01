import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
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
  form: FormGroup;
  contacts: Contact[];
  isSubmitted = false;

  constructor(
    private contactsService: ContactsService,
    private notificationService: NotificationService,
    private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    this.getContacts();
    this.getForm();
  }

  get formControls() {
    return this.form.controls;
  }

  createContact(): void {
    this.isSubmitted = true;
    if (this.isFormValid()) {
      this.contactsService.createContact(this.form.value as Contact).pipe(catchError((errorResponse: HttpErrorResponse) => {
        const error = errorResponse.error as BaseError;
        const message = !error.message ?
        'An error was encountered while creating contact' : error.message;

        this.notificationService.createError(message, error.title);
        this.isSubmitted = false;

        return EMPTY;
      }))
      .subscribe((data: Contact) => {
        this.contacts.push(data);
        this.form.reset();
        this.notificationService.createSuccess('New contact created', 'Success!');
        this.isSubmitted = false;
      });
    }
  }

  private getContacts(): void {
    this.contactsService.getContacts().pipe(catchError((errorResponse: HttpErrorResponse) => {
      const error = errorResponse.error as BaseError;
      const message = !error.message ?
        'An error was encountered while retrieving contacts data' : error.message;

        this.notificationService.createError(message, error.title);

        return EMPTY
    }))
    .subscribe((data: Contact[]) => this.contacts = data);
  }
  
  private getForm(): void {
    this.form = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  private isFormValid(): boolean {
    if (!this.form.valid) {
      this.notificationService.createError('Please make sure all required fields are filled out.', 'Error');

      return false;
    }

    return true;
  }
}
