#Thabo Matue Financial Transaction Management System Setup Instructions

## Prerequisites
- .NET SDK 8.x
- SQL Server Express (or SQL Server)
- Node.js 20+ and npm
- Angular CLI (optional, `npm install -g @angular/cli`)

## Database Setup (EF Core)
The API is configured for SQL Server Express in `FinancialTransactions.API/appsettings.json`:
```
Server=localhost\\SQLEXPRESS;Database=FinancialTransactionsDb;Trusted_Connection=True;TrustServerCertificate=True
```
The server used above is my local database server,so you will have to use yours that side then change 'localhost\\SQLEXPRESS' to point to your server

Apply migrations:
```
dotnet ef database update -p FinancialTransactions.Infrastructure -s FinancialTransactions.API
```

## Run the API
```
dotnet run --project FinancialTransactions.API
```
The API starts on `https://localhost:7150` and exposes Swagger at `/swagger`. I prefer to use Swagger for testing my endpoints.

## Run the Angular App
```
cd client/financial-transactions-app
npm install
ng serve (run this command within the path /src)
```
The UI runs at `http://localhost:4200` and targets the API at `https://localhost:7150` (see `src/environments/environment.ts`).

# Brief Explanation
- **Architecture**: Clean architecture split into `Domain`, `Application`, `Infrastructure`, and `API` projects.This architecture style is good for a scalability especially when the project grows.
- **Data access**: EF Core 8 with SQL Server and seeding for `TransactionStatuses`.
- **API**: Thin controllers delegating to services; ProblemDetails for errors.
- **UI**: Angular standalone components using Angular Material (`MatTable`, dialogs, reactive forms) and an HTTP error interceptor.Preferred Angular Material for simple UI.

## If I Had More Time more than that 24 hours from Lulama
- Add pagination, sorting, and filtering on the transactions table.
- Add unit tests for services and API endpoints.
- Add authentication/authorization and audit logging.
