import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { ContactsComponent } from './contacts/contacts.component';

import { ContactsService } from './contacts/contacts.service';
import { NotificationService }  from './core/services/notification-service.service';
import { ContactComponent } from './contacts/components/contact/contact.component';
import { NotificationComponent } from './contacts/components/notification/notification.component';

@NgModule({
  declarations: [
    AppComponent,
    ContactsComponent,
    ContactComponent,
    NotificationComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    ContactsService,
    NotificationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
