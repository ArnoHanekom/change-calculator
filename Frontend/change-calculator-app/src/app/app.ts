import { Component, signal } from '@angular/core';
import { RouterOutlet, RouterLinkWithHref, RouterLinkActive } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatToolbarModule, MatButtonModule, RouterLinkWithHref, RouterLinkActive],  
  styleUrl: './app.css',
  template: `
    <mat-toolbar color="primary" class="flex gap-4">
      <button mat-button routerLink="/" routerLinkActive="mat-raised-button" color="accent">New Transaction</button>
      <button mat-button routerLink="/history" routerLinkActive="mat-raised-button" color="accent">Transaction History</button>
    </mat-toolbar>

    <main class="p-6">
      <router-outlet></router-outlet>
    </main>
  `
})
export class App {
  protected readonly title = signal('change-calculator-app');
}
