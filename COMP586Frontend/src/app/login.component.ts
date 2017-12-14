import { Component } from '@angular/core'
import { FormBuilder, Validators } from '@angular/forms'
import { AuthenticationService } from './authentication.service'

@Component({
    templateUrl: './login.component.html'
})
export class LoginComponent {

    form
    
    constructor(public authentication: AuthenticationService, public fb: FormBuilder) {
        this.form = fb.group({
            email: ['', Validators.required],
            password: ['', Validators.required]
        })
    }
}