# RaceDay API

## 1. Project Overview

RaceDay API is an ASP.NET Core Web API developed as the backend for a running, walking, and cycling event management system. It provides endpoints for working with events, event categories, participant enrolments, results, authentication, and participant profiles.

The project establishes a backend foundation for a future RaceDay application.

## 2. Technologies Used

- C#
- ASP.NET Core Web API
- .NET 10
- Entity Framework Core
- Microsoft SQL Server
- Swagger / OpenAPI
- xUnit
- Git and GitHub
- GitHub Actions

## 3. Main Features

- REST API architecture
- Event management
- Event category management
- Participant enrolment management
- Results management
- Authentication-related functionality
- Participant profile management
- Database access through Entity Framework Core
- API documentation through Swagger
- Automated build and test workflow

## 4. Project Structure

The solution contains the main API project and a separate automated testing project.

```text
RaceDayAPI/
├── Controllers/
├── Data/
├── DTOs/
├── Filters/
├── Migrations/
├── Models/
├── Services/
├── Properties/
├── Program.cs
├── appsettings.json
├── RaceDayAPI.csproj
├── RaceDayAPI.Tests/
│   ├── RaceDayAPI.Tests.csproj
│   └── UnitTest1.cs
└── .github/
    └── workflows/
        └── dotnet.yml
```

The exact folder structure may differ depending on the solution layout.

## 5. Requirements

To run the project locally, you need:

- Visual Studio with ASP.NET and web development tools
- The .NET 10 SDK
- Microsoft SQL Server
- A suitable SQL Server connection string

## 6. Configuration

The API uses a SQL Server connection string named `RaceDayConnection`.

Configure the connection string in your local application configuration. For example:

```json
{
  "ConnectionStrings": {
    "RaceDayConnection": "Server=YOUR_SERVER;Database=RaceDayDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_SERVER` and the database name with the values for your environment.

Never commit database passwords, private credentials, or other secrets to a public repository.

## 7. Running the API

1. Clone the repository:

   ```bash
   git clone https://github.com/Kamogelo47/RaceDayAPI1.git
   ```

2. Open the solution in Visual Studio.
3. Restore the NuGet packages.
4. Configure the SQL Server connection string.
5. Build the solution.
6. Set `RaceDayAPI` as the startup project.
7. Run the application.
8. Open the Swagger URL displayed by the application to explore the available endpoints.

## 8. Running Automated Tests

The solution includes a test project named `RaceDayAPI.Tests`.

To run the tests from the terminal, navigate to the directory containing the solution file and execute:

```bash
dotnet restore
dotnet build
dotnet test
```

The test project uses xUnit and ASP.NET Core testing infrastructure to verify API behaviour.

Test results depend on the tests currently implemented in the project.

## 9. Continuous Integration

The repository includes a GitHub Actions workflow at:

`.github/workflows/dotnet.yml`

The workflow is configured to restore dependencies, build the solution, and run automated tests when changes are pushed to the `master` branch or when a pull request targets that branch.

Workflow results can be viewed in the **Actions** tab of the GitHub repository.

## 10. API Testing Checklist

The following checks can be performed using Swagger or a suitable API testing tool.

- [ ] Confirm that the API starts successfully.
- [ ] Open Swagger and confirm that the API endpoints are listed.
- [ ] Test event endpoints using valid requests.
- [ ] Test event category endpoints.
- [ ] Test enrolment endpoints with valid and invalid input.
- [ ] Test the results endpoints.
- [ ] Test authentication-related endpoints.
- [ ] Check how invalid requests are handled.
- [ ] Run the automated tests using `dotnet test`.
- [ ] Review the GitHub Actions workflow results after pushing changes.

**Note:** Mark each item complete only after you have tested it successfully.

## 11. Future Improvements

Potential future enhancements include:

- Improved authentication and authorisation
- Additional validation and error handling
- Expanded automated integration tests
- Improved API documentation
- Additional event and participant management functionality
- Deployment to a cloud hosting platform

## 12. Repository

**GitHub repository:**  
https://github.com/Kamogelo47/RaceDayAPI1

## 13. Author

Developed as part of the PROG6212 RaceDay API project.
