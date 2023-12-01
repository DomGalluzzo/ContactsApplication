import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { EMPTY, catchError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { Contact } from '../../../core/models/contact.model';
import { ContactsService } from '../../contacts.service';
import { BaseError } from '../../../core/models/base-error.model';
import { NotificationService } from '../../../core/services/notification-service.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent implements OnInit {
  @Input() contact: Contact;
  @Input() form: FormGroup;
  @Output() deleted = new EventEmitter<number>();
  @Output() updated = new EventEmitter<void>();
  isEditShown = false;
  isSubmitted = false;

  ngOnInit(): void {
    this.getForm();
  }

  constructor(
    private formBuilder: FormBuilder,
    private contactsService: ContactsService,
    private notificationService: NotificationService) {}

  toggleEdit(): void {
    this.isEditShown = !this.isEditShown;
    this.getForm();
  }

  deleteContact(): void {
    this.contactsService.deleteContact(this.contact.id)
      .pipe(catchError((errorResponse: HttpErrorResponse) => {
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

  updateContact(): void {
    this.isSubmitted = true;
    if (this.form.valid) {
      this.contactsService.updateContact(this.contact.id, this.form.value)
      .pipe(catchError((errorResponse: HttpErrorResponse) => {
        const error = errorResponse.error as BaseError;
        const message = !error.message ? 
        'An was encountered while updating contact' : error.message;
        
        this.notificationService.createError(message, error.title);
        this.isSubmitted = false;
        
        return EMPTY;
      }))
      .subscribe(() => {
        this.toggleEdit();
        this.isSubmitted = false;
        this.updated.emit();
      })
    }
  }

  private getForm(): void {
    this.form = this.formBuilder.group({
      firstName: [this.contact.firstName, Validators.required],
      lastName: [this.contact.lastName, Validators.required],
      email: [this.contact.email, [Validators.required, Validators.email]]
    })
  }
}
