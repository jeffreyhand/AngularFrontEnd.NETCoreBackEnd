import { Component } from '@angular/core'
import { FormBuilder, Validators } from '@angular/forms'
import { ApiService } from './api.service';

@Component({
    templateUrl:'./transaction.component.html'
})
export class TransactionComponent {

    form

    wallets

    constructor(public api: ApiService, public fb: FormBuilder) {

        this.form = fb.group({
            quantity: ['', Validators.required],
            price: ['', Validators.required],
            walletTitle: ['', Validators.required]
        })

        this.api.getWallets().subscribe(response => {
            this.wallets = response    // Saves response to wallets list.
        });
    }


}