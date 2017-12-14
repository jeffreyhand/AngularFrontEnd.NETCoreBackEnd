import { Component } from '@angular/core'
import { FormBuilder, Validators } from '@angular/forms'
import { ApiService } from './api.service';

@Component({
    templateUrl: './wallet.component.html'
})
export class WalletComponent {

    form
    
    wallets 


    constructor(public api: ApiService, public fb: FormBuilder) {
        this.form = fb.group({
            title: ['', Validators.required]
        })

        this.api.getWallets().subscribe(response => {
            this.wallets = response    // Saves response to wallets list.
        });
    }


}