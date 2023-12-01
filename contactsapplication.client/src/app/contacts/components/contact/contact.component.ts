import { Component, Input } from '@angular/core';

import { Contact } from '../../../core/models/contact.model';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent {
  @Input() contact: Contact;
}
