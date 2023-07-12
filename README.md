# bidTest

This repository contains:
- Front End App: BidTest/FrontEnd/person & ReadMe: BidTest/FrontEnd/person/README.md
- Back End Api: BidTest/BackEndApi/BackEndApi - c# .Net 6 API

Front End App expects the back end api is running at https://localhost:7239/
This can be changed at BidTest/FrontEnd/person/src/app/environments/environment.ts
Front end app is built using Angular 15, with Rxjs & Bootstrap 5.

Back End Api is only configured to accept Cors request from http://localhost:4200. Please make sure when running Front End App it is running on port 4200.
This can be changed in Startup.cs file.
Back end api can be tested using Swagger.
Back end contains BackEndApiTests project - which can help in testing the api functionality (built using Xunit, Moq & Fluent Assertions TDD/BDD style).
