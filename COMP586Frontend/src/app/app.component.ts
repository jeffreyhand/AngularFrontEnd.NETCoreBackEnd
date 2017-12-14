import { Component, NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { TransactionComponent } from './transaction.component';
import { RouterModule, Routes } from '@angular/router';


@Component({
  selector: 'app-root',
  template: '<nav></nav><router-outlet></router-outlet>'
})

@NgModule({
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})

export class AppComponent
{
  private hello: string = 'Hello, World!';
}
