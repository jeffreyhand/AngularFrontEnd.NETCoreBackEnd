** Update: The Azure backend server is rejecting requests due to CORS issue despite the configuration addressing it. I sent a ticket to Microsoft to resolve the issue and will update this when the site is back up **

# comp586project

To use this web application, go to http://comp586projectfrontend.azurewebsites.net/

This is digital currency wallet web application that allows users to record their purchases of different cryptocurrencies all in one place. Register using a valid email and a password (one uppercase letter and one special character is required). Then create a wallet name, such as “Bitcoin”. Finally, create a transaction using that wallet. A list of expandable transactions can be clicked on in the last Transactions page. The backend is in Asp.NET CORE 2.0  and the frontend is in Angular 5.0.5. The backend can be opened in Visual studio and the frontend app directory contains all the files needed to drop into an angular application folder and run.


**MVC Design Pattern**

The frontend files contain HTML (*.html) views and TypeScript (*.ts) for its models and controllers. The backend solution contains Model and Controller folders, respectively.


**Single-Page Application (SPA)**

Angular is a frameworks for Single Page Applications (SPAs). Routing and form submissions are fast because they employ the SPA techniques.


**Dependency Injection (DI)**
The backend ConfigureServices method in Startup.cs contains dependency injections for the database and identity services. Their constructor injections are marked in the comments where appropriate. There are also dependency injections in nearly all the Angular .ts files that pass in services.


**Object-relational mapping (ORM)**
Entity Framework Core is implemented in the backend to handle Object-relational mapping (ORM) Note: if running locally for the first time, the solution should be run first to build the database with Entity Framework if the database doesn’t exist already. It will seed the database initially. The CurrencyWalletContext.cs contains the three tables that are generated and synced with the ORM: Users, Wallets, and Transactions. There is a one-to-many relationship from Users to Wallet and a one-to-many relationship from Wallet to Transactions. Entity Framework automatically detects the IDs and assigns the foreign keys respectively.


**Token**
A JSON Web Token (JWT) is stored in the browsers localstorage in the frontend when the user is logged in. It is also saved in the backend’s database along with the rest of the user information. The JWT contains information to help identify the user. The frontend angular file, authentication.interceptor.ts, creates an HTTP interceptor that attaches the token to the header of any request. The authentication and login components on the frontend handle the business logic.


**Unit Testing**
Unit Testing with dependency injection is very easy find in the backend Visual Studio because there is a separate “UnitTestProject” test project attached to the COMP586Backend project. Those tests will test the generated database can be run by opening the Test Explorer. 
Unit tests in Angular are run with the built in Karma Test Runner. Files names sample and sampletest contain unit tests for the front end API. To run them, type “ng test” from the command line and Karma will open in a new browser window with the test results.

