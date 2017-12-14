import { Component } from '@angular/core';
import { ApiService } from './api.service';
import { NgModel } from '@angular/forms';
import { CurrencyPipe } from '@angular/common';
import { DatePipe } from '@angular/common';
import {MatExpansionModule} from '@angular/material/expansion';


@Component({
    selector: 'transactions',
    templateUrl:'./transactions.component.html'
})
export class TransactionsComponent {

    // Object of class type any initialized to an empty object.
    transaction = {

    }

    // Property that can be binded to html.
    transactions
    
    // Constructor for the POST service
    constructor (public api: ApiService)
    {
        this.api.getTransactions().subscribe(response => {
            this.transactions = response    // Saves response to transactions list.
        });
    }

    // Call to API service to get transactions from back end.
    ngOnInit() {
        this.api.getTransactions().subscribe(response => {
            this.transactions = response    // Saves response to transactions list.
        });
    }

}