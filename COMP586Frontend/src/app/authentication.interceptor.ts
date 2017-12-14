import { Injectable } from '@angular/core'
import { HttpInterceptor } from '@angular/common/http'

// Creates an HTTP interceptor (type of service) that will attach the token to the header of any request.
@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

    constructor() {}

    // Needed to implmenent the HttpInterceptor interface.
    intercept(request, next) {

        // Get token from browser's localstore
        var token = localStorage.getItem('token')

        // Copy the request being intercepted and modify the header to include the users token.
        var authenticationRequest = request.clone({
            headers: request.headers.set('Authorization', `Bearer ${token}`)   // passes token as template literal
        })
        return next.handle(authenticationRequest)
    }
}