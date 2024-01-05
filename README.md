# Journal Serive API

This is an ASP.NET Core Web API that manages patients and their journals

#

#### Uses [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) as an ORM

#### Uses [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) for storing data

#### Adapts the [Vertical Slice](https://www.jimmybogard.com/vertical-slice-architecture/) architecture

#### Adapts the [Mediator Pattern](https://refactoring.guru/design-patterns/mediator) with [MediatR](https://github.com/jbogard/MediatR)

#### [MediatR](https://github.com/jbogard/MediatR) in turn adapts the [CQRS](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs) pattern
 
## Endpoints

![firefox_NXmlEwYry9](https://github.com/VasilisKoupourtiadis/journal-service/assets/89838633/acc55400-76c6-4526-b1ab-04ede8a50204)

## Demo

### Patient Endpoints

https://github.com/VasilisKoupourtiadis/journal-service/assets/89838633/7e8c384e-b68a-4e02-b5c5-65ec2300af89

### Journal Endpoints

https://github.com/VasilisKoupourtiadis/journal-service/assets/89838633/fe9e5655-3b4b-4cbc-b8b3-7b6892166893

### Journal Entry Endpoints

https://github.com/VasilisKoupourtiadis/journal-service/assets/89838633/ef4f5472-7bfe-48c2-8c30-b51326518ff8

## Run locally

Clone the project

```bash
  git clone https://github.com/VasilisKoupourtiadis/journal-service.git
```

If using Visual Studio

```bash
  Update-Database
```

If using CLI

```bash
  dotnet ef database update
```

Run the project through the "https" option and you should be redirected to the swagger generated page to test the endpoints.

> [!NOTE]
> When testing the endpoints with swagger you might see resolver errors at the top of the page. If you do, just ignore them, the endpoints will still work.
