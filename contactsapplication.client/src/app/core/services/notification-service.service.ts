import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Notification } from '../models/notification.model';

@Injectable({
	providedIn: 'root'
})

export class NotificationService {
	notification: Subject<Notification> = new Subject();

	createError(message: string, title: string): void {
		const error: Notification = this.createNotification(message, 'error', title);
		this.notification.next(error);
	}

	createSuccess(message: string, title: string): void {
		const success: Notification = this.createNotification(message, 'success', title);
		this.notification.next(success);
	}

	private createNotification(message: string, type: string, title: string): Notification {
		return {
			title,
			message,
			type
		};
	}
}