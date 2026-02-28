import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { ToastService, Toast } from '../../services/toast.service';

@Component({
  selector: 'app-toast',
  standalone: false,
  template: `
    <div class="toast-container">
      <div
        *ngFor="let toast of toasts"
        class="custom-toast"
        [ngClass]="toast.type"
      >
        <i class="bi"
           [ngClass]="{
             'bi-check-circle-fill': toast.type === 'success',
             'bi-x-circle-fill': toast.type === 'error',
             'bi-exclamation-triangle-fill': toast.type === 'warning'
           }"></i>
        <span class="toast-message">{{ toast.message }}</span>
        <button class="toast-close" (click)="removeToast(toast)">
          <i class="bi bi-x-lg"></i>
        </button>
      </div>
    </div>
  `
})
export class ToastComponent implements OnInit, OnDestroy {
  toasts: Toast[] = [];
  private subscription!: Subscription;

  constructor(private toastService: ToastService) {}

  ngOnInit(): void {
    this.subscription = this.toastService.toast$.subscribe((toast) => {
      this.toasts.push(toast);
      setTimeout(() => this.removeToast(toast), 4000);
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  removeToast(toast: Toast): void {
    const index = this.toasts.indexOf(toast);
    if (index > -1) {
      this.toasts.splice(index, 1);
    }
  }
}
