import { Component, OnInit } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, Validators, AbstractControl } from '@angular/forms';
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
  mainForm: FormArray;
  newContactForm: FormGroup;
  contacts: Contact[];
  isSubmitted = false;

  constructor(
    private contactsService: ContactsService,
    private notificationService: NotificationService,
    private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    this.getContacts();
    this.getNewContactForm();
  }

  get formControls() {
    return this.newContactForm.controls;
  }

  createContact(): void {
    this.isSubmitted = true;
    if (this.isFormValid(this.newContactForm)) {
      this.contactsService.createContact(this.newContactForm.value as Contact).pipe(catchError((errorResponse: HttpErrorResponse) => {
        const error = errorResponse.error as BaseError;
        const message = !error.message ?
        'An error was encountered while creating contact' : error.message;

        this.notificationService.createError(message, error.title);
        this.isSubmitted = false;

        return EMPTY;
      }))
      .subscribe((data: Contact) => {
        this.contacts.push(data);
        this.newContactForm.reset();
        this.notificationService.createSuccess('New contact created', 'Success!');
        this.isSubmitted = false;
      });
    }
  }

  removeContact(contactId: number): void {
    this.contacts = this.contacts.filter(c => c.id !== contactId);
    this.notificationService.createSuccess('Contact deleted', 'Success!');
  }

  updateContact(): void {
    this.getContacts();
    this.notificationService.createSuccess('Contact updated', 'Success!');
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
  
  private getNewContactForm(): void {
    this.newContactForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  private isFormValid(formGroup: FormGroup): boolean {
    if (!formGroup.valid) {
      this.notificationService.createError('Please make sure all required fields are filled out.', 'Error');

      return false;
    }

    return true;
  }
}
