import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
          MatButtonModule,
          MatFormFieldModule,
          MatInputModule,
          MatCardModule,
          MatListModule,
          MatSelectModule,
          MatOptionModule,
          MatToolbarModule,
          MatSnackBarModule,
 
        } from '@angular/material';

import { MatExpansionModule } from '@angular/material/expansion';

import { FormsModule, ReactiveFormsModule } from '@angular/forms'; 
import { RouterModule } from '@angular/router';
import { ApiService } from './api.service';
import { AppComponent } from './app.component';
import { TransactionComponent } from './transaction.component';
import { TransactionsComponent } from './transactions.component';
import { HomeComponent } from './home.component';
import { NavComponent } from './nav.component';
import { RegisterComponent } from './register.component';
import { LoginComponent } from './login.component';
import { AuthenticationService } from './authentication.service';
import { AuthenticationInterceptor } from './authentication.interceptor';
import { WalletComponent } from './wallet.component';
import { SampleComponent } from './sample/sample.component';

// List of component views that can be navigated to.
// routes to <router-outlet> where specified in components
// empty route path is the home component
const routes = [
   { path: 'home', component: HomeComponent },
    { path: 'wallet', component: WalletComponent },
    { path: 'transaction', component: TransactionComponent },
    { path: 'transactions', component: TransactionsComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent }
]

@NgModule({
  declarations: [
    AppComponent,
    TransactionComponent,
    TransactionsComponent,
	  HomeComponent,
    NavComponent,
    RegisterComponent,
    LoginComponent,
    WalletComponent,
    SampleComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
	  RouterModule.forRoot(routes),
    BrowserAnimationsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatOptionModule,
    MatCardModule,
    FormsModule,
    ReactiveFormsModule,  // Instead of putting validation in HTML, Model-driven "Reactive" Form where model itself contains the validation rules
    MatListModule,
    MatToolbarModule,
    MatSnackBarModule,
    MatExpansionModule
  ],
  providers: [
    ApiService,
    AuthenticationService,
    {provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true}    // Need this object for AuthenticationInterceptor
  ],
  bootstrap: [AppComponent]
})

export class AppModule
{

}
