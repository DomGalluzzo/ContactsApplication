import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { NotificationService } from '../../../core/services/notification-service.service';
import { Notification } from '../../../core/models/notification.model';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.css'
})
export class NotificationComponent implements OnInit, OnDestroy {
  subscription: Subscription;
  notification: Notification;
  show = false;
  
  constructor(private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.subscription = this.notificationService.notification.subscribe(
      notification => {
        this.notification = notification;
        this.showDialog();
      }
    )
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  getClass(notification: Notification): string {
    return `notification--${notification.type}`;
  }

  showDialog(): void {
    this.show = true;
  }

  closeDialog():  void {
    this.show = false;
  }
}
