import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable()
export class AuthenticationService {

    constructor(private http: HttpClient, private router: Router)
    {

    }

    // Register new users.
    register(credentials)
    {
        // Any type allows the response object to be converted to a string.
        this.http.post<any>('http://localhost:51119/api/account', credentials).subscribe(response =>{
            localStorage.setItem('token', response)  // creates a key value pair in local storage within the broswer.
            this.router.navigate(['/wallet'])
        });
    }
 
    // Log in existing users.
    login(credentials)
    {
        // Any type allows the response object to be converted to a string.
        this.http.post<any>('http://localhost:51119/api/account/login', credentials).subscribe(response =>{
            localStorage.setItem('token', response)  // creates a key value pair in local storage within the broswer.
            this.router.navigate(['/transaction']);
        })
    }

    get isAuthenticated() {
        var token = localStorage.getItem('token')

        // Boolean that checks if token simply exists.
        if(!token)
        {
            return false;
        }
        else 
        {
            return true;
        }

    }

    logout() 
    {
        localStorage.removeItem('token');
        this.router.navigate(['/home']);
    }


}