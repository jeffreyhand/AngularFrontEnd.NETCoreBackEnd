import { Component } from '@angular/core';
import { TransactionComponent } from './transaction.component';
import { AuthenticationService } from './authentication.service';

@Component({
  selector: 'nav',
  template: `
    <mat-toolbar>
        <button mat-button routerLink="/home">Home</button>
        <button *ngIf = "authentication.isAuthenticated" mat-button routerLink="/wallet">Wallets</button>
        <button *ngIf = "authentication.isAuthenticated" mat-button routerLink="/transaction">Create Transaction</button>
        <button *ngIf = "authentication.isAuthenticated" mat-button routerLink="/transactions">Ledger</button>
        <span style = "flex: 1 1 auto;"></span>
        <button *ngIf = "!authentication.isAuthenticated" mat-button routerLink="/register">Register</button>
        <button *ngIf = "!authentication.isAuthenticated"  mat-button routerLink="/login">Sign In</button>
        <button *ngIf = "authentication.isAuthenticated" mat-button (click) = "authentication.logout()">Log Out</button>
    </mat-toolbar>
  `
})
export class NavComponent
{

  // Contructor Injection for Authentication service logout in navigation bar.
  constructor(public authentication: AuthenticationService)
  {

  }

}
