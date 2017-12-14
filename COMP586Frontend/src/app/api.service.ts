import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';    // For observables 
import { Router } from '@angular/router';

@Injectable()
export class ApiService {

    // Creates an observable subject Property
    private selectedTransaction = new Subject<any>();

    private selectedWallet = new Subject<any>();

    // An observable property that holds the reference to the user-selected transaction.
    transactionSelected = this.selectedTransaction.asObservable();
    walletSelected = this.selectedWallet.asObservable();

    constructor(private http: HttpClient, private router: Router)
    {
    }

    // GET transaction using JSON from the back-end.
    getTransactions()
    {
        return this.http.get('http://comp586backend.azurewebsites.net/api/transactions');
    }

    // POST request transaction using JSON to the back-end.
    postTransaction(transaction)
    {
        this.http.post('http://comp586backend.azurewebsites.net/api/transactions', transaction).subscribe(response => {
            console.log(response)
        });
        this.router.navigate(['/transactions'])
    }


    // Gets the user-selected transaction
    selectTransaction(transaction)
    {
        this.selectedTransaction.next(transaction);
    }

    // Update transactions
    putTransaction(transaction)
    {
        this.http.put('http://comp586backend.azurewebsites.net/api/transactions/' + transaction.id , transaction).subscribe(response => {
            console.log(response)
        });
    }

    

    // GET wallets using JSON from the back-end.
    getWallets()
    {
        return this.http.get('http://comp586backend.azurewebsites.net/api/wallets');
    }

    // Gets the user-selected transaction
    selectWallet(wallet)
    {
        this.selectedWallet.next(wallet);
    }

    // POST request wallet using JSON to the back-end.
    postWallet(wallet)
    {
        this.http.post('http://comp586backend.azurewebsites.net/api/wallets', wallet).subscribe(response => {
            console.log(response)
        });

        this.router.navigate(['/home'])
    }
}