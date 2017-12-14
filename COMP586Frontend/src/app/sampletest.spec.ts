describe('Sample Test', () => {
    it('true is true', () => expect(true).toBe(true));
   });
   
   import { AppComponent } from './app.component';
   import { WalletComponent } from './wallet.component';
   import { AuthenticationService } from './authentication.service';

   import { TestBed, inject } from '@angular/core/testing';
   import { NavComponent } from './nav.component';

      
  describe('Markdown Authentication service', () => {
    beforeEach(() => {
      TestBed.configureTestingModule({
        providers: [
            AuthenticationService
        ]
      });
    });
   

    it('Should translate markdown to HTML!', 
      inject([AuthenticationService], (authService) => {
   
      expect(authService).toBeDefined();
   
      expect(authService.toHtml('hi'))
                            .toContain('<p>hi</p>');
    }));
  });


  describe('No user logged in through Nav', () => {
    beforeEach(() => {
      TestBed.configureTestingModule({
        providers: [
            NavComponent
        ]
      });
    });

    it('Should only display Register or Sign In', 
    inject([NavComponent], (nav) => {
 
    expect(nav).toBeDefined();
 
    expect(nav.toHtml('')).toContain('Register');
    expect(nav.toHtml('')).toContain('Sign In');
  }));
});
